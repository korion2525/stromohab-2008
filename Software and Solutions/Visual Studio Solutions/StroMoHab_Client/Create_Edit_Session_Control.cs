using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;
using StroMoHab_Objects.Communication;
using StroMoHab_Remote_DataManager;

namespace StroMoHab_Client
{
    /// <summary>
    /// A control to setup and edit session configuration (Task, Scheduling etc..)
    /// </summary>
    public partial class Create_Edit_Session_Control : UserControl
    {
        #region Memeber Variables
        private string _searchTextString = "";
        Timer _mouseWheelGuiUpdateTimer = new Timer();
        Timer _delayedSelectTaskTimer = new Timer();
        private Task_Remote_DataManager _remote_Data_Manager = new Task_Remote_DataManager();
        private List<Task> _taskList = new List<Task>();
        private Task _taskToSelect;
        private bool _showDetailedTaskPreview = false;
        private Task_Detailed_Preview_Control task_Detailed_Preview_Control = new Task_Detailed_Preview_Control();
        #endregion

        #region Constructor
        public Create_Edit_Session_Control()
        {
            InitializeComponent();
            
            //Intercept the mouse wheel
            flowLayoutPanelTasks.MouseWheel += new MouseEventHandler(flowLayoutPanelTasks_MouseWheel);

            //A slight delay is required after the mouse wheel event
            _mouseWheelGuiUpdateTimer.Interval = 50;
            _mouseWheelGuiUpdateTimer.Tick += new EventHandler(_mouseWheelGuiUpdateTimer_Tick);

            //timer to delay selecting a task to allow time for the panel to resize
            _delayedSelectTaskTimer.Interval = 300;
            _delayedSelectTaskTimer.Tick += new EventHandler(_delayedSelectTaskTimer_Tick);

            

            //Fill out combobox
            foreach (Task.TaskTypeType type in Enum.GetValues(typeof(Task.TaskTypeType)))
            {
                comboBoxType.Items.Add(Task.GetEnumDescription(type));
            }
            comboBoxType.SelectedIndex = 0;
            foreach (Task.DistanceRangeType type in Enum.GetValues(typeof(Task.DistanceRangeType)))
            {
                comboBoxDistance.Items.Add(Task.GetEnumDescription(type));
            }
            comboBoxDistance.SelectedIndex = 0;
            foreach (Task.NumberOfObjectsRangeType type in Enum.GetValues(typeof(Task.NumberOfObjectsRangeType)))
            {
                comboBoxNumObjects.Items.Add(Task.GetEnumDescription(type));
            }
            comboBoxNumObjects.SelectedIndex = 0;

            //connect to the server via remoting
            if (TCPProcessor.ConnectedToServer)
            {
                _remote_Data_Manager = (Task_Remote_DataManager)Activator.GetObject(typeof(Task_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005, "TaskRemoteDataManagerConnection"));
                
            }

            //Setup the detailed task preview
            task_Detailed_Preview_Control.Visible = false;
            task_Detailed_Preview_Control.Dock = DockStyle.Fill;
            this.Controls.Add(task_Detailed_Preview_Control);
        }
        #endregion

        #region Task Loading
        /// <summary>
        /// Loads in tasks from the server - call when starting up but not at Initialisation to limit delays
        /// </summary>
        public void LoadInTasks()
        {
            UpdateListOfTasks();
            GenerateTaskControls();
        }
        /// <summary>
        /// Gets an updated list of tasks from the server
        /// </summary>
        private void UpdateListOfTasks()
        {
            _remote_Data_Manager.ClientRequestTaskList();
            _taskList = _remote_Data_Manager.Tasks;
        }

        /// <summary>
        /// Generates the Task_Preview_Controls for available tasks, sets the task, registers a click event handler and adds them
        /// </summary>
        private void GenerateTaskControls()
        {
            flowLayoutPanelTasks.Controls.Clear();
            foreach (Task t in _taskList)
            {
                Task_Preview_Control tpc = new Task_Preview_Control();
                tpc.Task = t;
                tpc.Click += new EventHandler(tpc_Click);
                tpc.DoubleClick += new EventHandler(tpc_DoubleClick);
                flowLayoutPanelTasks.Controls.Add(tpc);
            }
        }
        #endregion

        #region Filtering

