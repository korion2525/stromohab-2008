using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting;

using Win32Utilities;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;
using StroMoHab_Objects.Graphics;
using StroMoHab_Remote_DataManager;
using System.Drawing.Imaging;


namespace StroMoHab_Task_Designer.Forms
{
    /// <summary>
    /// Main Task Designer Form.
    /// </summary>
    public partial class TaskDesigner : Form
    {
        #region Member Variables
        private StromoLight_RemoteDrawingList.DrawingList m_objectsToDraw;
        private Task_Remote_DataManager _remote_DataManager = new Task_Remote_DataManager();
        private decimal m_prevHeight = 0;
        private const bool REMOTE_FILE = true;
        private bool readOnlyPlaybackMode = false;
        private Task _currentTask = new Task();
        private Task _defaultTask;

        private int m_currentSelectedValue = -1;

        #endregion Member Variables

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int CurrentSelectedObject
        {
            get { return m_currentSelectedValue; }
            set { m_currentSelectedValue = value; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Main Task Designer Form.
        /// </summary>
        public TaskDesigner()
        {
            InitializeComponent();
            TCPProcessor.ManagedConnectToServer();

            m_objectsToDraw = (StromoLight_RemoteDrawingList.DrawingList)Activator.GetObject(typeof(StromoLight_RemoteDrawingList.DrawingList), "tcp://localhost:8002/TaskDesignerConnection");
            m_prevHeight = this.numericUpDownObjectHeight.Value;

            

            SendMCECommand.StartCameras();
            //Set up getting tasks from server
            if (TCPProcessor.ConnectedToServer)
                _remote_DataManager = (Task_Remote_DataManager)Activator.GetObject(typeof(Task_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005,"TaskRemoteDataManagerConnection"));
            
            //make the default task
            _defaultTask = new Task();
            Corridor corridor = new Corridor(-1.5f, 0.0f, 4.0f, 1.5f, 6.0f, -500.0f);
            _defaultTask.ObjectList.Add(corridor);


            
        }

        void Task_Designer_Remote_Control_EditTaskRequest(Task task)
        {
            _currentTask = task;
            m_objectsToDraw.ObjectsToDraw = _currentTask.ObjectList;
        }
        /// <summary>
        /// Accepts the task set by the server
        /// </summary>
        /// <param name="task"></param>
        /// <param name="readOnly"></param>
        void Remote_DataManager_TaskSetByServer(Task task, bool readOnly)
        {
            m_objectsToDraw.ObjectsToDraw = task.ObjectList;
            readOnlyPlaybackMode = readOnly;
        }


        #endregion Constructor



        /// <summary>
        /// Menu items associated with the User Control
        /// </summary>
        public ToolStripItemCollection MenuStripItems
        {
            get 
            {
                menuStrip1.Visible = false;
                return this.menuStrip1.Items; 
            }
        }

        #region Form Load

        private void Form_TaskDesigner_Load(object sender, EventArgs e)
        {
            comboBxObjectToAdd.Text = "<Select>";
            comboBxObjectToAdd.SelectedIndex = 1;
            pictureBxTextureImage.Visible = checkBoxUseTexture.Checked;
            numericUpDownImageNumber.Enabled = checkBoxUseTexture.Checked;
            labelImage.Enabled = checkBoxUseTexture.Checked;

            string textureFilesPath;
            DirectoryInfo dInfoTextures = new DirectoryInfo(Environment.CurrentDirectory);
            dInfoTextures = dInfoTextures.Parent;
            textureFilesPath = dInfoTextures.FullName + @"\Textures";
            
            string[] textureFiles=new string[20];

            foreach (string currentTextureFilePath in Textures.ListOfTextureFilePaths("bmp"))
            {
                Bitmap currentBitmap = new Bitmap(currentTextureFilePath);
                currentBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                imageList.Images.Add(currentBitmap);
                this.numericUpDownImageNumber.Maximum = (imageList.Images.Count - 1);
                pictureBxTextureImage.Image = imageList.Images[(int)this.numericUpDownImageNumber.Value];
            }

            //if (Directory.Exists(textureFilesPath))
            //{
            //    textureFiles = Directory.GetFiles(textureFilesPath, "*.bmp");
            //}

            //if (textureFiles[0] == null)
            //{
            //    if (System.Windows.Forms.MessageBox.Show("Cannot find texture files. Navigate to directory?", "Cannot Find Texture Files", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            //        folderBrowser.Description = "Select texture files folder for Task Designer.";
            //        folderBrowser.ShowDialog();
            //        textureFilesPath = folderBrowser.SelectedPath;
            //        textureFiles = Directory.GetFiles(textureFilesPath, "*.bmp");
            //    }
            //    else
            //    {
            //        Environment.Exit(-1);
            //    }
            //}
            //    foreach (string currentFile in textureFiles)
            //    {
            //        try
            //        {
            //            Bitmap currentImage = new Bitmap(currentFile);
            //            currentImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //            imageList.Images.Add(currentImage);
            //            this.numericUpDownImageNumber.Maximum = (imageList.Images.Count - 1);
            //            pictureBxTextureImage.Image = imageList.Images[(int)this.numericUpDownImageNumber.Value];
            //        }
            //        catch (ArgumentNullException ex)
            //        {
            //            //                    System.Windows.Forms.MessageBox.Show("Cannot find texture files. Please ensure that the \"Textures\" folder is in the executable parent directory. Exception message: " + ex.Message, "Cannot Find Texture Files");
            //            if (System.Windows.Forms.MessageBox.Show("Cannot find texture files. Navigate to directory?", "Cannot Find Texture Files", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //            {
            //                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            //                folderBrowser.ShowDialog();
            //                textureFilesPath = folderBrowser.SelectedPath;
            //            }
            //            else
            //            {
            //                Environment.Exit(-1);
            //            }
            //        }
                
            //}

            

            


            Externals.LoadVisualiser(m_objectsToDraw);


           

            Externals.SendNotification("TDSOK");

            
        }

        #endregion Form Load

        #region Private Methods

        /// <summary>
        /// Updates the UI controls with a selected object's properties
        /// </summary>
        /// <param name="currentObject"></param>
        private void PopulateUIWithObjectProperties(OpenGlObject currentObject)
        {
            //populate controls with current object properties
            numericUpDownObjectWidth.Value = Convert.ToDecimal(currentObject.Length);
            numericUpDownObjectDepth.Value = Convert.ToDecimal(Math.Abs(currentObject.Depth)); //TODO: Check why OpenGLObject.Depth return +ve and -ve values for different objects.
            numericUpDownDistanceFromGround.Value = Convert.ToDecimal(currentObject.Y);
            numericUpDownObjectHeight.Value = Convert.ToDecimal(currentObject.Height);
            txtBxLeftRightPosition.Text = Convert.ToString(currentObject.X);

        }

        #endregion Private Methods

        #region Control Event Handlers

        private void comboBxObjectToAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBxObjectToAdd.SelectedItem.ToString())
            {
                case "Cube":
                    {
                        groupBxCube.Enabled = true;
                        groupBxCube.Show();
                        break;
                    }

                case "Corridor":
                    groupBxCube.Text = "Corridor";
                    break;

                default:
                    {
                        groupBxCube.Enabled = false;
                        break;
                    }
            }
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            OpenGlObject objectToAdd = null;

            if (txtBxLeftRightPosition.Text != "")
            {
                float xMin = ((float.Parse(txtBxLeftRightPosition.Text)) - (float)(numericUpDownObjectWidth.Value / 2));
                float xMax = xMin + ((float)numericUpDownObjectWidth.Value);

                float yMin = (float)numericUpDownDistanceFromGround.Value;
                float yMax = yMin + (float)numericUpDownObjectHeight.Value;

                float zMin = -(float)this.customNumericUpDownPlaceObjectAt.Value;
                float zMax = zMin + (float)numericUpDownObjectDepth.Value;

                if (checkBoxUseTexture.Checked)
                {
                    objectToAdd = new Cube((int)numericUpDownImageNumber.Value, 1, 1, xMin, yMin, zMin, xMax, yMax, zMax);
                }
                else
                {
                    objectToAdd = new Cube(xMin, yMin, zMin, xMax, yMax, zMax);
                }

                m_objectsToDraw.Add(objectToAdd);


            }

        }

