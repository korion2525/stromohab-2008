
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;
using System.IO;

namespace StroMoHab_Remote_DataManager
{
    /// <summary>
    /// A remote task file browser to save and load a task
    /// </summary>
    public partial class TaskFileBrowser : Form
    {
        private Task _selectedTask;
        private bool saveMode = false;
        private Task_Remote_DataManager _remote_DataManager;
        public TaskFileBrowser(Task_Remote_DataManager remote_dataManager, Task taskToSave)
        {
            _remote_DataManager = remote_dataManager;
            InitializeComponent();
            
            buttonGetList_Click(null, null);

            if (taskToSave == null) // Open Mode
            {
                buttonNewTask.Enabled = false;
                textBoxNewTaskName.Enabled = false;
                textBoxDescription.ReadOnly = true;
            }
            else // save mode
            {
                saveMode = true;
                textBoxNewTaskName.Text = taskToSave.FileName;
                textBoxDescription.Text = taskToSave.TaskDescription;
                buttonOpen.Enabled = false;
                _selectedTask = taskToSave;

                pictureBox.Image = ByteArrayToImage(_selectedTask.PreviewImage);
                labelTaskPreview.Visible = false;
            }
        }


        public Task Task
        {
            get { return _selectedTask; }
        }

        private void buttonGetList_Click(object sender, EventArgs e)
        {
            _remote_DataManager.ClientRequestTaskList();
            fileListBox.Items.Clear();
            foreach (string taskFile in _remote_DataManager.ListOfTasksNames)
            {
                fileListBox.Items.Add(taskFile);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedIndex != -1)
            {
                _remote_DataManager.ClientRequestTask(fileListBox.SelectedIndex);
                _selectedTask = _remote_DataManager.Task;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonNewTask_Click(object sender, EventArgs e)
        {
            if (ValidateFileName())
            {
                if (_selectedTask.NumberOfObjects == 0)
                    _selectedTask.TaskType = Task.TaskTypeType.Free_Walking;
                else
                    _selectedTask.TaskType = Task.TaskTypeType.Obstacle_Avoidence;

                _selectedTask.FileName = textBoxNewTaskName.Text;
                _selectedTask.TaskDescription = textBoxDescription.Text;
                _remote_DataManager.ClientRequestSaveTask(_selectedTask);
                this.Close();
            }
            else
                MessageBox.Show("Invalid File Name");
        }

        /// <summary>
        /// Validates the file name that will be used to save the file
        /// </summary>
        /// <returns></returns>
        private bool ValidateFileName()
        {
            if (textBoxNewTaskName.Text.Length == 0)
                return false;
            foreach (char ch in System.IO.Path.GetInvalidFileNameChars())
            {
                if (textBoxNewTaskName.Text.Contains(ch))
                    return false;
            }
            return true;
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saveMode)
            {
                _remote_DataManager.ClientRequestTask(fileListBox.SelectedIndex);
                textBoxNewTaskName.Text = _remote_DataManager.Task.FileName;
            }
            else
            {

                _remote_DataManager.ClientRequestTask(fileListBox.SelectedIndex);
                _selectedTask = _remote_DataManager.Task;
                textBoxDescription.Text = _selectedTask.TaskDescription;
                if (_selectedTask.PreviewImage != null)
                {
                    pictureBox.Image = ByteArrayToImage(_selectedTask.PreviewImage);
                    labelTaskPreview.Visible = false;
                }
                else
                {
                    pictureBox.Image = null;
                    labelTaskPreview.Visible = true;
                }
            }
           
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            //transform the byte array into an image
            MemoryStream ms = new MemoryStream(byteArray);
            Image img = Image.FromStream(ms);
            // Do NOT close the stream!

            return img;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete : " + fileListBox.SelectedItem, "Delete Task", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _remote_DataManager.ClientRequestDeleteTask(fileListBox.SelectedIndex);
                    buttonGetList_Click(null, null);
                }
            }
        }

    }
}
