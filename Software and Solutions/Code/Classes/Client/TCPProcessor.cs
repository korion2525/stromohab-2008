using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;
using StroMoHab_Objects.Communication.Forms;

namespace StroMoHab_Objects.Communication
{
    /// <summary>
    /// Connects to, and handles data from the Motion Capture Server.
    /// </summary>
    public class TCPProcessor
    {
        #region Member Variables
        /// <summary>
        /// The TCP Client object
        /// </summary>
        private static TcpClient m_client;
        /// <summary>
        /// Trackable List
        /// </summary>
        private static List<Trackable> trackableList = new List<Trackable>();
        /// <summary>
        /// Joint List
        /// </summary>
        private static List<Joint> jointList = new List<Joint>();
        /// <summary>
        /// Server IP Address used by m_client
        /// </summary>
        private static string _serverIPAddress = "";
        private static bool _firstFailedFindAttempt = true;
        private static string _defaultIP = "";
        private static bool _connected = false;

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


        /// <summary>
        /// Delegate for the TrackableListReceived Event
        /// </summary>
        /// <param name="trackableList">The Updated Trackable list</param>
        public delegate void TrackableListReceivedHandler(List<Trackable> trackableList);

        /// <summary>
        /// Delegate for the FilteredMarkerListReceived Event
        /// Access data Via FilteredMarkerList.listOfMarkers
        /// </summary>
        public delegate void FilteredMarkerListReceivedHandler();

        /// <summary>
        /// Delegate for the JointListReceivedEvent
        /// </summary>
        /// <param name="jointList">The Updated Joint List</param>
        public delegate void JointListReceivedHandler(List<Joint> jointList);

        /// <summary>
        /// Delegate for the ToggleDrawingOfFeetEvent.
        /// </summary>
        public delegate void ToggleDrawingOfFeetHandler(bool display);

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

        /// <summary>
        /// Fired when a whole trackable list has been received
        /// </summary>
        public static event TrackableListReceivedHandler TrackableListReceivedEvent;


        /// <summary>
        /// Fired when a whole joint list has been received
        /// </summary>
        public static event JointListReceivedHandler JointListReceivedEvent;


        /// <summary>
        /// Fired when a whole filtered marker list has been received
        /// </summary>
        public static event FilteredMarkerListReceivedHandler FilteredMarkerListReceivedEvent;

        /// <summary>
        /// Triggered when a command to toggle the drawing of the feet has been received.
        /// </summary>
        public static event ToggleDrawingOfFeetHandler ToggleDrawingOfFeetEvent;

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

        public static bool ConnectedToServer
        {
            get { return _connected; }
        }
        /// <summary>
        /// The IP address of the server
        /// </summary>
        public static string ServerIPAddress
        {
            get { return _serverIPAddress; }
        }
        #endregion Properties


        #region Public Methods
        /// <summary>
        /// Manages finding the server and connecting to it it
        /// The IP address used can be retried by accessing the ServerIPAddress property
        /// </summary>
        /// <returns></returns>
        public static void ManagedConnectToServer()
        {

            bool connected = false;
            string serverIP = "";

            while (!connected)
            {
                // Find the IP (either automatically or from the user) then try to connect
                serverIP = FindServerIPAddress();

                // In debug mode findserveripaddress returns null to allow debbugging without connecting to the server correctly
                // when this happens connect to local host via sync connect which allows fails, let it time out and then continue
                if (serverIP != null)
                {
                    System.Net.IPEndPoint tempIpAddress = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(serverIP), 8001);
                    if (connected = ConnectToServerAsync(tempIpAddress))
                        _connected = connected;

                }
                else
                {
                    System.Net.IPEndPoint tempIpAddress = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8001);
                    ConnectToServer(tempIpAddress);
                    connected = true;
                }

            }

