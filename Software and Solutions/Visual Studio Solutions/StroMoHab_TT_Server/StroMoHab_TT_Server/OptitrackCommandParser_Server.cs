using System;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using StroMoHab_Objects.Objects;
using StroMoHab_TT_Server.Treadmill;
using StroMoHab_TT_Server.DataProcessing;
using StroMoHab_TT_Server.MotionCapture;

namespace StroMoHab_TT_Server.Communication
{
    /// <summary>
    /// OptitrackCommandParser_Server is responsable for sending commands to MotionCapture
    /// It does so using the handleCommand method which is called by the TCPServer with new commands to issue
    /// Aditionally it has a timer which calls MotionCapture.UpdateCoordinates every 10 ms again through handleCommand
    /// The reson for this is handleCommand locks itself while executing preventing more than one command at once
    /// to be passed to MotionCapture. This is required because Natural Points API is unmanaged
    /// </summary>
    public class OptitrackCommandParser_Server
    {
        #region Member Variables
        private System.Timers.Timer updateTimer = null; //The timer that gets updates from the cameras
        private string msg = null; //statup message log
        private string SETTINGSFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Settings\\";
        private static bool _virtualMCPlayback = false;
        private bool _viewingMsg = false;
        #endregion Member Variables


        #region Delegate Declarations
        /// <summary>
        /// Handles event triggered when cameras are started.
        /// </summary>
        public delegate void CamerasStartedHandler();

        /// <summary>
        /// Handles event triggered when toggle feet commmand is received.
        /// </summary>
        public delegate void ToggleFeetCommandRecievedHandler(bool display);

        #endregion Delegate Declarations


        #region Event Declarations
        /// <summary>
        /// Triggered when cameras are started
        /// </summary>
        public static event CamerasStartedHandler camerasStartedEvent;
        public static event ToggleFeetCommandRecievedHandler ToggleFeetCommandReceivedEvent; 
        #endregion Event Declarations


        #region Motion Capture Methods

        /// <summary>
        /// updateTimer event. Calls handleCommand with the correct inputs so that it updates the coordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void updateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            handleCommand(new byte[4] {0,0,6,0}); // Call handleCommand with command number 6
        }

