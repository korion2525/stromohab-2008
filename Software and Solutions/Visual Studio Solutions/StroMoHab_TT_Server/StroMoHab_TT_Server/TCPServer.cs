using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows;
using StroMoHab_Objects.Objects;
using StroMoHab_TT_Server.Treadmill;
using StroMoHab_TT_Server.DataProcessing;
using StroMoHab_TT_Server.MotionCapture;

namespace StroMoHab_TT_Server.Communication
{
    /// <summary>
    /// TCPServer handels connections from the clients and passes incoming commands from them to OptitrackCommandParser_Server
    /// it doesn't issue any commands to MotionCapture, but it does pass motion capture data to JointProcessor and FilteredMarkerProcessor
    /// It listens to the events from MotionCapture,JointProcessor,FilteredMarkerProcessor,OptitrackCameraList,TreadmillController and 
    /// transmits the required data over the TCP connection to connected clients
    /// </summary>
    public class TCPServer
    {
        #region Member Variables
        private TcpListener tcpListener;
        private Thread listenThread;
        private static int port = 8001;
        private const int MAX_LENGTH_OF_RECIEVED_DATA_PACKET = 10;
        private string connectionStatus = "Waiting for connections";


        //lists and timestamps for the lists
        private long tempMarkerListTimeStamp = 0;
        private long tempTrackableListTimeStamp = 0;
        private List<Marker> tempMarkerList = new List<Marker>();
        private List<Trackable> tempTrackableList = new List<Trackable>();

        //Determins if FilteredMarkerProcessor should be called
        private bool processFilteredMarkers = true;

        // Motion Capture Treadmill - AKA automcatic treadmill detection via mc
        StroMoHab_TT_Server.Treadmill.MotionCaptureTreadmill mcTreadmill = new StroMoHab_TT_Server.Treadmill.MotionCaptureTreadmill();

        
        OptitrackCommandParser_Server commandParser = new OptitrackCommandParser_Server();

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
            MotionCaptureController.MarkerListAvaliable += new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCapture_MarkerListAvaliableEvent);
            OptitrackCameraList.TransmitCameraEvent += new OptitrackCameraList.TransmitCameraHandler(OptitrackCameraList_TransmitCameraEvent);
            TreadmillController.TransmitSpeedEvent += new TreadmillController.TransmitSpeedEventHandler(TreadmillController_TransmitSpeedEvent);
            MotionCaptureController.TrackableListAvaliable += new MotionCaptureController.TrackableListAvaliableEventHandler(MotionCapture_TrackableListAvaliableEvent);
            JointProcessor.JointListAvaliable += new JointProcessor.JointListAvaliableEventHandler(JointProcessor_JointListAvaliableEvent);
            FilteredMarkerProcessor.FilteredMarkerListAvaliable +=new FilteredMarkerProcessor.FilteredMarkerListAvaliableEventHandler(FilteredMarkerProcessor_FilteredMarkerListAvaliableEvent);
            OptitrackCommandParser_Server.ToggleFeetCommandReceivedEvent += new OptitrackCommandParser_Server.ToggleFeetCommandRecievedHandler(OptitrackCommandParser_Server_ToggleFeetCommandReceivedEvent);

            
            // Setup the multicast timer
            System.Windows.Forms.Timer serverIPMulticastTimer = new System.Windows.Forms.Timer();
            serverIPMulticastTimer.Interval = 500;
            serverIPMulticastTimer.Tick += new EventHandler(serverIPMulticastTimer_Tick);
            serverIPMulticastTimer.Start();

            
            // Setup treadmill speed detection callibration
            mcTreadmill.DetectTreadmillSpeed = StroMoHab_TT_Server.Properties.Settings.Default.DetectTreadmillSpeed;
            mcTreadmill.LoadTreadmillCalibration();

