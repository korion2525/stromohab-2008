using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;
using StroMoHab_Remote_DataManager;
using Win32Utilities;

namespace StroMoHab_Client
{
    /// <summary>
    /// This form controls running a session and reviewing a session
    /// It starts and stops playback and recording and sends the task to the visualiser
    /// </summary>
    public partial class Run_Review_Session_Form : Form
    {
        private Session _session;
        private Timer _delayedWindowSetup = new Timer();
        private Timer _waitForVisualiser = new Timer();
        private Timer _guiUpdate = new Timer();
        private StromoLight_RemoteDrawingList.DrawingList m_objectsToDraw = new StromoLight_RemoteDrawingList.DrawingList();
        Patient_Remote_DataManager _remote_DataManager = new Patient_Remote_DataManager();
        public Run_Review_Session_Form()
        {
            InitializeComponent();
            if (TCPProcessor.ConnectedToServer)
                _remote_DataManager = (Patient_Remote_DataManager)Activator.GetObject(typeof(Patient_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005, "PatientRemoteDataManagerConnection"));

            //Register for events from the server
            TCPProcessor.AvatarSpeedChangedEvent += new TCPProcessor.AvatarSpeedChangedHandler(TCPProcessor_AvatarSpeedChangedEvent);
            TCPProcessor.FilteredMarkerListReceivedEvent += new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);
            TCPProcessor.TrackableListReceivedEvent += new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            TCPProcessor.JointListReceivedEvent += new TCPProcessor.JointListReceivedHandler(TCPProcessor_JointListReceivedEvent);


            m_objectsToDraw = (StromoLight_RemoteDrawingList.DrawingList)Activator.GetObject(typeof(StromoLight_RemoteDrawingList.DrawingList), "tcp://localhost:8002/TaskDesignerConnection");

            StroMoHab_Task_Designer.Externals.LoadVisualiser(m_objectsToDraw);

            _delayedWindowSetup.Interval = 1500;
            _delayedWindowSetup.Tick += new EventHandler(_delayedWindowSetup_Tick);
            _delayedWindowSetup.Start();
            this.TopMost = true;

            _waitForVisualiser.Interval = 1000;
            _waitForVisualiser.Start();
            _waitForVisualiser.Tick += new EventHandler(_waitForVisualiser_Tick);

            _guiUpdate.Interval = 100;
            _guiUpdate.Tick += new EventHandler(_guiUpdate_Tick);
            _guiUpdate.Start();
        }


        #region Event Handlers
        void TCPProcessor_AvatarSpeedChangedEvent(float newSpeed)
        {
            TreadmillSpeed = newSpeed;
        }
        //TODO Update event handlers
        //Add code to do something with incoming data
        void TCPProcessor_JointListReceivedEvent(List<Joint> jointList)
        {
            if (jointList.Count > 0)
            {
                throw new NotImplementedException();
            }
        }

        void TCPProcessor_TrackableListReceivedEvent(List<Trackable> trackableList)
        {
            if (trackableList.Count > 0)
            {
                throw new NotImplementedException();
            }
        }

        void TCPProcessor_FilteredMarkerListReceivedEvent()
        {
            if (FilteredMarkerList.listOfMarkers.Count > 0)
            {
                //throw new NotImplementedException();
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// The current treadmill speed
        /// </summary>
        private float TreadmillSpeed { get; set; }
        /// <summary>
        /// The current session
        /// </summary>
        public Session CurrentSession
        {
            set
            {
                _session = value;
                if (value.ScheduledSession)
                    SetupScheduledSession();
                else
                    SetupCompletedSession();
            }
            get { return _session; }
        }
        #endregion

        #region GUI
        void _guiUpdate_Tick(object sender, EventArgs e)
        {
            if (_remote_DataManager.PlaybackStatus)
                buttonReview.Text = "Stop";
            else
                buttonReview.Text = "Start";

            if (_remote_DataManager.RecordingStatus)
                buttonRun.Text = "Stop";
            else
                buttonRun.Text = "Start";

            labelSpeed.Text = "Speed : " + TreadmillSpeed + " (mph)";


            //TODO Add real time diagnostic data to interface
            // Insert code to update gui objects here

        }

        /// <summary>
        /// Wait for the visualiser to load and then tell it what task to use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _waitForVisualiser_Tick(object sender, EventArgs e)
        {
            if (m_objectsToDraw.VisualiserLoading)
                return;
            else
            {
                VisualiserLoaded();
                _waitForVisualiser.Stop();
            }
        }

        /// <summary>
        /// Sends the visualiser information once it has loaded
        /// </summary>
        private void VisualiserLoaded()
        {
            m_objectsToDraw.ObjectsToDraw = CurrentSession.Task.ObjectList;
        }

        /// <summary>
        /// Wait and then setup the form size and position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _delayedWindowSetup_Tick(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
            this.Location = Screen.PrimaryScreen.Bounds.Location;

            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;
            this.TopMost = false;
            this.Focus();
            this.BringToFront();
            _delayedWindowSetup.Stop();
        }


        /// <summary>
        /// Sets up the interface for a completed session
        /// </summary>
        private void SetupCompletedSession()
        {
            this.Text = "Review Session";
            labelTitle.Text = "Review Session";
            buttonReview.Visible = true;
            buttonRun.Visible = false;
        }
        /// <summary>
        /// Sets up the interface for a scheduled sessison
        /// </summary>
        private void SetupScheduledSession()
        {
            this.Text = "Run Session";
            labelTitle.Text = "Run Session";
            buttonReview.Visible = false;
            buttonRun.Visible = true;
        }

        #endregion

        #region Playback and Record
        /// <summary>
        /// Stars playback
        /// </summary>
        private void StartPlayback()
        {
            m_objectsToDraw.ObjectsToDraw = CurrentSession.Task.ObjectList;
            _remote_DataManager.ClientStartPlayback();

        }
        /// <summary>
        /// Stops playback
        /// </summary>
        private void StopPlayback()
        {
            _remote_DataManager.ClientStopPlayback();

        }
        /// <summary>
        /// Starts recording
        /// </summary>
        private void StartRecording()
        {
            m_objectsToDraw.ObjectsToDraw = CurrentSession.Task.ObjectList;
            _remote_DataManager.ClientStartRecord();
        }
        /// <summary>
        /// Stops recording
        /// </summary>
        private void StopRecording()
        {
            if (TreadmillSpeed == 0)
            {
                _remote_DataManager.ClientStopRecord();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("The treadmill is still moving. Please stop the treadmill first", "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Actions
        private void buttonReview_Click(object sender, EventArgs e)
        {
            if (_remote_DataManager.PlaybackStatus)
                StopPlayback();
            else
                StartPlayback();
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (_remote_DataManager.RecordingStatus)
                StopRecording();
            else
                StartRecording();
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (_remote_DataManager.RecordingStatus == false && _remote_DataManager.PlaybackStatus == false)
                this.DialogResult = DialogResult.OK;
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        /// <summary>
        /// Switches in and out of full screen mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSwitchVMode_Click(object sender, EventArgs e)
        {
            if (m_objectsToDraw.FullScreen)
            {
                buttonSwitchVMode.Text = "Full Screen";
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
                this.Location = Screen.PrimaryScreen.Bounds.Location;
            }
            else
            {
                buttonSwitchVMode.Text = "Normal";
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                this.Location = Screen.PrimaryScreen.Bounds.Location;
            }
            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            Win32.SetForegroundWindow(visualiserWindowHandle);
            SendKeys.SendWait("{F1}");
        }

        #endregion



    }
}
