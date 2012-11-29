using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Messaging;
using StromoLight_Diagnostics;
using System.Runtime.Remoting;
using ALManagedStaticClass;
using System.IO;
using System.Timers;
using StroMoHab_Objects.Communication;


namespace StromoLight_Diagnostics
{
    
    public partial class frmUI : Form
    {
        TightropeData dataCollection;
        MarkerDataCollection allPositionData;
        ALSoundEnvironment sound;
        //Calibration markerHeightCal;
        TestSubject person = null;
        List<PersonData> personDataList;
        Form_LoadData form_LoadData;
        delegate void updateGUICallback();
        string newID;
        int startTime = 0;
        int testlength = 60000;
        private System.Timers.Timer guiUpdate;
        public frmUI()
        {
           
            InitializeComponent();
            btnStartTreadmill.Enabled = false;
            this.Text = "StromoLight Diagnostics";
            this.Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, this.Height);
            SendStartupNotification();
            nudSpeed.Value = 1.2M;
            guiUpdate = new System.Timers.Timer(10);
            guiUpdate.Elapsed += new System.Timers.ElapsedEventHandler(guiTimer_Elapsed);
            
        }

        #region Network connection and startup etc
        private void SendStartupNotification()
        {
            MessageQueue startupQueue = null;
            string startupQueueName = @".\Private$\StromoLight_Startup";

            if (MessageQueue.Exists(startupQueueName))
            {
                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");
                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = "DSOK";
                message.Label = "Message from Diagnostics";
                startupQueue.Send(message);
            }
            else
            {
                startupQueue = MessageQueue.Create(@".\Private$\StromoLight_Startup");

                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");

                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = "DSOK Started OK";
                message.Label = "Message from Diagnostics";
                startupQueue.Send(message);
            }
        }

        private void ConnectAndStartSession()
        {
            TCPProcessor.ManagedConnectToServer();
            SendMCECommand.StartCameras();

            //btnStartTreadmill.Enabled = true;

            nudSpeed.Enabled = true;


        }
        #endregion


