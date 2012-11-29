using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Net;
using StroMoHab_TT_Server.Communication;
using StroMoHab_Objects.Objects;
using StroMoHab_TT_Server.MotionCapture;
using StroMoHab_TT_Server.Treadmill;
using Ionic.Zip;
using Ionic.Zlib;

namespace StroMoHab_TT_Server.Forms
{
    /// <summary>
    /// GUI
    /// </summary>
    public partial class GUI : Form
    {
        private TCPServer tcpServer = new TCPServer();
        private bool motionCaptureStatus = false;
        private Timer guiUpdateTimer = new Timer();
        private Timer guiHider = new Timer();
        private string treadmillStatus = "Not Detected";
        private string treadmillSpeed = "0";
        private string _settingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Settings\\";
        private string _dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Data\\";


        /// <summary>
        /// GUI
        /// </summary>
        public GUI()
        {
            InitializeComponent();

            //Looks to see if the application has been upgraded (version number change) and if it has
            // gets a copy of the settings from the previous verision
            if (StroMoHab_TT_Server.Properties.Settings.Default.FirstRunAfterUpgrade)
            {
                StroMoHab_TT_Server.Properties.Settings.Default.Upgrade();
                StroMoHab_TT_Server.Properties.Settings.Default.FirstRunAfterUpgrade = false;
                //try
                //{
                    StroMoHab_TT_Server.Properties.Settings.Default.Save();
                //}
                //catch (System.Configuration.ConfigurationException ex)
                //{
                //    System.Diagnostics.Debug.WriteLine("No user config file exists: Exception message: " + ex.Message);
                //}
            }


            //GUI Update Timer
            guiUpdateTimer.Interval = 500;
            guiUpdateTimer.Tick += new EventHandler(UpdateGUIStatus);
            guiUpdateTimer.Start();

            //GUI Hider Timer - hides the gui after 10 seconds
            guiHider.Interval = 10000;
            guiHider.Tick += new EventHandler(guiHider_Tick);
            guiHider.Start();

            //Start and stop the server to get the camera count
            tcpServer.Start();
            tcpServer.Stop();

            //Setup the treadmill
            DetectTreadmill();
            /*
            //Get Connected Camera Count via WMI
            if ((usbCameras = StroMoHab_Utilites.WMIDevices.UpdateCameraList().Count) != 6)
                StroMoHab_Utilites.TopMostMessageBox.Show("Error! Only " + usbCameras + " Cameras Are Connected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            */

            MotionCaptureController.MarkerListAvaliable += new MotionCaptureController.MarkerListAvaliableEventHandler(MotionCapture_MarkerListAvaliable);
            TreadmillController.TransmitSpeedEvent += new TreadmillController.TransmitSpeedEventHandler(TreadmillController_TransmitSpeedEvent);

            // Get server version number
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            string version = assemName.Version.ToString();
            labelVersionText.Text = version;
            string ttVersion = "";
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(true);
            foreach (object obj in customAttributes)
            {
                if (obj is CustomAssemblyInfo.AssemblyTrackingToolsVersion)
                {
                    CustomAssemblyInfo.AssemblyTrackingToolsVersion ttAttr = (CustomAssemblyInfo.AssemblyTrackingToolsVersion)obj;
                    ttVersion = ttAttr.TrackingToolsVersion;
                }
            }
            labelTTVersionText.Text = ttVersion;

            if (tcpServer.DetectTreadmillSpeed)
                checkBox1.CheckState = CheckState.Checked;
            else
                checkBox1.CheckState = CheckState.Unchecked;

            #region Find server IP
            // Get host name
            String strHostName = Dns.GetHostName();

            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // Enumerate IP addresses
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && IPAddress.IsLoopback(ipaddress) == false)
                    labelServerIPText.Text = ipaddress.ToString();

            }
            #endregion Find server IP
        }

        void TreadmillController_TransmitSpeedEvent(float newSpeed)
        {
            treadmillSpeed = Math.Round(newSpeed, 1).ToString();
        }

        void MotionCapture_MarkerListAvaliable(List<Marker> markerList, long timeStamp)
        {
            if (MarkerList.listOfMarkers.Count > 0)
            {
                SetMarkerLabel1(MarkerList.listOfMarkers[0].zCoordinate.ToString());
            }
            if (MarkerList.listOfMarkers.Count > 1)
            {
                SetMarkerLabel2(MarkerList.listOfMarkers[1].zCoordinate.ToString());
            }

        }

        delegate void Invoker(string text);
        void SetMarkerLabel1(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Invoker(SetMarkerLabel1), text);
                return;
            }
            label2.Text = text;
        }

        void SetMarkerLabel2(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Invoker(SetMarkerLabel2), text);
                return;
            }
            label3.Text = text;
        }

        #region Treadmill Detection

        /// <summary>
        /// Gets a list of all the avaliable Serial Ports
        /// </summary>
        private void DetectTreadmill()
        {

            //Gets all the serial ports
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBoxCOMPort.Items.Add(portName);
            }

            string serialPort = null;
            if (comboBoxCOMPort.Items.Count > 0) // If there are avaliable serial ports use the last one
            {
                serialPort = (string)comboBoxCOMPort.Items[comboBoxCOMPort.Items.Count - 1];
                comboBoxCOMPort.Enabled = true;
            }
            else // No port - grey out the box and write no ports available in it
            {
                serialPort = "No Ports Available";
                comboBoxCOMPort.Items.Add(serialPort);
                comboBoxCOMPort.Enabled = false;
            }

            // Set the selected index (causes the method comboBoxCOMPort_SelectedIndexChanged() to be called)
            comboBoxCOMPort.SelectedIndex = comboBoxCOMPort.Items.Count - 1;

            treadmillStatus = serialPort;


        }
        #endregion Treadmill Detection


        #region GUI Buttons

        /// <summary>
        /// Changes the selected COM Port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCOMPort.Enabled) //If enabled (it gets dissabled if there are no ports
            {
                TreadmillController.ClearSerialPortName();
                TreadmillController.SetSerialPortName(comboBoxCOMPort.Items[comboBoxCOMPort.SelectedIndex].ToString());
                treadmillStatus = comboBoxCOMPort.Items[comboBoxCOMPort.SelectedIndex].ToString();
            }
        }

        /// <summary>
        /// Redetects avaliable COM ports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRestartTM_Click(object sender, EventArgs e)
        {
            comboBoxCOMPort.Items.Clear();
            TreadmillController.ClearSerialPortName();
            DetectTreadmill();
        }

        /// <summary>
        /// Restarts the Motion Capture API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRestartMC_Click(object sender, EventArgs e)
        {
            tcpServer.Stop();
            StroMoHab_Objects.Forms.TopMostMessageBox.Show("The Motion Capture Engine has been Stopped\nPress OK to Start it again", "StroMoHab Server");
            tcpServer.Start();
        }
        /// <summary>
        /// Turns treadmill speed detection on and off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
                tcpServer.DetectTreadmillSpeed = true;
            else
                tcpServer.DetectTreadmillSpeed = false;
        }
        /// <summary>
        /// Calibrates the treadmill speed detection 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCalibrateTreadmill_Click(object sender, EventArgs e)
        {
            if (motionCaptureStatus == false)
                tcpServer.Start();
            if (motionCaptureStatus)
            {
                checkBox1.CheckState = CheckState.Checked;
                tcpServer.CalibrateTreadmill();
            }
        }
        #endregion GUI Buttons


        #region Timer Events

        /// <summary>
        /// Updates the GUI Status labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateGUIStatus(object sender, EventArgs e)
        {

            motionCaptureStatus = tcpServer.Running();
            if (motionCaptureStatus)
                labelStatusMCText.Text = "Running";
            else
                labelStatusMCText.Text = "Not Running";

            labelStatusCCText.Text = tcpServer.CameraCount().ToString();
            labelStatusTCPText.Text = tcpServer.ConnectionStatus();
            labelStatusVMText.Text = MarkerList.listOfMarkers.Count.ToString();
            labelStatusVTText.Text = tcpServer.TrackableCount.ToString();
            labelStatusTSText.Text = treadmillSpeed;


        }

        /// <summary>
        /// Hides the gui and displays the notification icon BalloonTip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void guiHider_Tick(object sender, EventArgs e)
        {
            if (tcpServer.StartupLog() == null) // If there weren't any error starting up then hide
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                guiUpdateTimer.Stop();
                guiHider.Stop();
                notifyIcon.ShowBalloonTip(2000, "StroMoHab Server", "Connected Cameras : " + tcpServer.CameraCount().ToString() + "\nTreadmill Port : " + treadmillStatus + "\nMotion Capture: " + labelStatusMCText.Text + "\nServer IP : " + labelServerIPText.Text + "\nServer Status : " + tcpServer.ConnectionStatus(), ToolTipIcon.Info);
            }
        }

        #endregion Timer Events


        #region Hide/Show Window
        // Correctly Hides the window from the taskbar when minimized and shows it when visiable
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            guiUpdateTimer.Start();

        }

        private void GUI_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                guiUpdateTimer.Stop();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            guiUpdateTimer.Start();

        }
        #endregion Hide/Show Window


        #region Exit

        //Detect When the program is exiting and hide the notifyIcon, otherwise it sits in the tray until the user
        //puts their mouse over it
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_FormClosing(null, null);
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
            tcpServer.FinalShutdown();
            Environment.Exit(0);
        }


        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUI_FormClosing(null, null);
        }

        #endregion


        #region Import/Export Data and Settings
        private void importCalibrationTrackablesJointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workingDir = Environment.CurrentDirectory;


            OpenFileDialog openSettings = new OpenFileDialog();
            openSettings.Filter = "Calibration (*.cal) Trackables (*.tra) and Joint (joints.xml) Files|*.cal;*.tra;joints.xml";
            openSettings.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openSettings.Multiselect = true;
            if (openSettings.ShowDialog() == DialogResult.OK)
            {
                if (!System.IO.Directory.Exists(_settingsFolder))
                {
                    System.IO.Directory.CreateDirectory(_settingsFolder);
                }

                string msg = "Imported :";
                foreach (string fileName in openSettings.FileNames)
                {
                    try
                    {
                        //copy each file into the data folder at the correct location
                        if (fileName.EndsWith(".cal"))
                        {
                            System.IO.File.Copy(fileName, _settingsFolder + "cali.cal", true);
                        }
                        if (fileName.EndsWith(".tra"))
                        {
                            System.IO.File.Copy(fileName, _settingsFolder + "Trackables.tra", true);
                        }
                        if (fileName.EndsWith(".xml"))
                        {
                            System.IO.File.Copy(fileName, _settingsFolder + "joints.xml", true);
                        }
                        msg = msg + "\n" + fileName;
                    }
                    catch
                    {

                    }

                }
                msg = msg + "\n\nPlease now Restart the Motion Capture Engine";
                MessageBox.Show(msg, "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Environment.CurrentDirectory = workingDir;

            }
        }

        private void exportCalibrationTrackablesJointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workingDir = Environment.CurrentDirectory;

            FolderBrowserDialog exportSettings = new FolderBrowserDialog();
            exportSettings.ShowNewFolderButton = true;
            exportSettings.RootFolder = Environment.SpecialFolder.Desktop;
            string files = "";
            if (exportSettings.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.Copy(_settingsFolder + "cali.cal", exportSettings.SelectedPath + "\\cali.cal", true);
                    files = files + "\nCalibration";
                }
                catch
                {
                    files = files + "\nCalibration - FAILED";
                }
                try
                {
                    System.IO.File.Copy(_settingsFolder + "Trackables.tra", exportSettings.SelectedPath + "\\Trackables.tra", true);
                    files = files + "\nTrackables";
                }
                catch
                {
                    files = files + "\nTrackables - FAILED";
                }
                try
                {
                    System.IO.File.Copy(_settingsFolder + "joints.xml", exportSettings.SelectedPath + "\\joints.xml", true);
                    files = files + "\nJoints";
                }
                catch
                {
                    files = files + "\nJoints - FAILED";
                }

                if (files == "")
                {
                    files = "\nExport failed because there were no settings files to export";
                    MessageBox.Show(files, "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show("The following file(s) have been exported to :\n" + exportSettings.SelectedPath + files, "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Environment.CurrentDirectory = workingDir;
        }


        private void exportTasksPatientsSessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workingDir = Environment.CurrentDirectory;

            FolderBrowserDialog exportData = new FolderBrowserDialog();
            exportData.ShowNewFolderButton = true;
            exportData.RootFolder = Environment.SpecialFolder.Desktop;

            if (exportData.ShowDialog() == DialogResult.OK)
            {
                using (ZipFile zip = new ZipFile())
                {
                    try
                    {
                        zip.AddDirectory(_dataFolder);
                        zip.Save(exportData.SelectedPath + "\\StroMoHab Exported Data.smhd");
                        MessageBox.Show("Data has been exported to :\n" + exportData.SelectedPath + "\\StroMoHab Exported Data.smhd", "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {

                    }

                }
            }
            Environment.CurrentDirectory = workingDir;


        }
        #endregion

        private void importTasksPatientsSessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string workingDir = Environment.CurrentDirectory;

            if (!System.IO.Directory.Exists(_dataFolder))
                System.IO.Directory.CreateDirectory(_dataFolder);
            OpenFileDialog openData = new OpenFileDialog();
            openData.Filter = "StroMoHab Exported Data Files (*.smhd)|*.smhd";
            openData.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openData.Multiselect = true;
            if (openData.ShowDialog() == DialogResult.OK)
            {
                using (ZipFile zip = new ZipFile(openData.FileName))
                {
                    try
                    {
                        zip.ExtractAll(_dataFolder, ExtractExistingFileAction.OverwriteSilently);

                        MessageBox.Show(openData.FileName + " has been imported", "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                    }
                }

            }

            Environment.CurrentDirectory = workingDir;
        }

        private void toolStripMenuItemControlTreadmill_CheckStateChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemControlTreadmill.Checked)
            {
                buttonCalibrateTreadmill.Visible = false;
                checkBox1.Visible = false;
                label1.Visible = true;
                buttonRestartTM.Visible = true;
                comboBoxCOMPort.Visible = true;

            }
            else
            {
                buttonCalibrateTreadmill.Visible = true;
                checkBox1.Checked = false;
                checkBox1.Visible = true;
                label1.Visible = false;
                buttonRestartTM.Visible = false;
                comboBoxCOMPort.Visible = false;
            }
        }






    }
}