        private void numericUpDownSize_Leave(object sender, EventArgs e)
        {
            numericUpDownObjectWidth.Text = numericUpDownObjectWidth.Value.ToString();
        }

        private void numericUpDownDistanceFromGround_Leave(object sender, EventArgs e)
        {
            numericUpDownDistanceFromGround.Text = numericUpDownDistanceFromGround.Value.ToString();
        }

        private void customNumericUpDownCurrentPosition_Leave(object sender, EventArgs e)
        {
            customNumericUpDownPlaceObjectAt.Text = customNumericUpDownPlaceObjectAt.Value.ToString();
        }

        private void trackBarLeftRightPosition_ValueChanged(object sender, EventArgs e)
        {
            txtBxLeftRightPosition.Text = ((float)(trackBarLeftRightPosition.Value - 150) / 100).ToString();
        }

        private void txtBxLeftRightPosition_Leave(object sender, EventArgs e)
        {
            if (txtBxLeftRightPosition.Text == "")
            {
                txtBxLeftRightPosition.Text = "0";
            }
        }

        private void checkBoxUseTextures_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseTexture.Checked)
            {
                checkBoxUseTexture.Font = new Font(checkBoxUseTexture.Font, FontStyle.Bold);
            }
            else
            {
                checkBoxUseTexture.Font = new Font(checkBoxUseTexture.Font, FontStyle.Regular);
            }