        /// <summary>
        /// Clears all the filters
        /// </summary>
        public void Reset()
        {
            ShowDetailedTaskPreview = false;
            comboBoxType.SelectedIndex = 0;
            comboBoxDistance.SelectedIndex = 0;
            comboBoxNumObjects.SelectedIndex = 0;
            textBoxSearch.Text = "Task Name or Description";
            _searchTextString = "";

            FilterTasks();
            if (flowLayoutPanelTasks.Controls.Count != 0)
            {
                flowLayoutPanelTasks.ScrollControlIntoView(flowLayoutPanelTasks.Controls[0]);
            }
        }
        
        /// <summary>
        /// Filters out the tasks based on the selections made by the user
        /// </summary>
        private void FilterTasks()
        {
            OnSetProgressBarValue(0);
            ClearSelection();
            _searchTextString = _searchTextString.ToLower();
            string[] searchStrings = _searchTextString.Split(" ".ToCharArray());
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                tpc.Visible = true;

                if (comboBoxType.SelectedIndex !=0)
                {
                    if (tpc.Task.TaskType != (Task.TaskTypeType)comboBoxType.SelectedIndex)
                    {
                        tpc.Visible = false;
                     }
                }
                if (comboBoxDistance.SelectedIndex != 0)
                {
                    if (tpc.Task.DistanceRange != (Task.DistanceRangeType)comboBoxDistance.SelectedIndex)
                    {
                        tpc.Visible = false;
                    }
                }
                if (comboBoxNumObjects.SelectedIndex != 0)
                {
                    if (tpc.Task.NumberOfObjectsRange != (Task.NumberOfObjectsRangeType)comboBoxNumObjects.SelectedIndex)
                    {
                        tpc.Visible = false;
                    }
                }

                if (_searchTextString != "")
                {
                    bool foundAllStringsInName = true;
                    bool foundAllStringsInDescription = true;
                    foreach (string s in searchStrings)
                    {
                        if (!tpc.Task.FileName.ToLower().Contains(s))
                            foundAllStringsInName = false;
                        if (!tpc.Task.TaskDescription.ToLower().Contains(s))
                            foundAllStringsInDescription = false;
                    }
                    if (foundAllStringsInDescription == false && foundAllStringsInName == false)
                        tpc.Visible = false;                  


                }
            }
            OnSetProgressBarValue(100);
        }
        #endregion