            _serverIPAddress = serverIP;
        }

        /// <summary>
        /// Returns the string needed to connect to a specific remoting port and objectUri running on the server
        /// </summary>
        /// <param name="port">The port number</param>
        /// <param name="objectUri">The service name</param>
        /// <returns>Connection string in the form "tcp://ServerIPAddress:port/objectUri"</returns>
        public static string BuildServerRemotingString(int port, string objectUri)
        {
            return "tcp://" + ServerIPAddress + ":" + port + "/" + objectUri;
        }
        #endregion Public Methods


        #region Private Methods

        /// <summary>
        /// Connects to a given IPEndPoint
        /// Doesn't allow failed connections
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <returns></returns>
        private static bool ConnectToServerAsync(System.Net.IPEndPoint ipEndPoint)
        {
            m_client = new TcpClient();

            bool connectionSuceeded = false;

            //sets up an asyncronos connection
            IAsyncResult ar = m_client.BeginConnect(ipEndPoint.Address, ipEndPoint.Port, null, null);
            System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
            try
            {
                //blocks the current thread until either timeout is reached or connection is successful
                if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    m_client.Close();
                    System.Diagnostics.Debug.WriteLine("The server is not running");

                    return (connectionSuceeded = false);
                }

                m_client.EndConnect(ar);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                wh.Close();
            }

            connectionSuceeded = true;

            // create a thread to handle data received from Server
            Thread HandleDataFromServerThread = new Thread(new ParameterizedThreadStart(HandleDataFromServer));
            HandleDataFromServerThread.Name = "Handle Data From Server Thread";
            HandleDataFromServerThread.IsBackground = true;
            HandleDataFromServerThread.Start(m_client);

            return (connectionSuceeded);
        }

        /// <summary>
        /// Connects to a given IPEndPoint
        /// Does allow failed connections
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <returns></returns>
        private static bool ConnectToServer(System.Net.IPEndPoint ipEndPoint)
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
        private static string LoadDefaultIP()
        {
            if (Properties.Settings.Default.FirstRunAfterUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.FirstRunAfterUpgrade = false;
                try
                {
                    Properties.Settings.Default.Save();
                }
                catch (System.Configuration.ConfigurationException ex)
                {
                    System.Diagnostics.Debug.WriteLine("No user config file exists: Exception message: " + ex.Message);
                }
            }
            _defaultIP = Properties.Settings.Default.DefaultIP;
            return _defaultIP;
        }
        private static void SaveNewDefaultIP(string defaultIP)
        {
            _defaultIP = defaultIP;
            Properties.Settings.Default.DefaultIP = _defaultIP;
            Properties.Settings.Default.Save();
        }
        private static string DefaultIP
        {
            get { return _defaultIP; }
            set
            {

                SaveNewDefaultIP(value);
            }
        }
        /// <summary>
        /// Finds the StoMoHab Server by listening out for its UDP Multicast message.
        /// If it can't find the server it gives the user to option to try and find the server again
        /// or to manually enter the IP address.
        /// 
        /// The servers multicast message has a TTL of 0 so it will only function on the LAN and will not
        /// extend past a router, this is where the manual IP entry option is useful. If needed change the
        /// server code to so that its TTL is >0 so that the message can be recived outside of the LAN.
        /// </summary>
        /// <returns>The servers IP Address</returns>
        private static string FindServerIPAddress()
        {

            bool foundServer = false;
            bool timeOut = false;
            const int receiveTimeout = 3000; // Time to wait until giving up (ms)
            string ip = null; // Place to store the server ip once its been found
            _defaultIP = LoadDefaultIP(); // The default IP to fill the input box when the server can't be found automatically


            // set up the socket and ip end point
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            EndPoint ep = (EndPoint)iep;
            sock.Bind(iep);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("224.100.0.1")));
            sock.ReceiveTimeout = receiveTimeout;

            // prepare the data structures
            byte[] data = new byte[1024];
            int recv = 0;

            // While the server isn't found
            while (!foundServer)
            {
                timeOut = false;
                try
                {
                    // try and find the data
                    recv = sock.ReceiveFrom(data, ref ep);
                }
                catch (SocketException se)
                {
                    if (se.ErrorCode == 10060) // Connection Timed out
                    {
                        Console.WriteLine("Time out");
                        timeOut = true;
                        if (_firstFailedFindAttempt && FindServer.IsValidIP(_defaultIP)) // on the first fail try the defaul IP if its valid
                        {
                            ip = _defaultIP;
                            foundServer = true;
                            _firstFailedFindAttempt = false;
                        }
                        else // after that fails wait for input
                        {
                            //present the user with a gui to help resolve the problem and wait
                            FindServer findservergui = new FindServer(_defaultIP);
                            DialogResult result = findservergui.ShowDialog();

                            if (result == DialogResult.OK) // Manually entered a valid ip string
                            {
                                ip = findservergui.ServerIP;
                                foundServer = true;
                                DefaultIP = ip; // save it as the new default
                            }
                            if (result == DialogResult.Cancel) // Chose to exit
                            {
                                //in debug mode don't exit just return null to allow a failed connect
#if !DEBUG
                                Environment.Exit(0);
#endif
#if DEBUG
                                return null;
#endif

                            }
                            // otherwise they wanted to retry
                            findservergui.Dispose();
                        }
                    }
                }
                if (!timeOut) //if all went well and there wasn't a time out then process the udp packet
                {
                    string stringData = Encoding.ASCII.GetString(data, 0, recv);
                    if (stringData.CompareTo("StroMoHab Server") == 0)
                        foundServer = true; // If the packet came from the server then you have found it (very very unlikely it wouldn't have)

                    // Split the ip data from ip:port to just get ip
                    char[] splitChar = { ':' };
                    string[] ipdata = ep.ToString().Split(splitChar);
                    ip = ipdata[0];
                }
            }

            sock.Close(); // All done close the socket

            if (foundServer)
            {
                Console.WriteLine("Found Server at " + ip);
                return ip;
            }
            else // Shouldn't ever get here
                return null;
        }



        private static void HandleDataFromServer(object client)
        {
            TcpClient serverConnection = (TcpClient)client;


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
                        case 'M': // Marker

                            Marker receivedMarker = new Marker();
                            receivedMarker.MarkerId = binaryReader.ReadInt32();

                            //if have been sent End of Frame indication
                            if (receivedMarker.MarkerId == -2147483648)
                            {
                                receivedMarker.TimeStamp = binaryReader.ReadInt64();
                                readTimestamp = true;
                                MarkerList.RemoveExcessMarkersFromList(receivedMarker.TimeStamp);

                                if (WholeFrameReceivedEvent != null)
                                {
                                    WholeFrameReceivedEvent();
                                }
                            }

                            if (readTimestamp == false)
                            {
                                receivedMarker.TimeStamp = binaryReader.ReadInt64();
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
                        case 'F': // Filtered Marker

                            Marker receivedFilteredMarker = new Marker();
                            receivedFilteredMarker.MarkerId = binaryReader.ReadInt32();

                            //if have been sent End of Frame indication
                            if (receivedFilteredMarker.MarkerId == -2147483648)
                            {
                                receivedFilteredMarker.TimeStamp = binaryReader.ReadInt64();
                                readTimestamp = true;
                                FilteredMarkerList.RemoveExcessMarkersFromList(receivedFilteredMarker.TimeStamp);

                                if (FilteredMarkerListReceivedEvent != null)
                                {
                                    FilteredMarkerListReceivedEvent();
                                }
                            }

                            if (readTimestamp == false)
                            {
                                receivedFilteredMarker.TimeStamp = binaryReader.ReadInt64();
                            }

                            receivedFilteredMarker.xCoordinate = binaryReader.ReadDouble();
                            receivedFilteredMarker.yCoordinate = binaryReader.ReadDouble();
                            receivedFilteredMarker.zCoordinate = binaryReader.ReadDouble();

                            bytesRead = 36;

                            if (receivedFilteredMarker.MarkerId != -2147483648)
                            {
                                FilteredMarkerList.AddMarker(receivedFilteredMarker);
                            }

                            break;
                        case 'R': // Trackable

                            Trackable receivedTrackable = new Trackable();
                            receivedTrackable.ID = binaryReader.ReadInt32();

                            receivedTrackable.TimeStamp = binaryReader.ReadInt64();

                            if (receivedTrackable.ID == -2147483648)
                            {
                                if (receivedTrackable.TimeStamp == 0)
                                    trackableList.Clear();
                                //End of List
                                if (TrackableListReceivedEvent != null)
                                    TrackableListReceivedEvent(trackableList);
                                //Read in any remaining data out of the buffer
                                receivedTrackable.TrackableIndex = binaryReader.ReadInt32();
                                receivedTrackable.Name = binaryReader.ReadString();
                                receivedTrackable.xCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.yCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.zCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.Roll = binaryReader.ReadDouble();
                                receivedTrackable.Pitch = binaryReader.ReadDouble();
                                receivedTrackable.Yaw = binaryReader.ReadDouble();

                            }
                            else
                            {
                                if (trackableList.Count != 0) // alread contains data
                                {
                                    //If from a previous time frame, clear the list
                                    if (receivedTrackable.TimeStamp != trackableList[0].TimeStamp)
                                    {
                                        trackableList.Clear();
                                    }
                                }

                                receivedTrackable.TrackableIndex = binaryReader.ReadInt32();
                                receivedTrackable.Name = binaryReader.ReadString();
                                receivedTrackable.xCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.yCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.zCoordinate = binaryReader.ReadInt32();
                                receivedTrackable.Roll = binaryReader.ReadDouble();
                                receivedTrackable.Pitch = binaryReader.ReadDouble();
                                receivedTrackable.Yaw = binaryReader.ReadDouble();
                                trackableList.Add(receivedTrackable);
                            }

                            break;

                        case 'J': // Joint

                            Joint receivedJoint = new Joint();
                            receivedJoint.ID = binaryReader.ReadInt32();

                            receivedJoint.TimeStamp = binaryReader.ReadInt64();

                            if (receivedJoint.ID == -2147483648)
                            {
                                if (receivedJoint.TimeStamp == 0)
                                    jointList.Clear();

                                //End of List
                                if (JointListReceivedEvent != null)
                                    JointListReceivedEvent(jointList);


                                //Read in any remaining data out of the buffer
                                receivedJoint.Exists = binaryReader.ReadBoolean();
                                receivedJoint.Name = binaryReader.ReadString();
                                receivedJoint.Trackable1 = binaryReader.ReadInt32();
                                receivedJoint.Trackable2 = binaryReader.ReadInt32();
                                receivedJoint.RollOffset = binaryReader.ReadDouble();
                                receivedJoint.PitchOffset = binaryReader.ReadDouble();
                                receivedJoint.YawOffset = binaryReader.ReadDouble();
                                receivedJoint.Roll = binaryReader.ReadDouble();
                                receivedJoint.Pitch = binaryReader.ReadDouble();
                                receivedJoint.Yaw = binaryReader.ReadDouble();
                                receivedJoint.xCoordinate = binaryReader.ReadInt32();
                                receivedJoint.yCoordinate = binaryReader.ReadInt32();
                                receivedJoint.zCoordinate = binaryReader.ReadInt32();
                            }
                            else
                            {
                                if (jointList.Count != 0) // alread contains data
                                {
                                    //If from a previous time frame, clear the list
                                    if (receivedJoint.TimeStamp != jointList[0].TimeStamp)
                                    {
                                        jointList.Clear();
                                    }
                                }

                                receivedJoint.Exists = binaryReader.ReadBoolean();
                                receivedJoint.Name = binaryReader.ReadString();
                                receivedJoint.Trackable1 = binaryReader.ReadInt32();
                                receivedJoint.Trackable2 = binaryReader.ReadInt32();
                                receivedJoint.RollOffset = binaryReader.ReadDouble();
                                receivedJoint.PitchOffset = binaryReader.ReadDouble();
                                receivedJoint.YawOffset = binaryReader.ReadDouble();
                                receivedJoint.Roll = binaryReader.ReadDouble();
                                receivedJoint.Pitch = binaryReader.ReadDouble();
                                receivedJoint.Yaw = binaryReader.ReadDouble();
                                receivedJoint.xCoordinate = binaryReader.ReadInt32();
                                receivedJoint.yCoordinate = binaryReader.ReadInt32();
                                receivedJoint.zCoordinate = binaryReader.ReadInt32();
                                jointList.Add(receivedJoint);
                            }

                            break;


                        case 'C': // Camera

                            OptiTrackCamera receivedCamera = new OptiTrackCamera();
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

                        case 'T': // Treadmill (Speed)
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
                        case 'B': //flag type and flag
                            {
                                System.Diagnostics.Debug.WriteLine("MessageType: " + messageType.ToString());

                                char type = binaryReader.ReadChar();

                                System.Diagnostics.Debug.WriteLine("Type: " + type.ToString());

                                bool flag = binaryReader.ReadBoolean();

                                System.Diagnostics.Debug.WriteLine("Flag: " + flag.ToString());

                                bytesRead = 9;

                                switch (type)
                                {
                                    case 'F': //Toggle Feet
                                        {
                                            if (ToggleDrawingOfFeetEvent != null)
                                            {
                                                ToggleDrawingOfFeetEvent(flag);
                                            }
                                            break;
                                        }
                                }
                                break;
                            }

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

            }
        }
        #endregion


    }
}
