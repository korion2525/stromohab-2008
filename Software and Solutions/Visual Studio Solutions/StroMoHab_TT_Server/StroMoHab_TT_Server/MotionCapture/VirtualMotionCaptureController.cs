using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_TT_Server.Treadmill;
using StroMoHab_Objects.Objects;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using StroMoHab_TT_Server.DataStorage;

namespace StroMoHab_TT_Server.MotionCapture
{
    /// <summary>
    /// Controlls recording and playing back motion capture data
    /// 
    /// Record mode works by listening for Treadmill/Marker/Trackable events and saving them
    /// Playback work by injecting the data into callback methods inside MotionCapture which then send out the data
    /// as if it had just been collected from the API
    /// 
    /// 
    /// 
    /// IMPORTANT: When calling OpenMotionCaptureSubSession with a DateTime - you must check that it is valid and that
    /// the data file is stored on disk in the correct location. To do this use the following code: 
    /// <code>
    ///     if(System.IO.File.Exists(VirtualMotionCaptureController.BuildSubSessionFileName(dateTime)))
    ///     {
    ///         VirtualMotionCaptureController.OpenMotionCaptureSubSession(dateTime);
    ///     }
    ///     else
    ///     {
    ///         ...
    ///         do something when file doesn't exist
    ///         ...
    ///     }
    /// </code>
    /// 
    /// Call OpenMotionCaptureSubSession(VALID DateTime) and then MotionCaptureDataPlayback(true) to playback
    /// as well as setting Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback = true
    /// 
    /// or
    /// 
    /// MotionCaptureDataRecord(true) to record as well as setting
    /// Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback = false;
    /// 
    /// 
    /// CommandParser must be informed of the current mode to ensure that the update timer gets MC data from the correct
    /// motion capture controler (MotionCapture or VirtualMotionCaptureController)
    /// 
    /// </summary>
    static class VirtualMotionCaptureController
    {
        //TODO Need to keep track of visualiser position and save it into frame data so that we can jump around in the session

        #region Member Variables
        /// <summary>
        /// Represents where or not a recording session is currently running
        /// </summary>
        private static bool _recordingSessionStarted = false;
        /// <summary>
        /// Stores the current marker list
        /// </summary>
        private static List<Marker> _currentMarkerList = null;
        /// <summary>
        /// Stores the current trackable list
        /// </summary>
        private static List<Trackable> _currentTrackableList = null;
        /// <summary>
        /// Stores the time stamp for the current marker list
        /// </summary>
        private static long _currentMarkerTimeStamp = -1;
        /// <summary>
        /// Stores the time stamp for the current trackable list
        /// </summary>
        private static long _currentTrackableTimeStamp = -1;
        /// <summary>
        /// Stores the current sub session
        /// </summary>
        private static VirtualMotionCaptureSubSession _currentSubSession;
        /// <summary>
        /// Stores the current frame
        /// </summary>
        private static VirtualMotionCaptureFrame _currentFrame;
        /// <summary>
        /// Stores the current sub session start time
        /// </summary>
        private static DateTime _subSessionStartTime;
        /// <summary>
        /// Stores the current sub session end time
        /// </summary>
        private static DateTime _subSessionEndTime;
        private static string DATA_DIR = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Data\\";
        /// <summary>
        /// stores the speed before playback was paused
        /// </summary>
        private static float _lastSpeed = -1f;

        #endregion

        #region Delegates
        public delegate void VirtualMotionCaptureSubSessionRecordedEventHandler(DateTime subSessionStartTime, DateTime subSessionEndTime);

        private static void OnVirtualMotionCaptureSubSessionRecorded()
        {
            if (VirtualMotionCaptureSubSessionRecorded != null)
                VirtualMotionCaptureSubSessionRecorded(_subSessionStartTime, _subSessionEndTime);
        }

        public delegate void VirtualMotionCaptureSubSessionPlaybackEndedEventHandler(DateTime subSessionStartTime);

        private static void OnVirtualMotionCaptureSubSessionPlaybackEnded()
        {
            if (VirtualMotionCaptureSubSessionPlaybackEnded != null)
                VirtualMotionCaptureSubSessionPlaybackEnded(_subSessionStartTime);
        }

        #endregion

        #region Events

        public static event VirtualMotionCaptureSubSessionPlaybackEndedEventHandler VirtualMotionCaptureSubSessionPlaybackEnded;

        public static event VirtualMotionCaptureSubSessionRecordedEventHandler VirtualMotionCaptureSubSessionRecorded;

        #endregion