            //Setup remoting events
            DataStorage.TaskDataManager.RegisterEvents();
            DataStorage.PatientDataManager.RegisterEvents();
            DataStorage.ClinicianDataManager.RegisterEvents();
        }

        #region Multicast Server IP Broadcast

        /// <summary>
        /// Sets up and sends out a multicast message to alerlt client applications
        /// of the servers IP address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serverIPMulticastTimer_Tick(object sender, EventArgs e)
        {
            // Set up the socket and ip endpoint to the correct multicast address
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("224.100.0.1"), 9050);
            // Include a message just in case another service is using the same multicast IP and port number to ensure the clients
            // only connect to the correct IP
            byte[] data = Encoding.ASCII.GetBytes("StroMoHab Server");
            // Send the data and close the socket
            server.SendTo(data, iep);
            server.Close();
        }
        #endregion Multicast Server IP Broadcast


        #region Event Handlers

        void OptitrackCommandParser_Server_ToggleFeetCommandReceivedEvent(bool display)
        {
            Transmit('F', display);
        }


        void FilteredMarkerProcessor_FilteredMarkerListAvaliableEvent()
        {
            List<Marker> tempFilteredMarkerList = new List<Marker>(FilteredMarkerList.listOfMarkers);

            int[] treadmillMakrerIDNumbers;
            if (mcTreadmill.DetectTreadmillSpeed)
            {
                treadmillMakrerIDNumbers = mcTreadmill.FindSpeed(tempFilteredMarkerList); // Find the speed and get the 1st and (if applicable) 2nd treadmill marker id
               
                if (treadmillMakrerIDNumbers != null) // If a marker number was returned then strip them from the list
                {
                    int newMarkerNumber = 0;
                    List<Marker> newTempFilteredMarkerList = new List<Marker>();
                    foreach (Marker tempMarker in tempFilteredMarkerList)
                    {
                        //If the marker ID doesn't match the first or second tredmill marker then add it to the new filtered makrer lsit
                        //or the 3rd or 4th - now extended to allow for upto 4 visible treadmill markers
                        if ((tempMarker.MarkerId != treadmillMakrerIDNumbers[0]) && (tempMarker.MarkerId != treadmillMakrerIDNumbers[1]))
                            //&& (tempMarker.MarkerId != treadmillMakrerIDNumbers[2]) && (tempMarker.MarkerId != treadmillMakrerIDNumbers[3]))
                        {
                            Marker newTempMarker = new Marker(tempMarker);
                            newTempMarker.MarkerId = newMarkerNumber;
                            newMarkerNumber++;
                            newTempFilteredMarkerList.Add(newTempMarker);
                            System.Diagnostics.Debug.WriteLine("Filtered marker x/y/z:" + newTempMarker.xCoordinate + " " + newTempMarker.yCoordinate + " " + newTempMarker.zCoordinate);
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine("\n");
                        
                    // Save the newly generated list
                    tempFilteredMarkerList = new List<Marker>(newTempFilteredMarkerList);
                }

            }



            foreach (Marker marker in tempFilteredMarkerList)
            {
                TransmitFiltered(marker);
            }
            Marker endOfFrameMarker = new Marker();
            endOfFrameMarker.TimeStamp = tempFilteredMarkerList.Count;
            endOfFrameMarker.MarkerId = -2147483648;
            TransmitFiltered(endOfFrameMarker);

        }

        void JointProcessor_JointListAvaliableEvent(List<Joint> jointList)
        {
            List<Joint> tempJointList = new List<Joint>(jointList);
            int numJoints = 0;
            foreach (Joint joint in tempJointList)
            {

                if (joint.Name == null) // Prevents binarywriter inside Transmit() trying to write a null name
                    joint.Name = "Unknown";
                if (joint.Exists)
                {
                    Transmit(joint);
                    numJoints += 1;
                }
            }

            Joint endOfFrameJoint = new Joint();
            endOfFrameJoint.TimeStamp = numJoints;
            endOfFrameJoint.ID = -2147483648;
            endOfFrameJoint.Name = "End";
            Transmit(endOfFrameJoint);
        }

        private void TreadmillController_TransmitSpeedEvent(float newSpeed)
        {
            Transmit(newSpeed);
        }

        private void OptitrackCameraList_TransmitCameraEvent(OptiTrackCamera camera)
        {
            Transmit(camera);
        }

        private void MotionCapture_MarkerListAvaliableEvent(List<Marker> markerList, long timeStamp)
        {
            tempMarkerList = new List<Marker>(markerList);
            tempMarkerListTimeStamp = timeStamp;

            foreach (Marker marker in tempMarkerList)
            {
                Transmit(marker);
            }
            Marker endOfFrameMarker = new Marker();
            endOfFrameMarker.TimeStamp = markerList.Count;
            endOfFrameMarker.MarkerId = -2147483648;
            Transmit(endOfFrameMarker);


        }

        private void MotionCapture_TrackableListAvaliableEvent(List<Trackable> trackableList, long timeStamp)
        {
            tempTrackableList = new List<Trackable>(trackableList);
            tempTrackableListTimeStamp = timeStamp;

            foreach (Trackable trackable in tempTrackableList)
            {
                trackable.TimeStamp = timeStamp;
                if (trackable.Name == null) // Prevents binarywriter in Transmit trying to write a null name
                    trackable.Name = "Unknown";
                Transmit(trackable);
            }
            Trackable endOfFrameTrackable = new Trackable();
            endOfFrameTrackable.TimeStamp = trackableList.Count;
            endOfFrameTrackable.ID = -2147483648;
            endOfFrameTrackable.Name = "End";
            Transmit(endOfFrameTrackable);



            //Filter Markers - if they are from the same camera update FilteredMarkers
            if (tempTrackableListTimeStamp == tempMarkerListTimeStamp && processFilteredMarkers) 
                FilteredMarkerProcessor.UpdatedFilteredMarkers(tempMarkerList, tempTrackableList);

            //Update Joints
            JointProcessor.UpdateJoints(tempTrackableList);
            
        }

        #endregion Event Handlers


        #region TCP Client Methods

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

                if(ClientConnectionAcceptedEvent!=null)
                    ClientConnectionAcceptedEvent(clientConnection.Client.RemoteEndPoint);

                
                connectionStatus = "Connection accepted from " + clientConnection.Client.RemoteEndPoint;
                System.Diagnostics.Debug.WriteLine(connectionStatus);
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

                

                commandParser.handleCommand(recievedCommand);
            }

        }
        #endregion TCP Client Methods


        #region Transmit

        /// <summary>
        /// Transmits a filtered marker to the current tcpClient.
        /// </summary>
        /// <param name="marker"></param>
        public void TransmitFiltered(Marker marker)
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

                            binaryWriter.Write('F');

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
        /// Transmits a trackable to the current tcpClient.
        /// </summary>
        /// <param name="trackable"></param>
        public void Transmit(Trackable trackable)
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

                            binaryWriter.Write('R'); // T is used for Treadmill

                            binaryWriter.Write(trackable.ID);
                            binaryWriter.Write(trackable.TimeStamp);
                            binaryWriter.Write(trackable.TrackableIndex);
                            binaryWriter.Write(trackable.Name);
                            binaryWriter.Write(trackable.xCoordinate);
                            binaryWriter.Write(trackable.yCoordinate);
                            binaryWriter.Write(trackable.zCoordinate);
                            binaryWriter.Write(trackable.Roll);
                            binaryWriter.Write(trackable.Pitch);
                            binaryWriter.Write(trackable.Yaw);
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
        /// Transmits a joint to the current tcpClient.
        /// </summary>
        /// <param name="joint"></param>
        public void Transmit(Joint joint)
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

                            binaryWriter.Write('J');

                            binaryWriter.Write(joint.ID);
                            binaryWriter.Write(joint.TimeStamp);
                            binaryWriter.Write(joint.Exists);
                            binaryWriter.Write(joint.Name);
                            binaryWriter.Write(joint.Trackable1);
                            binaryWriter.Write(joint.Trackable2);
                            binaryWriter.Write(joint.RollOffset);
                            binaryWriter.Write(joint.PitchOffset);
                            binaryWriter.Write(joint.YawOffset);
                            binaryWriter.Write(joint.Roll);
                            binaryWriter.Write(joint.Pitch);
                            binaryWriter.Write(joint.Yaw);
                            binaryWriter.Write(joint.xCoordinate);
                            binaryWriter.Write(joint.yCoordinate);
                            binaryWriter.Write(joint.zCoordinate);
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
                MotionCaptureController.MarkerListAvaliable -= new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCapture_MarkerListAvaliableEvent);
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
                MotionCaptureController.MarkerListAvaliable += new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCapture_MarkerListAvaliableEvent);
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
        /// <param name="type">type</param>
        /// <param name="flag">flag</param>
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

                            binaryWriter.Write('B');
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


        #region Motion Capture and Status Methods

        /// <summary>
        /// Gets the connection status of the server, e.g. waiting for connections or a client has connected
        /// </summary>
        /// <returns></returns>
        public string ConnectionStatus()
        {
            return connectionStatus;
        }

        /// <summary>
        /// Starts the Motion Capture Engine
        /// </summary>
        public void Start()
        {
            commandParser.handleCommand(new byte[4] { 0, 0, 0, 0 });

        }

        /// <summary>
        /// Stops the Motion Capture Engine
        /// </summary>
        public void Stop()
        {
            commandParser.handleCommand(new byte[4] { 0, 0, 1, 0 });
        }

        /// <summary>
        /// Shuts down the api and drivers so that the application can close
        /// </summary>
        public void FinalShutdown()
        {
            commandParser.handleCommand(new byte[4] { 0, 0, 9, 0 });
        }

        /// <summary>
        /// Returns true if the Motion Capture engine is running, and false if not
        /// </summary>
        public bool Running()
        {
            return MotionCaptureController.APIRunning;
        }


        /// <summary>
        /// Returns the number of connected cameras as an int
        /// </summary>
        public int CameraCount()
        {
            return MotionCaptureController.GetConnectedCameraCount();
        }

        /// <summary>
        /// Returns a log containing any errors during startup
        /// </summary>
        /// <returns></returns>
        public string StartupLog()
        {
            return commandParser.StartupLog;
        }

        /// <summary>
        /// The number of trackables visiable
        /// </summary>
        public int TrackableCount
        {
            get { return tempTrackableList.Count; }

        }

        #endregion Motion Capture and Status Methods


        public bool DetectTreadmillSpeed
        {
            get 
            {
                return mcTreadmill.DetectTreadmillSpeed;
            }
            set
            {
                mcTreadmill.DetectTreadmillSpeed = value;
            }
        }


        public void CalibrateTreadmill()
        {
            mcTreadmill.CalibrateTreadmill();
        }
    }
}


