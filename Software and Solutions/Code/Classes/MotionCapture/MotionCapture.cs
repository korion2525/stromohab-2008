using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Diagnostics;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.MotionCapture
{
    /// <summary>
    /// Provides Methods to control and interact with the Tracking Tools API
    /// Call Initialize, LoadCalibration, LoadTrackable then Start
    /// OR Call Initialize, LoadCalibration and LoadTrackable, then manage a Timer that calls UpdateCoordinates()
    /// Once that is done, subscribe to the events needed
    /// </summary>
    /// <author>Will Lunniss</author>
    public static class MotionCaptureController
    {
        #region Member Variables
        private static bool apiRunning = false; //Set by Initialize/Shutdown and used by other methods to determin if they can run
        private static List<Trackable> trackableList = new List<Trackable>();
        private static System.Timers.Timer updateTimer = null; //The timer that gets updates from the cameras
        private static bool processMarkers = true;
        private static bool processTrackables = true;
        private static int numberOfCameras = -1;
        private static string connectedCameraDetails = null; // String containg the number of cameras and their serial number if connected

        #endregion Member Variables

        #region Delegates
        /// <summary>
        /// Delegate for MarkerListAvaliable Event
        /// </summary>
        /// <param name="markerList">The Updated Marker List</param>
        /// <param name="timeStamp">The Time that the marker data was collected</param>
        public delegate void MarkerListAvaliableEventHandler(List<Marker> markerList, long timeStamp);

        /// <summary>
        /// Delegate for TrackableListAvaliable Event
        /// </summary>
        /// <param name="trackableList">The Updated Trackable List</param>
        /// <param name="timeStamp">The Time that the trackable data was collected</param>
        public delegate void TrackableListAvaliableEventHandler(List<Trackable> trackableList, long timeStamp);

        #endregion Delegates

        /// <summary>
        /// Method to allow the virtual motion capture controller to inject recorded motion capture data into the motion capture controller
        /// </summary>
        /// <param name="markerList">The marker list</param>
        /// <param name="timeStamp">The time stamp</param>
        internal static void VirtualMotionCaptureControllerCallbackOnMarkerListAvaliable(List<Marker> markerList, long timeStamp)
        {
            OnMarkerListAvaliable(new List<Marker>(markerList), timeStamp);
        }

        /// <summary>
        /// Method to allow the virtual motion capture controller to inject recorded motion capture data into the motion capture controller
        /// </summary>
        /// <param name="trackableList">The trackable list</param>
        /// <param name="timeStamp">The time stamp</param>
        internal static void VirtualMotionCaptureControllerCallbackOnTrackableListAvaliable(List<Trackable> trackableList, long timeStamp)
        {
            OnTrackableListAvaliable(new List<Trackable>(trackableList), timeStamp);
        }


        #region Events
        /// <summary>
        /// MarkerListAvaliable Event
        /// </summary>
        public static event MarkerListAvaliableEventHandler MarkerListAvaliable;

        /// <summary>
        /// TrackableListAvaliable Event
        /// </summary>
        public static event TrackableListAvaliableEventHandler TrackableListAvaliable;



        private static void OnMarkerListAvaliable(List<Marker> markerList, long timeStamp)
        {
            if (MarkerListAvaliable != null)
                MarkerListAvaliable(markerList, timeStamp);
        }


        private static void OnTrackableListAvaliable(List<Trackable> trackableList, long timeStamp)
        {
            if (TrackableListAvaliable != null)
                TrackableListAvaliable(trackableList, timeStamp);
        }

        private static void UpdateTimerEvent(object source, EventArgs e)
        {
            if (APIRunning)
                UpdateCoordinates(processMarkers, processTrackables);
        }

        #endregion Events

        #region Methods

        #region Methods - Initialize/Shutdown/Start/Stop
        /// <summary>
        /// Starts up the Tracking Tools API
        /// </summary>
        public static int Initialize()
        {
            int result = -1;
            // Check that the Tracking Tools API .dll file exists then initialize

            result = TrackingTools.Initialize();
            if (result == 100)// Custom error code for DLL missing - shut down program
            {
                MessageBox.Show("NPTrackingTools.dll is missing", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            else
            {
                apiRunning = true;
                UpdateCameraList();
                if (result != 0)
                {
                    // If the API failed to initialize correctly shutdown the api but keep the program running
                    Shutdown();
                }
            }

            return result;
        }

        /// <summary>
        /// Shuts down the Tracking Tools API - but doesn't shutdown the driver thread
        /// </summary>
        public static void Shutdown()
        {
            apiRunning = false;
            TrackingTools.Shutdown();
        }

        /// <summary>
        /// Shuts down the API and driver threads - the application must now be re-started
        /// </summary>
        public static void FinalShutdown()
        {
            TrackingTools.FinalShutdown();
        }

        /// <summary>
        /// Starts the timer to update camera data every 10 ms
        /// On each update an event will be fired once the data has been processed
        /// </summary>
        /// <returns name="errorCode"></returns>
        public static int Start()
        {
            if (APIRunning)
            {
                if (updateTimer == null)
                {
                    updateTimer = new System.Timers.Timer();
                    updateTimer.Interval = 10;
                    updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimerEvent);
                    updateTimer.Start();
                    bool enabled = updateTimer.Enabled;
                    return 0;
                }
                else
                    return 102;
            }
            else
                return 101;

        }

        /// <summary>
        /// Stops the timer which updates the camera data
        /// </summary>
        public static void Stop()
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
                updateTimer.Dispose();
                updateTimer = null;
            }
        }

        #endregion Methods - Initialize/Shutdown/Start/Stop


        #region Methods - Error Processing

        /// <summary>
        /// Turns an error code of type int into a string containing the error message
        /// </summary>
        /// <param name="errorCode"></param>
        public static string GetErrorMessage(int errorCode)
        {
            string errorMessage = null;
            switch (errorCode)
            {
                case 0: // Method worked
                    errorMessage = "Success";
                    break;
                case 100: // Custom error when .dll is missing
                    errorMessage = "NPTrackingTools.dll is missing";
                    break;
                case 101: // Custom error when API is not running
                    errorMessage = "Tracking Tools API is not running\n";
                    break;
                case 102: //Timer is already running
                    errorMessage = "The update timer has already been started";
                    break;
                case 2: //orveride missing file
                    errorMessage = "File not found - Have you imported the requiered settings?";
                    break;
                case 11: // Overide Invalid licence error and add the list of cameras connected
                    errorMessage = "Invalid Licence - Usually the licenced camera isn't connected\n" + connectedCameraDetails;
                    break;
                default: // Decode error message using Tracking Tools API as its not a custom error code
                    errorMessage = TrackingTools.GetResultString(errorCode);
                    break;
            }
            return errorMessage;
        }

        #endregion Methods - Error Processing


        #region Methods - Load

        /// <summary>
        /// Loads the calibration file defined in CalibrationPath
        /// </summary>
        /// <param name="CalibrationPath">The .cal file Path</param>
        public static int LoadCalibration(string CalibrationPath)
        {
            int result = -1;
            if (APIRunning)
                result = TrackingTools.LoadCalibration(CalibrationPath);
            else
                result = 101; // Custom error code for API not running
            return result;
        }

        /// <summary>
        /// Loads the Trackables Definition file defined in TrackablePath
        /// </summary>
        /// <param name="TrackablePath">The .tra file path</param>
        public static int LoadTrackables(string TrackablePath)
        {
            int result = -1;
            if (APIRunning)
                result = TrackingTools.LoadTrackables(TrackablePath);
            else
                result = 101; // Custom error code for API not running
            return result;
        }

        /// <summary>
        /// Loads the Tracking Tools Project (Calibration + Rigid Bodys etc)
        /// Not very usefull as they can't be transfered between each new development build
        /// </summary>
        /// <param name="ProjectPath"></param>
        public static int LoadProject(string ProjectPath)
        {
            int result = -1;
            if (APIRunning)
                result = TrackingTools.LoadProject(ProjectPath);
            else
                result = 101; // Custom error code for API not running
            return result;
        }


        #endregion Methods - Load


        #region Methods - Update

        /// <summary>
        /// Updates the camera list
        /// </summary>
        public static void UpdateCameraList()
        {
            if (APIRunning)
            {
                numberOfCameras = TrackingTools.CameraCount();
                OptiTrackCamera camera = new OptiTrackCamera();


                connectedCameraDetails = "  Number of Cameras Connected: " + numberOfCameras + "\n";

                double[] tempRotationMatrix = new double[9];
                int j = 0;
                for (int i = 0; i < numberOfCameras; i++)
                {
                    camera.CameraNumber = i;
                    camera.CameraName = TrackingTools.CameraName(i);
                    camera.xCoordinate = TrackingTools.CameraLocationX(i) * 1000;
                    camera.yCoordinate = TrackingTools.CameraLocationY(i) * 1000;
                    camera.zCoordinate = TrackingTools.CameraLocationZ(i) * 1000;

                    connectedCameraDetails += "     Camera : " + camera.CameraName + "\n";

                    j = 0;
                    for (j = 0; j < 9; j++)
                    {
                        tempRotationMatrix[j] = TrackingTools.CameraOrientation(i, j);
                    }
                    camera.RotationMatrix = tempRotationMatrix;
                    OptitrackCameraList.AddCamera(camera);
                }

            }
        }

        /// <summary>
        /// Updates the list of markers with lastest marker coordinates
        /// </summary>
        public static void UpdateCoordinates(bool updateMarker, bool updateTrackable)
        {
            if (APIRunning)
            {
                // Get the timestamp to apply to all markers and trackables
                long timeStamp = System.DateTime.Now.Ticks;
                try
                {
                    if (TrackingTools.UpdateAll() == 0) // If it updates correctly
                    {
                        if (updateMarker)
                        {
                            int markerCount = TrackingTools.FrameMarkerCount();

                            Marker marker = new Marker();
                            for (int i = 0; i < markerCount; i++)
                            {
                                //Get the marker details from TrackingTools
                                marker.MarkerId = i;
                                marker.TimeStamp = timeStamp;
                                marker.xCoordinate = -(int)(TrackingTools.FrameMarkerX(i) * 1000);
                                marker.yCoordinate = (int)(TrackingTools.FrameMarkerY(i) * 1000);
                                marker.zCoordinate = (int)(TrackingTools.FrameMarkerZ(i) * 1000);

                                MarkerList.AddMarker(marker);
                                marker = new Marker();
                            }
                            MarkerList.RemoveExcessMarkersFromList(markerCount);

                            OnMarkerListAvaliable(MarkerList.listOfMarkers, timeStamp);

                        }

                        if (updateTrackable)
                        {
                            int trackableCount = TrackingTools.TrackableCount();
                            trackableList.Clear();
                            Trackable newTrackable = null;

                            // Go through the loaded trackables and if they are in the current frame
                            // add them to the trackableList
                            for (int i = 0; i < trackableCount; i++)
                            {
                                if (TrackingTools.IsTrackableTracked(i))
                                {
                                    newTrackable = TrackingTools.GetTrackableLocation(i);
                                    newTrackable.TimeStamp = timeStamp;
                                    trackableList.Add(newTrackable);
                                }
                            }
                            OnTrackableListAvaliable(trackableList, timeStamp);
                        }
                    }
                }
                catch (AccessViolationException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error With UpdateALL() " + ex + "   Time Stamp : " + timeStamp);

                }
                catch (System.Runtime.InteropServices.SEHException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error With UpdateALL() " + ex + "   Time Stamp : " + timeStamp);

                }
            }
        }

        #endregion Methods - Update


        #region Methods - Get Details
        /// <summary>
        /// Returns a list of OptiTrackCamera containing the full details of the cameras
        /// Use GetConnectedCameraDetails for a simple string containing the number of cameras and their serial numbers
        /// </summary>
        /// <returns></returns>
        public static List<OptiTrackCamera> GetCameraList()
        {
            return OptitrackCameraList.listOfCameras;
        }

        /// <summary>
        /// Returns a list of Markers that are both single markers and part of trackables
        /// </summary>
        /// <returns></returns>
        public static List<Marker> GetMarkerList()
        {
            return MarkerList.listOfMarkers;
        }


        /// <summary>
        /// Returns a list of Trackables
        /// </summary>
        /// <returns></returns>
        public static List<Trackable> GetTrackableList()
        {
            return trackableList;
        }

        /// <summary>
        /// Provides a string containg the number of cameras and their serial numbers
        /// Use GetCameraList for full details of the cameras
        /// </summary>
        public static string GetConnectedCameraDetails()
        {
            return connectedCameraDetails;
        }
        /// <summary>
        /// Returns the number of connected cameras
        /// </summary>
        /// <returns></returns>
        public static int GetConnectedCameraCount()
        {
            return numberOfCameras;
        }

        #endregion Methods - Get Details


        // TODO Sort out calibration!
        //public static void Calibrate()
        //{
        //    if (trackableList.Count == 0)
        //    {
        //        if (MarkerList.listOfMarkers.Count == 2)
        //        {
        //            Marker leftFoot_Marker = new Marker(MarkerList.listOfMarkers[0]);
        //            Marker rightFoot_Marker = new Marker(MarkerList.listOfMarkers[1]);

        //            if (leftFoot_Marker.xCoordinate > rightFoot_Marker.xCoordinate)
        //            {
        //                Marker tempMarker = new Marker(leftFoot_Marker);

        //                leftFoot_Marker = new Marker(rightFoot_Marker);
        //                rightFoot_Marker = new Marker(tempMarker);
        //            }
        //            //m_calibration.Store(leftFoot_Marker, rightFoot_Marker);

        //        }
        //else
        //{
        //    if (MarkerList.listOfMarkers.Count == 0)
        //    {
        //        m_calibration.Reset();
        //    }
        //}
        //    }
        //    if (trackableList.Count == 2)
        //    {
        //        Trackable leftFoot_Trackable = new Trackable(trackableList[0]);
        //        Trackable rightFoot_Trackable = new Trackable(trackableList[1]);

        //        if (leftFoot_Trackable.xCoordinate > rightFoot_Trackable.xCoordinate)
        //        {
        //            Trackable tempTrackable = new Trackable(leftFoot_Trackable);

        //            leftFoot_Trackable = new Trackable(rightFoot_Trackable);
        //            rightFoot_Trackable = new Trackable(tempTrackable);
        //        }

        //        //m_calibration.Store(trackableList[0], trackableList[1]);
        //    }
        //}

        //private static bool m_remotingInitialised = false;
        //public static void InitialiseRemoting()
        //{
        //    if (m_remotingInitialised == false)
        //    {
        //        m_calibration = new Calibration();
        //        RemotingServices.Marshal(m_calibration, "Calibration");
        //        m_remotingInitialised = true;
        //    }
        //}


        #endregion Methods


        #region Properties


        /// <summary>
        /// UpdateTimerRunning - True if the Update Timer is running, this implies that the API is also running correctly
        /// Note this property is read-only
        /// </summary>
        public static bool UpdateTimerRunning
        {
            get
            {
                if (updateTimer != null)
                    return updateTimer.Enabled;
                else
                    return false;
            }
        }

        /// <summary>
        /// APIRunning - True if the API is running, false if not 
        /// Note this property is read-only
        /// </summary>
        public static bool APIRunning
        {
            get
            {
                return apiRunning;
            }
        }
        /// <summary>
        /// ProcessMarkers - Set to true to process markers when an update event occurs
        /// and false not to (defult is true)
        /// </summary>
        public static bool ProcessMarkers
        {
            set
            {
                processMarkers = value;
            }
            get
            {
                return processMarkers;
            }
        }
        /// <summary>
        /// ProcessTrackables - Set to true to process trackables when an update event occurs
        /// and false not to (defult is true)
        /// </summary>
        public static bool ProcessTrackables
        {
            set
            {
                processTrackables = value;
            }
            get
            {
                return processTrackables;
            }
        }


        #endregion Properties

    }
}