        #region Methods - Public
        /// <summary>
        /// Turns on and off recording motion capture data
        /// </summary>
        /// <param name="record"></param>
        public static void MotionCaptureDataRecord(bool record)
        {

            if (record)
            {
                MotionCaptureDataPlayback(false);
                TreadmillController.TransmitSpeedEvent += new TreadmillController.TransmitSpeedEventHandler(TreadmillController_TransmitSpeedEvent);

                //TODO: Allow recording without the treadmill                
                /* 
                 * Copy the code in TreadmillController_TransmitSpeedEvent into here and use an if(RecordWithoutTreadmill) to switch between the 
                 * two options
                 * 
                 * you should then have code that registers for the marker and trackable lists and calls  RecordedSubSessionStart(newSpeed);
                 * 
                 * 
                 * */
            }
            else
            {
                MotionCaptureController.MarkerListAvaliable -= new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCaptureController_MarkerListAvaliable);
                MotionCaptureController.TrackableListAvaliable -= new MotionCaptureController.TrackableListAvaliableEventHandler(MotionCaptureController_TrackableListAvaliable);
                TreadmillController.TransmitSpeedEvent -= new TreadmillController.TransmitSpeedEventHandler(TreadmillController_TransmitSpeedEvent);
                /*
                 * In here you will need to also call RecordedSessionEnd in a similar if(RecordWithoutTreadmill) statement
                 * then in the code one level above this class (PatientDataManager) you need to use the DateTime sessionStartTime variable to later on
                 * open the session for playback
                 */
            }

        }


        /// <summary>
        /// Turns on or off playing back motion capture data
        /// You must call OpenMotionCaptureSubSession first
        /// </summary>
        /// <param name="playback"></param>
        public static void MotionCaptureDataPlayback(bool playback)
        {
            if (playback)
            {
                MotionCaptureDataRecord(false);
                if (_lastSpeed != -1f)
                    TreadmillController.SetSpeed(_lastSpeed);
            }
            else
            {
                _lastSpeed = TreadmillController.GetSpeed(); // save the current speed
            }
        }

        /// <summary>
        /// Opens a sub session using its DateTime value.
        /// </summary>
        /// <param name="subSession"></param>
        public static void OpenMotionCaptureSubSession(DateTime subSession)
        {
            _lastSpeed = -1f; // reset the last speed
            OpenSubSession(subSession);
        }


        /// <summary>
        /// Sends the virtual motion capture data to the motion capture controller
        /// and sends out any treadmill speed updates
        /// </summary>
        public static void UpdateCoordinates()
        {
            // Get the next frame from the database
            bool status = GetNextFrame();
            if (status == false)
            {
                OnVirtualMotionCaptureSubSessionPlaybackEnded();
                return;
            }
            else
            {

                // Check to see if the treadmill speed changed this session, if so send it out
                float _treadmillSpeed = 0f;
                if (TreadmillSpeedChanged(out _treadmillSpeed))
                    TreadmillController.SetSpeed(_treadmillSpeed);

                // Send out the motion capture data
                MotionCaptureController.VirtualMotionCaptureControllerCallbackOnMarkerListAvaliable(_currentFrame.MarkerList, _currentFrame.TimeStamp);
                MotionCaptureController.VirtualMotionCaptureControllerCallbackOnTrackableListAvaliable(_currentFrame.TrackableList, _currentFrame.TimeStamp);
            }
        }
        #endregion

        #region Methods - Private

