using System;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Stromohab_MCE
{
    class TCPServer
    {
        #region Member Variables
        private TcpListener tcpListener;
        private Thread listenThread;
        private static int port = 8001;
        private const int MAX_LENGTH_OF_RECIEVED_DATA_PACKET = 10;

        TcpClient clientConnection;

        private List<TcpClient> clientConnectionList;

        private object m_lock = new object();

        #endregion Member Variables
        #region Delegate Declarations
        public delegate void ServerListeningStartedHandler(int port);
        public delegate void ClientConnectionAcceptedHandler(EndPoint clientConnection);
        #endregion Delegate Delclarations
        #region Event Declarations
        /// <summary>
        /// Fired when server starts listening for clients.
        /// </summary>
        public static event ServerListeningStartedHandler ServerListeningStartedEvent;
        /// <summary>
        /// Fired when a new client connection is established.
        /// </summary>
        public static event ClientConnectionAcceptedHandler ClientConnectionAcceptedEvent;
        #endregion Event Declarations

        /// <summary>
        /// Creates a new thread that listens for clients
        /// </summary>
        public TCPServer()
        {
            clientConnectionList = new List<TcpClient>(4);

            this.tcpListener = new TcpListener(IPAddress.Any, port);               // listen on all network interfaces, on port number (defined in Declarations)
            this.listenThread = new Thread(new ThreadStart(ListenForClients));      // create new thread to do listening
            listenThread.Name = "ListenThread"; //Name thread (useful for debugging)
            listenThread.IsBackground = true;
            this.listenThread.Start();  // Start the listening thread
            // Signal listening thread has started, if have any listeners
            if (ServerListeningStartedEvent != null)
            {
                ServerListeningStartedEvent(port);
            }
            // Register events
            MotionCapture.CoordinatesAvailableEvent += new MotionCapture.CoordinatesAvailableHandler(MotionCapture_CoordinatesAvailableEvent);
            
            OptitrackCameraList.TransmitCameraEvent += new OptitrackCameraList.TransmitCameraHandler(OptitrackCameraList_TransmitCameraEvent);
            TreadmillController.TransmitSpeedEvent += new TreadmillController.TransmitSpeedEventHandler(TreadmillController_TransmitSpeedEvent);
        }

        private void TreadmillController_TransmitSpeedEvent(float newSpeed)
        {
            Transmit(newSpeed);
        }

        private void OptitrackCameraList_TransmitCameraEvent(OptiTrackCamera camera)
        {
            Transmit(camera);
        }

        private void MotionCapture_CoordinatesAvailableEvent(Marker marker)
        {
            Transmit(marker);
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();   // start listening for incoming connections. Maximum incoming connections can be entered as argument to Start()

            while (true)
            {
                // blocks until a client has connected to the server
                clientConnection = this.tcpListener.AcceptTcpClient();
                clientConnection.NoDelay = true;
                clientConnection.SendTimeout = 200;

                // create a thread to handle communication with the connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Name = "Client Thread";
                clientThread.IsBackground = true;
                clientThread.Start(clientConnection);

                ClientConnectionAcceptedEvent(clientConnection.Client.RemoteEndPoint);

                System.Diagnostics.Debug.WriteLine("Connection accepted from " + clientConnection.Client.RemoteEndPoint);

            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            clientConnectionList.Add(tcpClient);

            if (tcpClient.Connected == false)
            {
                return;
            }

            NetworkStream clientStream = tcpClient.GetStream();

            //BinaryReader binaryReader = new BinaryReader(clientStream);

            int bytesRead;
            byte[] recievedCommand = new byte[10];

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    if (tcpClient.Connected == true)
                    {
                        byte[] packetLength = new byte[2];
                        //read the packet length from the stream
                        bytesRead = clientStream.Read(packetLength, 0, 2);

                        //create a buffer of a size to fit the pending packet length
                        recievedCommand = new byte[BitConverter.ToUInt16(packetLength, 0)];

                        for (int i = 0; i < 2; i++)
                        {
                            recievedCommand[i] = packetLength[i];
                        }
                        //read the remaining data from the stream and write it to the recieved command array.
                        bytesRead += clientStream.Read(recievedCommand, 2, recievedCommand.Length - 2);

                    }
                }
                catch
                {
                    // a socket error has occured
                    System.Diagnostics.Debug.WriteLine("Socket error has occured in TCPServer.HandleClientComm");
                    break;
                }
                if (bytesRead == 0)
                {
                    // the client has disconnected from the server
                    System.Diagnostics.Debug.WriteLine("The client has disconnected from the server");
                    break;
                }

                //else message has been successfully recieved

                //write line to debug window

                System.Diagnostics.Debug.WriteLine("Command received: ");
                foreach (byte singleByte in recievedCommand)
                {
                    System.Diagnostics.Debug.WriteLine((int)singleByte);
                }

                OptitrackCommandParser_Server commandParser = new OptitrackCommandParser_Server();

                commandParser.handleCommand(recievedCommand);
            }

        }

        #region Transmit

        /// <summary>
        /// Transmits a marker to the current tcpClient.
        /// </summary>
        /// <param name="marker"></param>
        public void Transmit(Marker marker)
        {
            //lock this code so that if multiple Transmits are called (marker + speed for example), only one thread can access the connection stream at a time
            lock (m_lock)
            {
                //create a list to store any dead connections in (for removal from the main list at the end of the function)
                List<TcpClient> connectionsToRemoveFromList = new List<TcpClient>(clientConnectionList.Count);

                foreach (TcpClient singleClientConnection in clientConnectionList)
                {
                    try
                    {
                        if (singleClientConnection.Connected == false)
                        {
                            throw new System.IO.IOException(singleClientConnection.Client.RemoteEndPoint.ToString() + " is no longer connected to the server");
                        }
                        else
                        {
                            //send the data
                            BinaryWriter binaryWriter = new BinaryWriter(new BufferedStream(singleClientConnection.GetStream()));

                            binaryWriter.Write('M');

                            binaryWriter.Write(marker.MarkerId);
                            binaryWriter.Write(marker.TimeStamp);
                            binaryWriter.Write(marker.xCoordinate);
                            binaryWriter.Write(marker.yCoordinate);
                            binaryWriter.Write(marker.zCoordinate);
                            binaryWriter.Flush();
                        }
                    }

                    catch (System.IO.IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }

                    catch (InvalidOperationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }

                }

                foreach (TcpClient connectionToRemove in connectionsToRemoveFromList)
                {
                    clientConnectionList.Remove(connectionToRemove);
                }
            }
        }

        /// <summary>
        /// Transmits a camera to the current tcpClient
        /// </summary>
        /// <param name="camera"></param>
        public void Transmit(OptiTrackCamera camera)
        {
            lock (m_lock)
            {
                MotionCapture.CoordinatesAvailableEvent -= new MotionCapture.CoordinatesAvailableHandler(MotionCapture_CoordinatesAvailableEvent);
                //if there is a client connection
                if (clientConnectionList.Count != 0)
                {
                    //and if the client is connected
                    if (clientConnectionList[0].Connected == true)
                    {

                        foreach (TcpClient singleClientConnection in clientConnectionList)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(new BufferedStream(singleClientConnection.GetStream()));

                            binaryWriter.Write('C');

                            binaryWriter.Write(camera.CameraNumber);
                            binaryWriter.Write(camera.xCoordinate);
                            binaryWriter.Write(camera.yCoordinate);
                            binaryWriter.Write(camera.zCoordinate);

                            double[] rotationMatrixBuffer = camera.RotationMatrix;
                            System.Diagnostics.Debug.WriteLine("Camera rotation matrix: ");
                            for (int i = 0; i < 9; i++)
                            {
                                System.Diagnostics.Debug.WriteLine(rotationMatrixBuffer[i].ToString());
                                binaryWriter.Write(rotationMatrixBuffer[i]);
                            }

                            binaryWriter.Flush();
                        }
                    }
                }
                MotionCapture.CoordinatesAvailableEvent += new MotionCapture.CoordinatesAvailableHandler(MotionCapture_CoordinatesAvailableEvent);
            }
        }

        /// <summary>
        /// Transmits the current speed to all tcpClients.
        /// </summary>
        /// <param name="speed"></param>
        public void Transmit(float speed)
        {
            lock (m_lock)
            {
                List<TcpClient> connectionsToRemoveFromList = new List<TcpClient>(clientConnectionList.Count);

                foreach (TcpClient singleClientConnection in clientConnectionList)
                {
                    try
                    {
                        if (singleClientConnection.Connected == false)
                        {
                            throw new System.IO.IOException(singleClientConnection.Client.RemoteEndPoint.ToString() + " is no longer connected to the server. Error thrown from Transmit Speed");
                        }
                        else
                        {
                            //send the data
                            BinaryWriter binaryWriter = new BinaryWriter(new BufferedStream(singleClientConnection.GetStream()));

                            binaryWriter.Write('T');
                            binaryWriter.Write((double)speed);
                            binaryWriter.Flush();
                        }
                    }

                    catch (System.IO.IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }

                    catch (InvalidOperationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }
                }
            }
        }

        /// <summary>
        /// Transmits a labelled flag to all tcpClients.
        /// </summary>
        /// <param name="speed"></param>
        public void Transmit(char type, bool flag)
        {
            lock (m_lock)
            {
                List<TcpClient> connectionsToRemoveFromList = new List<TcpClient>(clientConnectionList.Count);

                foreach (TcpClient singleClientConnection in clientConnectionList)
                {
                    try
                    {
                        if (singleClientConnection.Connected == false)
                        {
                            throw new System.IO.IOException(singleClientConnection.Client.RemoteEndPoint.ToString() + " is no longer connected to the server. Error thrown from Transmit Speed");
                        }
                        else
                        {
                            //send the data
                            BinaryWriter binaryWriter = new BinaryWriter(new BufferedStream(singleClientConnection.GetStream()));

                            binaryWriter.Write('F');
                            binaryWriter.Write(type);
                            binaryWriter.Write(flag);
                            binaryWriter.Flush();
                        }
                    }

                    catch (System.IO.IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }

                    catch (InvalidOperationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("The client has disconnected. Exception message: " + ex.Message);
                        connectionsToRemoveFromList.Add(singleClientConnection);
                    }
                }
            }
        }

        #endregion Transmit
    }
}