            labelImage.Enabled = checkBoxUseTexture.Checked;
            numericUpDownImageNumber.Enabled = checkBoxUseTexture.Checked;
            pictureBxTextureImage.Visible = checkBoxUseTexture.Checked;
            pictureBxTextureImage.Enabled = checkBoxUseTexture.Checked;
        }

        private void customNumericUpDownSelectObject_ValueChanged(object sender, EventArgs e)
        {
            if (m_currentSelectedValue != -1)
            {
                m_objectsToDraw.SetSelected(m_currentSelectedValue, false);
            }

            if (customNumericUpDownSelectObject.Value < m_objectsToDraw.ObjectsToDraw.Count)
            {
                //set selected property of object in list
                m_objectsToDraw.SetSelected((int)customNumericUpDownSelectObject.Value, true);
                
                //populate UI with selected objects properties
                PopulateUIWithObjectProperties(m_objectsToDraw.GetObjectFromList((int)customNumericUpDownSelectObject.Value));

             
            }
            else
            {
                if (m_currentSelectedValue != -1)
                {
                    customNumericUpDownSelectObject.Value = (decimal)m_currentSelectedValue;
                }
            }
            m_currentSelectedValue = (int)customNumericUpDownSelectObject.Value;

            if (m_currentSelectedValue == 0)
            {
                CorridorControls(true);
            }
            else
            {
                CorridorControls(false);
            }
        }

        private void CorridorControls(bool activate)
        {
            //5Panel c
        }

        private void Form_TaskDesigner_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Escape)
            {
                //this.Close();
            }
        }

        private void checkBoxSelectObject_CheckedChanged(object sender, EventArgs e)
        {
            labelObjectNumber.Enabled = checkBoxSelectObject.Checked;
            customNumericUpDownSelectObject.Enabled = checkBoxSelectObject.Checked;
            m_currentSelectedValue = (int)customNumericUpDownSelectObject.Value;
            buttonRemoveObject.Enabled = checkBoxSelectObject.Checked;

            m_objectsToDraw.SetSelected(m_currentSelectedValue, checkBoxSelectObject.Checked);

            if (this.checkBoxSelectObject.Checked)
            {
                PopulateUIWithObjectProperties(m_objectsToDraw.GetObjectFromList((int)customNumericUpDownSelectObject.Value));
            }
        }

        private void numericUpDownImageNumber_ValueChanged(object sender, EventArgs e)
        {
            if (imageList.Images.Count >0)
            {
                pictureBxTextureImage.Image = imageList.Images[(int)numericUpDownImageNumber.Value];
            }
            if (checkBoxSelectObject.Checked)
            {
                m_objectsToDraw.SetTextureNumber((int)customNumericUpDownSelectObject.Value, (int)numericUpDownImageNumber.Value);
            }
        }

        private void Form_TaskDesigner_Activated(object sender, EventArgs e)
        {
            this.numericUpDownCurrentPosition.Focus();
        }

        private void buttonRemoveObject_Click(object sender, EventArgs e)
        {
            m_objectsToDraw.RemoveAt((int)this.customNumericUpDownSelectObject.Value);

            if (m_objectsToDraw.ObjectsToDraw.Count > 0)
            {
                if (this.customNumericUpDownSelectObject.Value - 1 > 0)
                {
                    this.customNumericUpDownSelectObject.Value -= 1;
                }
                else
                {
                    this.customNumericUpDownSelectObject.Value = m_objectsToDraw.ObjectsToDraw.Count;
                }
            }
        }


        private void numericUpDownCurrentPosition_ValueChanged(object sender, EventArgs e)
        {
            m_objectsToDraw.MoveAvatarInVisualiser((float)numericUpDownCurrentPosition.Value);
            this.customNumericUpDownPlaceObjectAt.Value = this.numericUpDownCurrentPosition.Value + 2m;
        }

        private void createANewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentTask = _defaultTask;
            m_objectsToDraw.ObjectsToDraw = _currentTask.ObjectList;
        }

        private void saveTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentTask.ObjectList = m_objectsToDraw.ObjectsToDraw;
            numericUpDownCurrentPosition.Value = 0; // reset view then grab a screen shot
            _currentTask.PreviewImage = GetScreenshotFromVisualiser();
            
            
            if (readOnlyPlaybackMode == false)
            {
                TaskFileBrowser taskFileDialog = new TaskFileBrowser(_remote_DataManager, _currentTask);
                taskFileDialog.ShowDialog();
            }
            else
                MessageBox.Show("Cannot moddify the current task.", "Playback mode enabled.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          
        }

        private void openExistingTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (readOnlyPlaybackMode == false)
            {
                TaskFileBrowser taskFileDialog = new TaskFileBrowser(_remote_DataManager, null);
                if (taskFileDialog.ShowDialog() != DialogResult.Cancel)
                {

                    _currentTask = taskFileDialog.Task;
                    m_objectsToDraw.ObjectsToDraw = _currentTask.ObjectList;
                }

            }
            else
                MessageBox.Show("Cannot moddify the current task.", "Playback mode enabled.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void numericUpDownObjectWidth_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectObject.Checked)
            {
                if (this.customNumericUpDownSelectObject.Value != 0)
                {
                    m_objectsToDraw.SetNewObjectWidth((int)customNumericUpDownSelectObject.Value, (float)numericUpDownObjectWidth.Value);
                }
                else
                {
                    m_objectsToDraw.ReplaceAt(0, new Corridor((float)this.numericUpDownObjectWidth.Value, m_objectsToDraw.ObjectsToDraw[0].Height, m_objectsToDraw.ObjectsToDraw[0].Depth));
                }
            }


        }

        private void numericUpDownObjectHeight_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectObject.Checked)
            {
                if (this.customNumericUpDownSelectObject.Value != 0)
                {
                    m_objectsToDraw.SetNewObjectHeight((int)customNumericUpDownSelectObject.Value, (float)numericUpDownObjectHeight.Value);

                    //TODO: Fix this section. It attempts to keep objects on the ground while increasing their height. If this isn't required, remove.

                    //this.numericUpDownDistanceFromGround.Minimum = this.numericUpDownObjectHeight.Value / 2;

                    //if (this.numericUpDownObjectHeight.Value > m_prevHeight)
                    //{
                    //    this.numericUpDownDistanceFromGround.Value += (this.numericUpDownObjectHeight.Value - m_prevHeight) / 2;
                    //    m_prevHeight = this.numericUpDownObjectHeight.Value;
                    //}
                    //else
                    //{
                    //    this.numericUpDownDistanceFromGround.Value -= (m_prevHeight - this.numericUpDownObjectHeight.Value) / 2;
                    //    m_prevHeight = this.numericUpDownObjectHeight.Value;
                    //}
                }
                else
                {
                    m_objectsToDraw.ReplaceAt(0, new Corridor(m_objectsToDraw.ObjectsToDraw[0].Length, (float)this.numericUpDownObjectHeight.Value, m_objectsToDraw.ObjectsToDraw[0].Depth));
                }
            }
        }

        private void numericUpDownObjectDepth_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectObject.Checked)
            {
                if (this.customNumericUpDownSelectObject.Value != 0)
                {
                    m_objectsToDraw.SetNewObjectDepth((int)customNumericUpDownSelectObject.Value, (float)numericUpDownObjectDepth.Value);
                }
                else
                {
                    m_objectsToDraw.ReplaceAt(0, new Corridor(m_objectsToDraw.ObjectsToDraw[0].Length, m_objectsToDraw.ObjectsToDraw[0].Height, (float)this.numericUpDownObjectDepth.Value));
                }
            }
        }

        private void numericUpDownDistanceFromGround_ValueChanged(object sender, EventArgs e)
        {
            //check to see if modifying existing object
            if (checkBoxSelectObject.Checked)
            {
                //corridor currently always has object ID 0
                if (this.customNumericUpDownSelectObject.Value != 0)
                {
                    m_objectsToDraw.SetNewObjectY((int)this.customNumericUpDownSelectObject.Value, (float)this.numericUpDownDistanceFromGround.Value);
                }
            }
        }

        private void trackBarLeftRightPosition_Scroll(object sender, EventArgs e)
        {
            //check to see if modifying existing object
            if (checkBoxSelectObject.Checked)
            {
                //corridor currently always has object ID 0
                if (this.customNumericUpDownSelectObject.Value != 0)
                {
                    //set objects X value, using value in txtBxLeftRightPosition.Text, _not_ trackBarLeftRightPosition.Value as this is not currently scaled correctly.
                    m_objectsToDraw.SetNewObjectX((int)this.customNumericUpDownSelectObject.Value, (float)Convert.ToDouble(this.txtBxLeftRightPosition.Text));
                }
            }
        }

        private void ckBoxTaskDesignMode_CheckedChanged(object sender, EventArgs e)
        {
            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            Win32.SetForegroundWindow(visualiserWindowHandle);
            SendKeys.SendWait("{F1}");
        }

        #endregion Control Event Handlers

        #region Form Closing
        /// <summary>
        /// Performs a cleanup of the drawing objects.
        /// </summary>
        /*public void Close()
        {
            /*
            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            if (visualiserWindowHandle != null)
            {
                Win32.SetForegroundWindow(visualiserWindowHandle);
                SendKeys.Send("{Escape}");
            }
            ////
            Form_TaskDesigner_FormClosing(null, null);
        }*/
        private void Form_TaskDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //clear the selected property for each 
                    for (int i = 0; i < m_objectsToDraw.ObjectsToDraw.Count; i++)
                    {
                        m_objectsToDraw.SetSelected(i, false);
                    }
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Diagnostics.Debug.WriteLine("Tried to access object not in object drawing list. Exception message: " + ex.Message);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                System.Diagnostics.Debug.WriteLine("The visualiser no longer appears to be running. Exception message: " + ex.Message);
            }


        }

        #endregion Form Closing

        private Image ByteArrayToImage(byte[] byteArray)
        {
            //transform the byte array into an image
            MemoryStream ms = new MemoryStream(byteArray);
            Image img = Image.FromStream(ms);
            // Do NOT close the stream!

            return img;
        }

        private byte[] GetScreenshotFromVisualiser()
        {
            m_objectsToDraw.GeneratingScreenShot = true;
            //Find the visualiser and send it the command
            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            Win32.SetForegroundWindow(visualiserWindowHandle);
            SendKeys.SendWait("{F11}");

            //Wait for it to be ready
            int i = 0;
            while (m_objectsToDraw.TaskScreenShot == null || m_objectsToDraw.GeneratingScreenShot)
            {
                i++;
                System.Threading.Thread.Sleep(100);
                if (i >= 20)
                    break;
            }

            return m_objectsToDraw.TaskScreenShot;
        }

        /// <summary>
        /// Forces the Task Designer to get the current task from the Server
        /// This is used when in playback mode
        /// </summary>
        public void GetCurrentTaskFromServer()
        {
            _currentTask = _remote_DataManager.Task;
            try
            {
                m_objectsToDraw.ObjectsToDraw = _currentTask.ObjectList;
            }
            catch
            {
                Externals.LoadVisualiser(m_objectsToDraw);
                MessageBox.Show("Loading Visualiser...\nPress Ok when Done.");
                m_objectsToDraw.ObjectsToDraw = _currentTask.ObjectList;

            }
        }

       

    }
}