        #region Playback
        /// <summary>
        /// Gets the next frame from the database
        /// </summary>
        /// <returns></returns>
        private static bool GetNextFrame()
        {
            _currentFrame = _currentSubSession.GetNextFrame();
            if (_currentFrame == null)
            {
                _currentSubSession.ResetSubSession();
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Checks the database to see if the treadmill speed changed this frame
        /// </summary>
        /// <param name="newSpeed"></param>
        /// <returns></returns>
        private static bool TreadmillSpeedChanged(out float newSpeed)
        {
            if (_currentFrame.TreadmillSpeed != -1f)
            {
                newSpeed = _currentFrame.TreadmillSpeed; //Check database of a speed change event if there is one get it and return true
                return true;
            }
            else
            {
                newSpeed = 0f;
                return false;
            }

        }
        #endregion

        #region Record

        /// <summary>
        /// Starts recording a new sub session
        /// </summary>
        /// <param name="_treadmillSpeed">The starting speed</param>
        private static void RecordedSubSessionStart(float _treadmillSpeed)
        {
            _subSessionStartTime = DateTime.Now;
            _currentSubSession = new VirtualMotionCaptureSubSession();
            _currentFrame = new VirtualMotionCaptureFrame();
            SaveTreadmillSpeedChange(_treadmillSpeed);
        }

        /// <summary>
        /// Saves a treadmill speed change
        /// </summary>
        /// <param name="newSpeed"></param>
        private static void SaveTreadmillSpeedChange(float newSpeed)
        {
            _currentFrame.TreadmillSpeed = newSpeed;
        }

        /// <summary>
        /// Saves one frame of data
        /// </summary>
        private static void SaveFrame()
        {
            _currentFrame.MarkerList = new List<Marker>(_currentMarkerList);
            _currentFrame.TrackableList = new List<Trackable>(_currentTrackableList);
            _currentFrame.TimeStamp = _currentTrackableTimeStamp;
            _currentSubSession.AddFrame(_currentFrame);
            _currentFrame = new VirtualMotionCaptureFrame();
        }

        /// <summary>
        /// Ends recording the current session
        /// </summary>
        private static void RecordedSessionEnd()
        {
            MotionCaptureController.MarkerListAvaliable -= new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCaptureController_MarkerListAvaliable);
            MotionCaptureController.TrackableListAvaliable -= new MotionCaptureController.TrackableListAvaliableEventHandler(MotionCaptureController_TrackableListAvaliable);

            SaveTreadmillSpeedChange(0f);
            _currentFrame.MarkerList = new List<Marker>();
            _currentFrame.TrackableList = new List<Trackable>();
            _currentSubSession.AddFrame(_currentFrame);

            _subSessionEndTime = DateTime.Now;
            SaveSubSession(_subSessionStartTime);
        }
        #endregion

        #region Database Save and Load
        /// <summary>
        /// Saves the current sub session to the database
        /// </summary>
        private static void SaveSubSession(DateTime subSessionStartTime)
        {
            bool saveSucceeded = false;
            string fileName = BuildSubSessionFileName(subSessionStartTime);

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, _currentSubSession);
                fs.Close();
                saveSucceeded = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Save Unsuccesful. System Message: " + ex.Message);
            }
            finally
            {
                if (saveSucceeded)
                {
                    //System.Windows.Forms.MessageBox.Show("Session Saved", "Save Successful");

                    OnVirtualMotionCaptureSubSessionRecorded();
                }
            }
        }

        /// <summary>
        /// Opens a sub session from the database
        /// </summary>
        private static void OpenSubSession(DateTime subSessionStartTime)
        {


            string fileName = BuildSubSessionFileName(subSessionStartTime);
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();

                _currentSubSession = (VirtualMotionCaptureSubSession)bFormatter.Deserialize(fs);
                _subSessionStartTime = subSessionStartTime;


            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Session file not found. Exception message: " + ex.Message, "Default Task File Not Found");
                _currentSubSession = null;
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Session file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                _currentSubSession = null;
            }
            catch (System.IO.IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error opening session file. Exception message: " + ex.Message, "Unanticipated Error Opening Default Task File");
                _currentSubSession = null;
            }

        }

        /// <summary>
        /// Turns the session start time into the file name used to store the data
        /// </summary>
        /// <param name="sessionStartTime">Session start time</param>
        /// <returns>File name</returns>
        public static string BuildSubSessionFileName(DateTime sessionStartTime)
        {
            return DATA_DIR + sessionStartTime.Year + sessionStartTime.Month + sessionStartTime.Day + "_" + sessionStartTime.Hour + sessionStartTime.Minute + sessionStartTime.Second + sessionStartTime.Millisecond + ".smhs";
        }

        #endregion

        #endregion

        #region Methods - Event Actions
        /// <summary>
        /// Responds to the updated treadmill speed
        /// </summary>
        /// <param name="newSpeed"></param>
        static void TreadmillController_TransmitSpeedEvent(float newSpeed)
        {
            if (newSpeed == 0f) // Treadmill has stopped/session has stopped
            {
                if (_recordingSessionStarted) // If running a session, stop it
                    RecordedSessionEnd();

                _recordingSessionStarted = false;

            }
            else if (newSpeed > 0.1f) // Tradmill is moving / session is running
            {
                if (_recordingSessionStarted == false) // If not running a session, start one
                {
                    MotionCaptureController.MarkerListAvaliable += new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCaptureController_MarkerListAvaliable);
                    MotionCaptureController.TrackableListAvaliable += new MotionCaptureController.TrackableListAvaliableEventHandler(MotionCaptureController_TrackableListAvaliable);

                    RecordedSubSessionStart(newSpeed);
                }
                else
                    SaveTreadmillSpeedChange(newSpeed); // Otherwise just store the new speed in the current frame

                _recordingSessionStarted = true;
            }

        }

        /// <summary>
        /// Saves the new trackable data
        /// </summary>
        /// <param name="trackableList"></param>
        /// <param name="timeStamp"></param>
        static void MotionCaptureController_TrackableListAvaliable(List<Trackable> trackableList, long timeStamp)
        {
            _currentTrackableList = new List<Trackable>(trackableList);
            _currentTrackableTimeStamp = timeStamp;

            if (_currentMarkerTimeStamp == _currentTrackableTimeStamp)
                SaveFrame();
        }

        /// <summary>
        /// Saves the new marker data
        /// </summary>
        /// <param name="markerList"></param>
        /// <param name="timeStamp"></param>
        static void MotionCaptureController_MarkerListAvaliable(List<Marker> markerList, long timeStamp)
        {
            //Deep copy the list
            _currentMarkerList = new List<Marker>();
            foreach (Marker m in markerList)
            {
                Marker newM = new Marker(m);
                _currentMarkerList.Add(newM);
            }
            _currentMarkerTimeStamp = timeStamp;

            if (_currentMarkerTimeStamp == _currentTrackableTimeStamp)
                SaveFrame();
        }

        #endregion

    }
}