        /// <summary>
        /// Shows a message box when there is a problem
        /// </summary>
        public void ShowMessageBox()
        {
            
             _viewingMsg = true;
            DialogResult result = StroMoHab_Objects.Forms.TopMostMessageBox.Show("An error occured while starting. Here is the message log :\n\n" + msg + "\n\nPlease fix the problem(s) and press \"Restart the Motion Capture Engine\" to try again", "Motion Caputure Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            _viewingMsg = false;
        }

        #region Start/Stop

        /// <summary>
        /// Starts the Motion Capture Engine
        /// </summary>
        public void MotionCaptureStart()
        {
            bool errors = false;
            int result = -1;
            string filepath = null;

            msg = null;

            filepath = SETTINGSFOLDER;

            if(MotionCaptureController.APIRunning == false) // Only start motion capture if it isn't already running
            {
                result = MotionCaptureController.Initialize();
                msg += "Initializing Tracking Tools Motion Caputre API :\n" + MotionCaptureController.GetErrorMessage(result);
                if (result != 0)
                    errors = true;

                result = MotionCaptureController.LoadCalibration(filepath + "cali.cal");
                msg += "\nLoading Calibration File \"cali.cal\" :\n" + MotionCaptureController.GetErrorMessage(result);
                if (result != 0)
                    errors = true;

                result = MotionCaptureController.LoadTrackables(filepath + "Trackables.tra");
                msg += "\nLoading Trackable File \"Trackables.tra\" :\n" + MotionCaptureController.GetErrorMessage(result);
                if (result != 0)
                    errors = true;

                result = DataProcessing.JointProcessor.LoadJointDefinition(filepath + "joints.xml");
                if (result == 0)
                    msg += "\nLoading Joint File \"joints.xml\" :\nSuccess";
                else
                {
                    errors = true;
                    msg += "\nLoading Joint File \"joints.xml\" :\nFailed";
                }

                if (errors)
                {
                    Thread messageBoxThread = new Thread(new ThreadStart(ShowMessageBox));
                    if(_viewingMsg == false)
                        messageBoxThread.Start();
                    MotionCaptureStop(); // Falied to start correctly so shut it down
                }
                else
                {
                    msg = null; // Clear the log as there weren't any problems
                    StartUpdateTimer();
                    if (camerasStartedEvent != null)
                        camerasStartedEvent();

                }
            }

        }
        

        /// <summary>
        /// Stops the Motion Capture Engine
        /// </summary>
        public void MotionCaptureStop()
        {
            StopUpdateTimer();
            MotionCaptureController.Shutdown();
        }

        /// <summary>
        /// Shuts down the api and drivers so that the program can exit
        /// </summary>
        public void FinalShutdown()
        {
            MotionCaptureController.FinalShutdown();
        }

        #endregion Start/Stop

        #region Update Timer

        /// <summary>
        /// Starts the timer to update camera data every 10 ms
        /// On each update an event will be fired once the data has been processed
        /// </summary>
        /// <returns name="errorCode"></returns>
        public int StartUpdateTimer()
        {
            if (MotionCaptureController.APIRunning)
            {
                if (updateTimer == null)
                {
                    updateTimer = new System.Timers.Timer();
                    updateTimer.Interval = 10;
                    updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(updateTimer_Elapsed);
                    updateTimer.Start();
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
        public void StopUpdateTimer()
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
                updateTimer.Dispose();
                updateTimer = null;
            }
        }
        #endregion Update Timer

        #endregion Motion Capture Methods


        #region Handle Command
        /// <summary>
        /// Converts byte to command and then executes command
        /// </summary>
        /// <param name="receivedCommand"></param>
        public void handleCommand(byte[] receivedCommand)
        {
            lock (this) // Lock the code
            {
                int command = (int)receivedCommand[2];
                switch (command)
                {
                    // 0 is Start Cameras
                    case 0:
                       MotionCaptureStart();
                        break;
                    // 1 is Stop Cameras
                    case 1: 
                        MotionCaptureStop();
                        break;
                    // 2 is Send Camera Coordinates
                    case 2:
                        if (MotionCaptureController.APIRunning)
                        {
                            MotionCaptureController.UpdateCameraList();
                            OptitrackCameraList.TransmitListOfCameras();
                        }
                        break;
                    // 3 is Start Treadmill At Fixed Speed
                    case 3:
                        {
                            TreadmillController.SetSpeed((float)BitConverter.ToDouble(receivedCommand, 4));
                            break;
                        }
                    // 4 is Stop Treadmill
                    case 4:
                         {
                            TreadmillController.SetSpeed(0.0f);
                            break;
                        }
                    case 6:
                        {
                            if (VirtualMotionCaputrePlayback)
                            {
                                VirtualMotionCaptureController.UpdateCoordinates();
                            }
                            else
                            {
                                MotionCaptureController.UpdateCoordinates(true, true);
                            }
                            break;
                        }
                    // 7 is Calibrate ground plane.
                    case 7:
                        {
                        //TODO: Reinstate or remove this?
                            //MotionCapture.Calibrate();
                            break;
                        }
                        //8 is toggle displaying feet
                    case 8:
                        {
                            if (ToggleFeetCommandReceivedEvent != null)
                            {
                                ToggleFeetCommandReceivedEvent(BitConverter.ToBoolean(receivedCommand,4));
                            }
                            break;
                        }
                    case 9: // Shuts down the drivers so the application can close
                        {
                            FinalShutdown();
                            break;
                        }
                    case 10: // Clears the CoR data - This should be called at the start of each session
                        {
                            JointProcessor.ResetCoRCalculations();
                            break;
                        }
                    case 11: // Requests an update for the current speed
                        {
                            TreadmillController.TransmitSpeed();
                            break;
                        }
                    default:
                        {
                            System.Diagnostics.Debug.WriteLine("Invalid command received: " + command.ToString());
                            break;
                        }
                }
            }


        }
        #endregion Handle Command


        #region Properties
        /// <summary>
        /// A log detailing any problems with startup
        /// </summary>
        public string StartupLog
        {
            get { return msg; }
        }
        /// <summary>
        /// When set to TRUE - VirtualMotionCaptureController.UpdateCoordinates will be called (Playback mode)
        /// When set to FALSE - MotionCapture.UpdateCoordinates will be called (Live or Record mode)
        /// </summary>
        public static bool VirtualMotionCaputrePlayback
        {
            set { _virtualMCPlayback = value; }
            get { return _virtualMCPlayback; }
        }
        #endregion Properties

    }
}
