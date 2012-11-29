using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace Stromohab_MCE
{
    /// <summary>
    /// Main application form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Member Variables
            OptitrackCommandParser_Server commandParser = new OptitrackCommandParser_Server();

        #endregion
        #region Delegate Declarations
            delegate void UpdateFormCamerasStartedCallback(string text, bool enable);
            delegate void UpdateStatusBarServerServerStartedCallback(int portNumber);
            delegate void UpdateStatusBarCallback(string text);
        #endregion Delegate Declarations

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        //name thread (useful for debug)
            Thread.CurrentThread.Name = "Main Form Thread";
        //load camera calibration profile
            MotionCapture.LoadProfile("C:\\Users\\mag501\\Documents\\SVN_WorkingCopy\\Stromohab\\stromohab_08\\OptiTrack_Calibrations\\CalibrationFile_020609.cal");
        //register custom events
            OptitrackCommandParser_Server.camerasStartedEvent += new OptitrackCommandParser_Server.CamerasStartedHandler(commandParser_camerasStartedEvent);
            TCPServer.ServerListeningStartedEvent +=new TCPServer.ServerListeningStartedHandler(TCPServer_ServerListeningStartedEvent);
            TCPServer.ClientConnectionAcceptedEvent += new TCPServer.ClientConnectionAcceptedHandler(TCPServer_ClientConnectionAcceptedEvent);
        //start listening for incoming connections            
            TCPServer tcpServer = new TCPServer();

        //enumerate COM ports
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBoxTreadmillPort.Items.Add(portName);
            }
            comboBoxTreadmillPort.Text = (string)comboBoxTreadmillPort.Items[comboBoxTreadmillPort.Items.Count-1];

            TreadmillController.SetSerialPortName(comboBoxTreadmillPort.Text);

        }



        #region Events
            /// <summary>
            /// Triggered when the cameras are started
            /// </summary>
            void commandParser_camerasStartedEvent()
            {
                UpdateStatusBar("Cameras Started");
                this.timerPollCameraInterface.Enabled = true;
            }

            void TCPServer_ServerListeningStartedEvent(int port)
            {
                UpdateStatusBar("Listening for connections on port: " + port.ToString());
            }

            ///// <summary>
        ///// Use this to ensure access to form controls is thread safe.
        ///// Sets statusBar1.Text and timerPollCameraInterface.Enabled.
        ///// </summary>
        ///// <param name="text"></param>
        ///// <param name="enable"></param>
        //private void UpdateFormCamerasStarted(string text,bool enable)
        //{
        //    if (this.statusBar1.InvokeRequired)
        //    {
        //        UpdateFormCamerasStartedCallback d = new UpdateFormCamerasStartedCallback(UpdateFormCamerasStarted);
        //        this.Invoke(d, new object[] { text,enable });
        //    }
        //    else
        //    {
        //    //put required actions here
        //        this.statusBar1.Text = text;
        //        timerPollCameraInterface.Enabled = enable;
        //    }
        //}

        //private void UpdateStatusBarServerStarted(int portNumber)
        //{
        //    if (this.statusBar1.InvokeRequired)
        //    {
        //        UpdateStatusBarServerServerStartedCallback d = new UpdateStatusBarServerServerStartedCallback(UpdateStatusBarServerStarted);
        //        this.Invoke(d, new object[] { portNumber });
        //    }
        //    else
        //    {
        //        //put required actions here
        //        this.statusBar1.Text = ("Waiting for Connection on Port: " + portNumber.ToString());
        //    }
        //}
        
            void TCPServer_ClientConnectionAcceptedEvent(System.Net.EndPoint clientConnection)
            {
                UpdateStatusBar("Connected to client: " + clientConnection.ToString());
            }
        #endregion Events

        /// <summary>
        /// Thread safe update of the status bar with supplied string
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusBar(string text)
        {
            if (this.statusBar1.InvokeRequired)
            {
                UpdateStatusBarCallback d = new UpdateStatusBarCallback(UpdateStatusBar);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                //put required actions here
                this.statusBar1.Text = text;
                
                if (text.Equals("Cameras Started"))
                {
                    this.timerPollCameraInterface.Enabled = true;
                }

            }
        }

        /// <summary>
        /// Shows the "Open File" dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenCalibrationFile_Click(object sender, EventArgs e)
        {
        //show "Open File" dialog
            openFileDialogCalibrationFile.ShowDialog();
        }

        /// <summary>
        /// Triggered when user presses OK in "Open File" dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialogCalibrationFile_FileOk(object sender, CancelEventArgs e)
        {
        //set the filename
            textBoxCalibrationFilePath.Text = openFileDialogCalibrationFile.FileName;
        //load the selected profile
            MotionCapture.LoadProfile(textBoxCalibrationFilePath.Text);
        }

        /// <summary>
        /// Polling timer on form - used to fetch coordinates from motion capture interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPollCameraInterface_Tick(object sender, EventArgs e)
        {
            MotionCapture.UpdateCoordinates();

            if (MarkerList.listOfMarkers.Count != 0)
            {
                statusBar1.Text = ("Cameras Running     x[0]: " + MarkerList.listOfMarkers[0].xCoordinate.ToString("N2") + " Number of markers detected: " + MarkerList.listOfMarkers.Count.ToString());
            }
        }

        /// <summary>
        /// Starts and stops the cameras server-side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCameraControl_Click(object sender, EventArgs e)
        {
            if (buttonCameraControl.Text == "Start Cameras")
            {
                statusBar1.Text = "Starting Cameras";
                MotionCapture.LoadProfile(textBoxCalibrationFilePath.Text);
                MotionCapture.StartCameras();
                statusBar1.Text = "Cameras Started";
                timerPollCameraInterface.Enabled = true;
                buttonCameraControl.Text = "Stop Cameras";
            }
            else
            {
                timerPollCameraInterface.Enabled = false;
                MotionCapture.StopCameras();
                statusBar1.Text = "Cameras Stopped";
                buttonCameraControl.Text = "Start Cameras";
            }

        }

        private void buttonDisconnectAll_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdateCameraList_Click(object sender, EventArgs e)
        {
            MotionCapture.UpdateCameraList();
            OptitrackCameraList.TransmitListOfCameras();
        }

        private void btnTreadmillControl_Click(object sender, EventArgs e)
        {
            if (btnTreadmillControl.Text == "Start Treadmill")
            {
                TreadmillController.SetSerialPortName(comboBoxTreadmillPort.Text);
                TreadmillController.SetSpeed(2.0f);
                btnTreadmillControl.Text = "Stop Treadmill";
            }
            else if (btnTreadmillControl.Text == "Stop Treadmill")
            {
                TreadmillController.SetSpeed(0.0f);
                btnTreadmillControl.Text = "Start Treadmill";
            }
        }

        private void btnConnectToTreadmill_Click(object sender, EventArgs e)
        {
            TreadmillController.SetSerialPortName(comboBoxTreadmillPort.Text);
        }

    }
}