        #region Actions
        void tpc_Click(object sender, EventArgs e)
        {
            ClearSelection();
            SelectTask_Preview_Control(sender);
        }
        void tpc_DoubleClick(object sender, EventArgs e)
        {
            ClearSelection();
            SelectTask_Preview_Control(sender);
            buttonSelect_Click(null, null);
        }
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FilterTasks();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnCancel();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if(CurrentClinician.EditTasks)
            {

            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                if (tpc.Selected)
                {
                    OnTaskSelect(tpc.Task);
                    break;
                }
            }

        }

        public Task SelectedTask
        {
            get
            {
                foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
                {
                    if (tpc.Selected)
                    {
                        return tpc.Task;
                    }
                }
                return null;
            }
        }


        private void flowLayoutPanelTasks_SizeChanged(object sender, EventArgs e)
        {
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                if (tpc.Selected)
                {
                    DelayedSelectTask(tpc.Task);

                }
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (TCPProcessor.ConnectedToServer)
            {
                _remote_Data_Manager = (Task_Remote_DataManager)Activator.GetObject(typeof(Task_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005, "TaskRemoteDataManagerConnection"));
                _remote_Data_Manager.ClientRequestTaskList();
                UpdateListOfTasks();
                GenerateTaskControls();
                OnSelectionStateChanged(false);
            }
        }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(null, null);
        }

        private void comboBoxDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(null, null);
        }

        private void comboBoxNumObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonFilter_Click(null, null);
        }

        private void textBoxSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSearch.Text.StartsWith("Task Name or Description"))
                textBoxSearch.Text = "";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (!textBoxSearch.Text.StartsWith("Task Name or Description"))
            {
                _searchTextString = textBoxSearch.Text;
            }
            FilterTasks();
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonSearch_Click(null, null);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Shows the detailed task preview so that the user can get a better idea of the task before selecting it
        /// </summary>
        public bool ShowDetailedTaskPreview
        {
            set
            {
                _showDetailedTaskPreview = value;
                if (value)
                {
                    foreach (Control c in this.Controls)
                    {
                        c.Visible = false;
                    }
                    task_Detailed_Preview_Control.Task = SelectedTask;
                    task_Detailed_Preview_Control.Visible = value;
                }
                else
                {
                    foreach (Control c in this.Controls)
                    {
                        c.Visible = true;
                    }
                    task_Detailed_Preview_Control.Visible = false;
                }
            }
            get { return _showDetailedTaskPreview; }
        }
        /// <summary>
        /// Gets or sets the current clinician - used to determine permissions and to get the name of the last person to make an edit
        /// </summary>
        public Clinician CurrentClinician { get; set; }
        #endregion

        #region Events
        private void OnSetProgressBarValue(int value)
        {
            if (SetProgressBarValue != null)
                SetProgressBarValue(value);
        }
        public delegate void SetProgressBarValueEventHandler(int value);
        public static event SetProgressBarValueEventHandler SetProgressBarValue;

        private void OnTaskSelect(Task task)
        {
            if (TaskSelect != null)
                TaskSelect(task);
        }
        public delegate void TaskSelectEventHandler(Task task);
        public static event TaskSelectEventHandler TaskSelect;

        private void OnSelectionStateChanged(bool selected)
        {
            if (SelectionStateChanged != null)
                SelectionStateChanged(selected);
        }
        public delegate void SelectionStateChangedEventHandler(bool selected);
        public static event SelectionStateChangedEventHandler SelectionStateChanged;

        private void OnCancel()
        {
            if (Cancel != null)
                Cancel();
        }
        public delegate void CancelEventHandler();
        public static event CancelEventHandler Cancel;

        private void OnCreateNewTask()
        {
            if (CreateNewTask != null)
                CreateNewTask();
        }
        public delegate void CreateNewTaskEventHandler();
        public static event CreateNewTaskEventHandler CreateNewTask;
        #endregion

        #region Task Selection and Scrolling
        /// <summary>
        /// Clears any selections
        /// </summary>
        private void ClearSelection()
        {
            OnSelectionStateChanged(false);
            // unselect all controls
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                tpc.Selected = false;
            }
        }
        /// <summary>
        /// Select a Task_Preview_Control
        /// </summary>
        /// <param name="control"></param>
        private void SelectTask_Preview_Control(object control)
        {
            Task_Preview_Control tpc = (Task_Preview_Control)control;
            tpc.Selected = true;
            OnSelectionStateChanged(true);
        }


        void _delayedSelectTaskTimer_Tick(object sender, EventArgs e)
        {
            SelectTask(_taskToSelect);
            _delayedSelectTaskTimer.Stop();
        }
        /// <summary>
        /// Selects the specified task (after a short delay to ensure the panel size has setup correct for it to scroll)
        /// </summary>
        /// <param name="task"></param>
        public void DelayedSelectTask(Task task)
        {
            _taskToSelect = task;
            _delayedSelectTaskTimer.Start();
        }
        /// <summary>
        /// Selects a specific task
        /// </summary>
        /// <param name="task"></param>
        private void SelectTask(Task task)
        {
            ClearSelection();
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                if (tpc.Task.FileName.CompareTo(task.FileName) == 0)
                {
                    SelectTask_Preview_Control(tpc);
                    flowLayoutPanelTasks.ScrollControlIntoView(tpc);
                    _mouseWheelGuiUpdateTimer.Start();
                    break;
                }
            }
        }
        private void flowLayoutPanelTasks_MouseEnter(object sender, EventArgs e)
        {
            //give the panel focus so that it can be scrolled
            this.flowLayoutPanelTasks.Focus();
        }
        private void ReSelectTask()
        {
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                if (tpc.Selected)
                    tpc.Selected = true; // reselect to ensure the selection border is re-drawn
            }
        }
        private void ReSelectTaskAndScroll()
        {
            foreach (Task_Preview_Control tpc in flowLayoutPanelTasks.Controls)
            {
                if (tpc.Selected)
                {
                    tpc.Selected = true; // reselect to ensure the selection border is re-drawn
                    flowLayoutPanelTasks.ScrollControlIntoView(tpc);
                }
            }
        }

        private void flowLayoutPanelTasks_Scroll(object sender, ScrollEventArgs e)
        {
            ReSelectTask();
        }
        void flowLayoutPanelTasks_MouseWheel(object sender, MouseEventArgs e)
        {
            _mouseWheelGuiUpdateTimer.Start();
        }

        void _mouseWheelGuiUpdateTimer_Tick(object sender, EventArgs e)
        {
            ReSelectTask();
            _mouseWheelGuiUpdateTimer.Stop();
        }
        #endregion

        private void buttonLaunchTD_Click(object sender, EventArgs e)
        {
            OnCreateNewTask();
        }
    }
}
