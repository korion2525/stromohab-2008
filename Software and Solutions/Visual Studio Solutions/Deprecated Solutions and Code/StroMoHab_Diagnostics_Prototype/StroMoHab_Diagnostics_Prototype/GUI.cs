using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Runtime.Remoting;  
using System.Runtime.Remoting.Channels;  
using System.Runtime.Remoting.Channels.Tcp;

namespace StroMoHab_Diagnostics_Prototype
{
    public partial class GUI : Form
    {
        #region Member Variables
        //To add more trackables or joints, simply increase the MAX_Trackable_Number or MAX_Joint_Number
        //and add the extra strings the the TRACKABLE_NAMES or JOINT_NAMES arrays in order of ID number
        private const int MAX_Trackable_Number = 4;
        private const int MAX_Joint_Number = 2;
        private string[] TRACKABLE_NAMES = new string[MAX_Trackable_Number] {"Left Foot",
            "Right Foot","Left Lower Leg","Right Lower Leg"};
        private string[] JOINT_NAMES = new string[MAX_Joint_Number] { "Left Ankle", "Right Ankle" };

        private bool[] showTrackable = new bool[MAX_Trackable_Number];
        private bool[] showJoint = new bool[MAX_Joint_Number];
        //Offsets are stored in an array
        //          [ID,pitchOffset]
        //          [ID,yawOffset]
        //          [ID,rollOffset]
        private double[,] trackableOffset = new double[MAX_Trackable_Number, 3];
        private double[,] jointOffset = new double[MAX_Joint_Number, 3];

        private List<Stromohab_MCE.Trackable> trackableList = new List<Stromohab_MCE.Trackable>();
        private List<Stromohab_MCE.Joint> jointList = new List<Stromohab_MCE.Joint>();

        private bool connected = false;

        private System.Windows.Forms.Timer guiUpdateTimer = new System.Windows.Forms.Timer();
        private System.Diagnostics.Process openGLApp = null;
        OpenGLViewer.OpenGLViewerRemoteControl openGLViewerRemoteControl = null;

        #endregion Member Variables


        #region Constructor
        public GUI()
        {
            InitializeComponent();
            

            #region Timers
            // Manages gui updates
            guiUpdateTimer.Interval = 100;
            guiUpdateTimer.Tick += new EventHandler(guiUpdateTimer_Tick);
            guiUpdateTimer.Start();

            #endregion Timers

            #region OpenGL
            //Start OpenGLViewer, listen for it to exit and setup remoting
            openGLApp = new System.Diagnostics.Process();
            openGLApp.StartInfo.FileName = "OpenGLViewer.exe";
            openGLApp.EnableRaisingEvents = true;
            openGLApp.Start();
            openGLApp.Exited += new EventHandler(openGLApp_Exited);

            openGLViewerRemoteControl = (OpenGLViewer.OpenGLViewerRemoteControl)Activator.GetObject(typeof(OpenGLViewer.OpenGLViewerRemoteControl), "tcp://127.0.0.1:8008/Remote");
              

            #endregion OpenGL 

            //Build the GUI for the trackables and joints definied in the member variables
            BuildGUI();

            //connect to the server
            connect_Click(null, EventArgs.Empty);
        }
        #endregion Constructor


