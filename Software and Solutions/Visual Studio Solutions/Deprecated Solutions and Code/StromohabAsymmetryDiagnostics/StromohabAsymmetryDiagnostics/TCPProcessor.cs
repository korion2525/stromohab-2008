using System;

using System.Net.Sockets;
using System.Threading;
using System.IO;
using Stromohab_MCE;


namespace Stromohab_MCE_Connection
{
    /// <summary>
    /// Connects to, and handles data from the Motion Capture Server.
    /// </summary>
    public class TCPProcessor
    {
        #region Member Variables

        private static TcpClient m_client;
        
        #endregion Member Variables

        #region Delegate Declarations
        
        /// <summary>
        /// Delegate for the WholeFrameReceived Event.
        /// </summary>
        public delegate void WholeFrameReceivedHandler();

        /// <summary>
        /// Delegate for the AvatarSpeedChanged Event.
        /// </summary>
        /// <param name="newSpeed"></param>
        public delegate void AvatarSpeedChangedHandler(float newSpeed);
        
        #endregion Delegate Delclarations

        #region Event Declarations
        
        /// <summary>
        /// Fired when a whole frame has been received.
        /// </summary>
            public static event WholeFrameReceivedHandler WholeFrameReceivedEvent;

        /// <summary>
        /// Raised when avatar speed is changed
        /// </summary>
            public static event AvatarSpeedChangedHandler AvatarSpeedChangedEvent;

        #endregion Event Declarations  


        #region Properties

        /// <summary>
        /// The tcpClient connection to the server.
        /// </summary>
        public static TcpClient Connection
        {
            get
            {
                return m_client;
            }
        }

        #endregion Properties

            #region Public Methods
            /// <summary>
            /// Connects to a given IPEndPoint
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <returns></returns>
        public static bool ConnectToServer(System.Net.IPEndPoint ipEndPoint)
        {
            m_client = new TcpClient();
            bool connectionSuceeded = false;

            try
            {
                m_client.Connect(ipEndPoint);
                connectionSuceeded = true;

                // create a thread to handle data received from Server
                Thread HandleDataFromServerThread = new Thread(new ParameterizedThreadStart(HandleDataFromServer));
                HandleDataFromServerThread.Name = "Handle Data From Server Thread";
                HandleDataFromServerThread.IsBackground = true;
                HandleDataFromServerThread.Start(m_client);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("The server is not running");

                return (connectionSuceeded = false);
            }
            return (connectionSuceeded);
        }

        #endregion Public Methods

        #region Private Methods
        private static void HandleDataFromServer(object client)
        {
            TcpClient serverConnection = (TcpClient)client;
            //NetworkStream serverStream = serverConnection.GetStream();

            Marker receivedMarker = new Marker();
            OptiTrackCamera receivedCamera = new OptiTrackCamera();

            BinaryReader binaryReader = new BinaryReader(new BufferedStream(serverConnection.GetStream()));

            int bytesRead;
            while (true)
            {
                bytesRead = 0;
                bool readTimestamp = false;
                try
                {

                    //blocks until a client sends a message

                    char messageType = binaryReader.ReadChar();
                    bytesRead = 1;

                    switch (messageType)
                    {
                        case 'M': 
                            receivedMarker.MarkerId = binaryReader.ReadInt32();
                            
                            //if have been sent End of Frame indication
                            //TimeStamp field of End of Frame Indicator contains number of markers in the frame
                            //ie. receivedMarker.TimeStamp = number of markers in list
                            if (receivedMarker.MarkerId == -2147483648)
                            {
                                receivedMarker.TimeStamp = binaryReader.ReadInt32();
                                readTimestamp = true;
                                MarkerList.RemoveExcessMarkersFromList(receivedMarker.TimeStamp);

                                //MarkerList.MarkerBinList();

                                if (WholeFrameReceivedEvent != null)
                                {
                                    WholeFrameReceivedEvent();
                                }
                            }

                            if (readTimestamp == false)
                            {
                                receivedMarker.TimeStamp = binaryReader.ReadInt32();
                            }
                            
                            receivedMarker.xCoordinate = binaryReader.ReadDouble();
                            receivedMarker.yCoordinate = binaryReader.ReadDouble();
                            receivedMarker.zCoordinate = binaryReader.ReadDouble();

                            bytesRead = 36;

                            if (receivedMarker.MarkerId != -2147483648)
                            {
                                MarkerList.AddMarker(receivedMarker);
                            }

                            break;

                        case 'C':
                            receivedCamera.CameraNumber = binaryReader.ReadInt32();
                            receivedCamera.xCoordinate = binaryReader.ReadDouble();
                            receivedCamera.yCoordinate = binaryReader.ReadDouble();
                            receivedCamera.zCoordinate = binaryReader.ReadDouble();

                            double[] rotationMatrixBuffer = new double[9];

                            for (int i = 0; i < 9; i++)
                            {
                                rotationMatrixBuffer[i] = binaryReader.ReadDouble();
                            }
                            receivedCamera.RotationMatrix = rotationMatrixBuffer;

                            bytesRead = 108;

                            OptitrackCameraList.AddCamera(receivedCamera);

                            break;

                        case 'T':
                            if (AvatarSpeedChangedEvent != null)
                            {
                                AvatarSpeedChangedEvent((float)binaryReader.ReadDouble());
                                bytesRead = 8;
                            }
                            else
                            {
                                binaryReader.ReadDouble();
                                bytesRead = 8;
                            }
                            break;

                        default:
                            System.Diagnostics.Debug.WriteLine("A transmit error has occured. " + messageType.ToString() + " recieved");
                            bytesRead = 5;
                            break;
                    }


                }
                catch (System.IO.IOException)
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

                //OptitrackCommandParser commandParser = new OptitrackCommandParser();

                //commandParser.handleCommand(recievedCommand[0]);
            }
        }
        #endregion
    }
}