        public int StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private void guiTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateGUI();
        }

        private void updateGUI()
        {
            if (txtSSSI.InvokeRequired)
            {
                updateGUICallback d = new updateGUICallback(updateGUI);
                Invoke(d, new object[] { });
            }
            else
            {
                try
                {
                    if (dataCollection.SSSI1.Count > 0) txtSSSI.Text = dataCollection.SSSI1[dataCollection.SSSI1.Count - 1].ToString();
                    if (dataCollection.CDSI1.Count > 0) txtCDSI.Text = dataCollection.CDSI1[dataCollection.CDSI1.Count - 1].ToString();
                    if (dataCollection.DSSI1.Count > 0) txtDSSI.Text = dataCollection.DSSI1[dataCollection.DSSI1.Count - 1].ToString();
                    if (dataCollection.SISI1.Count > 0) txtSISI.Text = dataCollection.SISI1[dataCollection.SISI1.Count - 1].ToString();
                    txtStride.Text = dataCollection.Stridelength.ToString();
                }
                catch (ArgumentOutOfRangeException) { }
            }

        }

        private void frmUI_Load(object sender, EventArgs e)
        {
            btnStartTreadmill.Enabled = false;
            nudSpeed.Enabled = false;
            chkLink.Checked = true;
            timer1.Enabled = false;
            
            personDataList = new List<PersonData>();
            btnSavePersonData.Text = "New test subject";
            btnSavePersonData.Enabled = false;
            ConnectAndStartSession();
            FolderSetup();
            LoadPersonData();
            sound = new ALSoundEnvironment();
        }

        private void FolderSetup()
        {
            foreach (string test in cbTestID.Items)
            {
                string path = lblPath.Text + test;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private void btnStartTreadmill_Click(object sender, EventArgs e)
        {
            StartAll();
        }

        private bool RepeatedTest()
        {
            bool duplicateTest = false;
            foreach (string file in person.PersonData.FileList)
            {
                string[] split = file.Split(new Char[] { '_','\\' });
                if (split[0] == cbTestID.Text)
                {
                    DialogResult result = MessageBox.Show("This test has already been done. Continue?", "Repeat test?", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2 );
                    if (result == DialogResult.No)
                    {
                        duplicateTest = true;
                    }
                    break;//to prevent multiple dialogue boxes
                }
            }
            return duplicateTest;
        }

        private void StartAll()
        {
            if (btnStartTreadmill.Text == "Start task")
            {
                if (!RepeatedTest())
                {
                    txtInitials.Enabled = false;
                    if (chkLink.Checked == true)
                    {
                        if (chkDelayStart.Checked == true)
                        {
                            timer1.Interval = 6000;
                            timer1.Enabled = true;
                        }
                        else
                        {
                            StartData();
                        }
                    }
                }
            }
            else
            {
                StopData();
            }
        }

        private void StartData()
        {
            //if (CalibrateFeet() == true)//check that no exception occurred during feet calibtation
            {
                btnStartTreadmill.Text = "Stop task";
                string fileID = fbdPath.SelectedPath + @"\" + txtFileName.Text;
                if (person.Left != null)
                {
                    startTime = person.Left.CurrentMarker.TimeStamp;
                    dataCollection = new TightropeData(person, fileID, startTime);
                    allPositionData = new MarkerDataCollection(fileID);

                    //wait here for WFRE/Frontfoot to be not null
                    if (cbTestID.Text == "Sound")
                    {
                        sound.Left = person.Left;
                        sound.Right = person.Right;
                        if (cbDiscrete.Checked)
                        {
                            sound.FootDown = true;
                            sound.DiscreteFootDown_On();
                        }
                        else
                        {
                            sound.FootDown = false;
                        }
                        if (cbFrequency.Checked)
                        {
                            sound.StartSound();
                        }
                    }
                    person.NewTestFrameReceiveEvent();
                    SendMCECommand.SetTreadmillSpeed((float)nudSpeed.Value);
                    timer1.Interval = testlength;
                    timer1.Enabled = true;
                    guiUpdate.Start();
                }
            }
        }

        private void StopData()
        {
            SendMCECommand.SetTreadmillSpeed(0.0F);
            timer1.Enabled = false;
            guiUpdate.Stop();
            dataCollection.DataEventCleanUp();
            allPositionData.MarkerDataCollectionCleanUp();
            person.StopTestFrameReceiveEvent();
            //person.Right.FootCleanUp();
            //person.Left.FootCleanUp();
            //person.Left = null;
            //person.Right = null;
            System.Diagnostics.Debug.WriteLine("End of session.");
            if (cbTestID.Text == "Sound")
            {
                sound.StopUpdate = true;
                if (cbDiscrete.Checked) sound.DiscreteFootDown_Off();
                sound.FootDown = false;
                sound.StopPlaying();
                //sound.EndCurrentEnvironment();
            }
            btnStartTreadmill.Text = "Start task";
            if (!Directory.Exists(txtFileName.Text)) FolderSetup();
            dataCollection.ToFile();
            dataCollection.WriteRawFeetData();
            allPositionData.FileMarkerListPositionData();
            allPositionData.FileMarkerListPositionData_CSV();
            //dataCollection.WriteAccuracyData();
            startTime = 0;
            Foot.Frontfoot = null;
            
            person.PersonData.AddFileName(txtFileName.Text);
            lbFileNames.Items.Add(txtFileName.Text);
            int tmp = int.Parse(txtTestNumber.Text);
            tmp++;
            txtTestNumber.Text = tmp.ToString();
            txtFileName.Text = cbTestID.Text + @"\" + cbTestID.Text + "_" + txtTestNumber.Text + "_" + person.PersonData.PersonID;
            btnSavePersonData.Enabled = true;
        }


        private void nudSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (btnStartTreadmill.Text == "Stop task")
            {
                SendMCECommand.SetTreadmillSpeed((float)nudSpeed.Value);
            }
        }

        private void chkLink_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLink.Checked == true)
            {
                chkDelayStart.Enabled = true;
            }
            else
            {
                chkDelayStart.Enabled = false;
            }
        }

        private void frmUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //personDataList.Add(person.SerialData);//this saves the last person into the list ie. others are saved on entry of new person
            SavePersonData(); //save/serialize personDataList
            sound.EndCurrentEnvironment();
            SendMCECommand.StopTreadmill();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (timer1.Interval == 6000)
            {
                StartData();
            }
            else
            {
                if (timer1.Interval == testlength)
                {
                    timer1.Enabled = false;
                    timer1.Interval = 6000;
                    StopData();
                }
            }
        }


        //private bool CalibrateFeet()
        //{
            //bool successfulCalibration = false;
            //try
            //{
            //    SendMCECommand.Calibrate();
            //    if ((markerHeightCal.LeftFoot_Marker != null)&&(markerHeightCal.RightFoot_Marker != null))
            //    {
            //        person.Left.CalibrateFoot(markerHeightCal.LeftFoot_Marker);
            //        person.Right.CalibrateFoot(markerHeightCal.RightFoot_Marker);
            //        successfulCalibration = true;
            //    }
            //    else if ((markerHeightCal.LeftFoot_Trackable != null)&&(markerHeightCal.LeftFoot_Trackable != null))
            //    {
            //        person.Left.CalibrateFoot(markerHeightCal.LeftFoot_Trackable);
            //        person.Right.CalibrateFoot(markerHeightCal.RightFoot_Trackable);
            //        successfulCalibration = true;
            //    }
            //}
            //catch (NullReferenceException)
            //{
            //    MessageBox.Show("Please check person on treadmill with markers attached and they have been entered as a participant.");
            //    return false;
            //}
            //return successfulCalibration;
        //}

        private void btnFolder_Click(object sender, EventArgs e)
        {
            fbdPath.ShowDialog();
            lblPath.Text = fbdPath.SelectedPath + @"\";
        }

        private uint DeDuplicate(string subjectID)
        {
            uint dedupe = 0;
            bool unique;
            do
            {
                unique = true;
                foreach (PersonData subject in personDataList)//does the ID already exist
                {
                    string ID = subjectID + dedupe;
                    if (subject.PersonID == ID) //if so, deduplicate
                    {
                        unique = false;
                        dedupe++;
                        break;
                    }
                }
            } while (!unique);
            return dedupe;

        }

        /// <summary>
        /// Load in personData from file for the start of a new session
        /// </summary>
        private void LoadPersonData()
        {
            //List<PersonData> List = new List<PersonData>();

            FileStream fileStream = new FileStream(lblPath.Text + "PersonData", FileMode.OpenOrCreate);
            if (fileStream.Length > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                personDataList = (List<PersonData>)formatter.Deserialize(fileStream);
            }
            fileStream.Close();
        }

        private void SavePersonData()
        {
            FileStream filestream = new FileStream(lblPath.Text + "PersonData", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(filestream, personDataList);
            filestream.Close();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            //option to remove last added in case of mistake eg hitting 'New' button to soon
            //could add amend button?
            form_LoadData = new Form_LoadData();
            form_LoadData.Show();
        }


        private void txtInitials_TextChanged(object sender, EventArgs e)
        {
            if (txtInitials.Text != "")
            {
                uint dedupe = DeDuplicate(txtInitials.Text);
                newID = txtInitials.Text + dedupe;
                txtFileName.Text = cbTestID.Text + @"\" + cbTestID.Text + "_" + txtTestNumber.Text + "_" + newID;
                //btnStartTreadmill.Enabled = true;
                btnSavePersonData.Enabled = true;
            }
            else
            {
                //btnStartTreadmill.Enabled = false;
                btnSavePersonData.Enabled = false;
            }
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            if (btnSavePersonData.Text == "Save record")
            {
                person.PersonData.Age = (uint)nudAge.Value;
                if (rbF.Checked) person.PersonData.Gender = "F";
                else person.PersonData.Gender = "M";
                if (rbLeft.Checked)
                {
                    person.PersonData.FootPreference = "L";
                }
                else
                {
                    if (rbRight.Checked)
                    {
                        person.PersonData.FootPreference = "R";
                    }
                    else
                    {
                        person.PersonData.FootPreference = "N";
                    }
                }
                person.PersonData.AddPersonNotes(txtNotes.Text);
                personDataList.Add(person.PersonData);
                //person.Left.FootCleanUp();
                //person.Right.FootCleanUp();
                Foot.Frontfoot = null;
                person.Left = null;
                person.Right = null;
                person.PersonData = null;
                person = null;
                btnStartTreadmill.Enabled = false;
                txtTestNumber.Text = "0";
                btnSavePersonData.Enabled = false;
                txtNotes.Text = "";
                txtFileName.Text = "";
                txtInitials.Enabled = true;
                lbFileNames.Items.Clear();
                SavePersonData();
                btnSavePersonData.Text = "New test subject";
                txtInitials.Text = "";
                txtInitials.Focus();
            }
            else
            {
                try
                {
                    person = new TestSubject(newID);
                    txtInitials.Enabled = false;
                    btnStartTreadmill.Enabled = true;
                    txtTestNumber.Text = "1";
                    txtFileName.Text = cbTestID.Text + @"\" + cbTestID.Text + "_" + txtTestNumber.Text + "_" + person.PersonData.PersonID;
                    btnSavePersonData.Text = "Save record";
                    btnSavePersonData.Enabled = false;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Check test participant is on the treadmill with markers in place.");
                }
            }
        }

        private void cbTestID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (person != null)
            {
                txtFileName.Text = cbTestID.Text + @"\" + cbTestID.Text + "_" + txtTestNumber.Text + "_" + person.PersonData.PersonID;
            }
            else
            {
                txtFileName.Text = cbTestID.Text + @"\" + cbTestID.Text + "_" + txtTestNumber.Text + "_" + newID;
            }

            if ((cbTestID.Text == " ") || (cbTestID.Text == "Sound") || cbTestID.Text == "Baseline")
            {
                //SendMCECommand.ToggleDrawingFeet(false);
            }
            else if (cbTestID.Text == "Vision")
            {
               // SendMCECommand.ToggleDrawingFeet(true);
            }
            

        }


        bool toggleFeet = false;
        private void button1_Click(object sender, EventArgs e)
        {
            toggleFeet = !toggleFeet;
           // SendMCECommand.ToggleDrawingFeet(toggleFeet);
        }

        private void fbdPath_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnPlaySound_Click(object sender, EventArgs e)
        {
            if (cbTestID.Text == "Sound")
            {
                if (btnPlaySound.Text == "Play sound")
                {
                    //if (CalibrateFeet())
                    {
                        btnPlaySound.Text = "Stop sound";

                        if (cbTestID.Text == "Sound")
                        {
                            try
                            {
                                //sound.LoadWAV();
                                sound.Left = person.Left;
                                sound.Right = person.Right;
                                sound.StartSound();
                            }
                            catch
                            {
                                MessageBox.Show("Is person on treadmill with markers; participant entered?");
                            }
                        }
                    }
                }
                else
                {
                    sound.StopUpdate = true;
                    sound.StopPlaying();
                    btnPlaySound.Text = "Play sound";
                }
            }
        }

        private void nudTaskTime_ValueChanged(object sender, EventArgs e)
        {
            testlength = (int)(nudTaskTime.Value) * 1000;
        }

        private void btnVisualFeedback_Click(object sender, EventArgs e)
        {
            if (btnVisualFeedback.Text == "Start visual feedback")
            {
                btnVisualFeedback.Text = "Stop visual feedback";
                SendMCECommand.SetTreadmillSpeed((float)nudSpeed.Value);
                System.Diagnostics.Debug.WriteLine("Speed sent:" + ((float)nudSpeed.Value).ToString());
            }
            else
            {
                btnVisualFeedback.Text = "Start visual feedback";
                SendMCECommand.SetTreadmillSpeed(0.0F);
            }
        }



     
        
    }
}