        #region Build GUI
        private void BuildGUI()
        {
            
            checkedListBoxTrackables.ItemCheck += new ItemCheckEventHandler(checkedListBoxTrackables_ItemCheck);
            for (int i = 0; i < MAX_Trackable_Number; i++)
            {
                GroupBox bodyPart = new GroupBox();
                bodyPart.Size = new System.Drawing.Size(140,120);
                bodyPart.Text = TRACKABLE_NAMES[i];
                bodyPart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Label data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(6, 22);
                data.Text = "Pitch :";
                bodyPart.Controls.Add(data);
                data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(9, 55);
                data.Text = "Yaw :";
                bodyPart.Controls.Add(data);
                data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(12, 88);
                data.Text += "Roll :";
                bodyPart.Controls.Add(data);
                flowLayoutPanelDisplayTrackables.Controls.Add(bodyPart);
                checkedListBoxTrackables.Items.Add(TRACKABLE_NAMES[i], true);
            }




            checkedListBoxJoints.ItemCheck += new ItemCheckEventHandler(checkedListBoxJoints_ItemCheck);
            for (int i = 0; i < MAX_Joint_Number; i++)
            {
                GroupBox joint = new GroupBox();
                joint.Size = new System.Drawing.Size(190, 120);
                joint.Text = JOINT_NAMES[i];
                joint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Label data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(6, 22);
                data.Text = "Dorsal Flexion :";
                joint.Controls.Add(data);
                data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(73, 55);
                data.Text = "Yaw :";
                joint.Controls.Add(data);
                data = new Label();
                data.AutoSize = true;
                data.Location = new System.Drawing.Point(76, 88);
                data.Text += "Roll :";
                joint.Controls.Add(data);
                flowLayoutPanelDisplayJoints.Controls.Add(joint);
                checkedListBoxJoints.Items.Add(JOINT_NAMES[i], true);
            }



        }



        #endregion Build GUI


        #region Update GUI

        private void UpdateGUI()
        {
            bool[] trackedTrackable = new bool[MAX_Trackable_Number];
            bool[] trackedJoint = new bool[MAX_Joint_Number];

            foreach (Stromohab_MCE.Trackable trackable in trackableList)
            {
                flowLayoutPanelDisplayTrackables.Controls[trackable.ID - 1].Controls[0].Text = "Pitch : " + Math.Round(trackable.Pitch - trackableOffset[trackable.ID - 1, 0], 1).ToString();
                flowLayoutPanelDisplayTrackables.Controls[trackable.ID - 1].Controls[1].Text = "Yaw : " + Math.Round(trackable.Yaw - trackableOffset[trackable.ID -1,1],1).ToString();
                flowLayoutPanelDisplayTrackables.Controls[trackable.ID - 1].Controls[2].Text = "Roll : " + Math.Round(trackable.Roll - trackableOffset[trackable.ID - 1, 2], 1).ToString();
                trackedTrackable[trackable.ID - 1] = true;
            }

            for (int i = 0; i < MAX_Trackable_Number; i++)
            {

                if (trackedTrackable[i])
                    flowLayoutPanelDisplayTrackables.Controls[i].ForeColor = Color.DarkGreen;
                else
                    flowLayoutPanelDisplayTrackables.Controls[i].ForeColor = Color.Red;
                if (showTrackable[i])
                    flowLayoutPanelDisplayTrackables.Controls[i].Visible = true;
                else
                    flowLayoutPanelDisplayTrackables.Controls[i].Visible = false;

            }

            foreach (Stromohab_MCE.Joint joint in jointList)
            {
                flowLayoutPanelDisplayJoints.Controls[joint.ID - 1].Controls[0].Text = "Dorsal Flexion : " + Math.Round(joint.Pitch - jointOffset[joint.ID - 1, 0], 1).ToString();
                flowLayoutPanelDisplayJoints.Controls[joint.ID - 1].Controls[1].Text = "Yaw : " + Math.Round(joint.Yaw - jointOffset[joint.ID - 1,1],1).ToString();
                flowLayoutPanelDisplayJoints.Controls[joint.ID - 1].Controls[2].Text = "Roll : " + Math.Round(joint.Roll - jointOffset[joint.ID - 1, 2],1).ToString();
                trackedJoint[joint.ID - 1] = true;
            }

            for (int i = 0; i < MAX_Joint_Number; i++)
            {

                if (trackedJoint[i])
                    flowLayoutPanelDisplayJoints.Controls[i].ForeColor = Color.DarkGreen;
                else
                    flowLayoutPanelDisplayJoints.Controls[i].ForeColor = Color.Red;
                if (showJoint[i])
                    flowLayoutPanelDisplayJoints.Controls[i].Visible = true;
                else
                    flowLayoutPanelDisplayJoints.Controls[i].Visible = false;

            }
        }

        #endregion Update GUI


        #region Button Actions

        private void buttonCalibrate_Click(object sender, EventArgs e)
        {
            foreach (Stromohab_MCE.Trackable trackable in trackableList)
            {
                trackableOffset[trackable.ID - 1, 0] = Math.Round(trackable.Pitch, 0);
                trackableOffset[trackable.ID - 1, 1] = Math.Round(trackable.Yaw,0);
                trackableOffset[trackable.ID - 1, 2] = Math.Round(trackable.Roll,0);
            }
            foreach (Stromohab_MCE.Joint joint in jointList)
            {

                jointOffset[joint.ID - 1, 0] = Math.Round(joint.Pitch - joint.PitchOffset, 0);
                jointOffset[joint.ID - 1, 1] = Math.Round(joint.Yaw - joint.YawOffset, 0);
                jointOffset[joint.ID - 1, 2] = Math.Round(joint.Roll - joint.RollOffset, 0);

            }
            openGLViewerRemoteControl.UpdateTrackableOffset(trackableOffset);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            trackableOffset = new double[MAX_Trackable_Number, 3];
            jointOffset = new double[MAX_Joint_Number, 3];
            openGLViewerRemoteControl.UpdateTrackableOffset(trackableOffset);
        }

        private void connect_Click(object sender, EventArgs e)
        {

            if (!connected)
            {
                System.Net.IPEndPoint tempIpAddress = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8001);
                connected = Stromohab_MCE_Connection.TCPProcessor.ConnectToServer(tempIpAddress);
                if (connected)
                {
                    Stromohab_MCE_Connection.SendMCECommand.StartCameras();
                    Stromohab_MCE_Connection.TCPProcessor.JointListReceivedEvent += new Stromohab_MCE_Connection.TCPProcessor.JointListReceivedHandler(TCPProcessor_JointListReceivedEvent);
                    Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent += new Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
                    
                }
                else
                    MessageBox.Show("Failed To Connect");
                
            }
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MAX_Trackable_Number; i++)
            {

                checkedListBoxTrackables.SetItemChecked(i, true);
            }
            for (int i = 0; i < MAX_Joint_Number; i++)
            {

                checkedListBoxJoints.SetItemChecked(i, true);
            }
        }

        private void buttonShowTracked_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MAX_Trackable_Number; i++)
            {
                if(flowLayoutPanelDisplayTrackables.Controls[i].ForeColor == Color.DarkGreen)
                    checkedListBoxTrackables.SetItemChecked(i, true);
                else
                    checkedListBoxTrackables.SetItemChecked(i, false);
            }
            for (int i = 0; i < MAX_Joint_Number; i++)
            {
                if (flowLayoutPanelDisplayJoints.Controls[i].ForeColor == Color.DarkGreen)
                    checkedListBoxJoints.SetItemChecked(i, true);
                else
                    checkedListBoxJoints.SetItemChecked(i, false);
            }
        }
        #endregion Button Actions


        #region Events

        void checkedListBoxJoints_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue != CheckState.Checked)
                showJoint[e.Index] = true;
            else
                showJoint[e.Index] = false;
            
        }

        void checkedListBoxTrackables_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.CurrentValue != CheckState.Checked)
                showTrackable[e.Index] = true;
            else
                showTrackable[e.Index] = false;
            openGLViewerRemoteControl.SetTrackableIDsToDisplay(e.Index + 1, showTrackable[e.Index]);
        }

        void guiUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        void openGLApp_Exited(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

      
        void TCPProcessor_TrackableListReceivedEvent(List<Stromohab_MCE.Trackable> newTrackableList)
        {
            trackableList = new List<Stromohab_MCE.Trackable>(newTrackableList);
        }

        void TCPProcessor_JointListReceivedEvent(List<Stromohab_MCE.Joint> newJointList)
        {
            jointList = new List<Stromohab_MCE.Joint>(newJointList);
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            openGLApp.Kill();
        }
        private void GUI_Resize(object sender, EventArgs e)
        {
            tabControl.Width = this.Width;
            tabControl.Height = this.Height - 20;
        }

       

        #endregion Events



        

      

       

       

        

     
    }
}
