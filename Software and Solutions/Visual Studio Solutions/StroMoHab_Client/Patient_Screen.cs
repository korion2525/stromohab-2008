using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Remote_DataManager;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    /// <summary>
    /// The main screen where sessions and scheduled and run
    /// </summary>
    public partial class Patient_Screen : UserControl
    {
        #region Member Variables
        private int _selectedPatientIndex = 0;
        private Timer _runReviewButtonTimer = new Timer();
        private Patient _patient;
        private int _selectedSessionIndex = 0;
        /// <summary>
        /// Used for sorting the list
        /// </summary>
        private ListViewColumnSorter _sessionSorter;
            
        /// <summary>
        /// Remote connection to the server
        /// </summary>
        private Patient_Remote_DataManager _remote_DataManager = new Patient_Remote_DataManager();
        
        #endregion

        #region Constructor
        public Patient_Screen()
        {
            
            InitializeComponent();

            if (TCPProcessor.ConnectedToServer)
                _remote_DataManager = (Patient_Remote_DataManager)Activator.GetObject(typeof(Patient_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005, "PatientRemoteDataManagerConnection"));

            _sessionSorter = new ListViewColumnSorter();
            this.listViewSessions.ListViewItemSorter = _sessionSorter;

            listViewSessions.Groups.Add(listViewGroup_New);
            listViewSessions.Groups.Add(listViewGroup_Scheduled);
            listViewSessions.Groups.Add(listViewGroup_Completed);

            _runReviewButtonTimer.Interval = 100;
            _runReviewButtonTimer.Tick += new EventHandler(_runReviewButtonTimer_Tick);
            _runReviewButtonTimer.Start();

            Create_Edit_Session_Control.Cancel += new Create_Edit_Session_Control.CancelEventHandler(Create_Edit_Session_Control_Cancel);
            Create_Edit_Session_Control.TaskSelect += new Create_Edit_Session_Control.TaskSelectEventHandler(Create_Edit_Session_Control_TaskSelect);
            Create_Edit_Session_Control.SetProgressBarValue += new Create_Edit_Session_Control.SetProgressBarValueEventHandler(Create_Edit_Session_Control_SetProgressBarValue);
            Create_Edit_Session_Control.SelectionStateChanged += new Create_Edit_Session_Control.SelectionStateChangedEventHandler(Create_Edit_Session_Control_SelectionStateChanged);
            Create_Edit_Session_Control.CreateNewTask += new Create_Edit_Session_Control.CreateNewTaskEventHandler(Create_Edit_Session_Control_CreateNewTask);




        }
        #endregion

        #region Event Handlers
        void Create_Edit_Session_Control_CreateNewTask()
        {
            //Load the task designer and pass null to indicate you want the task designer to make a new task based on its default
            LoadTaskDeisnger(null);
        }

        void Create_Edit_Session_Control_SelectionStateChanged(bool selected)
        {
            if (create_Edit_Session_Control.Visible)
            {
                buttonEdit.Enabled = selected;
                buttonOpen.Enabled = selected;
            }
        }

        void Create_Edit_Session_Control_SetProgressBarValue(int value)
        {
            OnSetProgressBarValue(value);
        }

        void Create_Edit_Session_Control_TaskSelect(Task task)
        {
            if (create_Edit_Session_Control.ShowDetailedTaskPreview)
            {
                ShowScheduledSessionControlButtons();
                create_Edit_Session_Control.Visible = false;
                session_Details_Control.Visible = true;
                _patient.Sessions[_selectedSessionIndex].Clinician = CurrentClinician.NameString;
                _patient.Sessions[_selectedSessionIndex].Task = task;
                _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
                FillInFields();
                session_Details_Control.Session = _patient.Sessions[_selectedSessionIndex];
                for (int i = 0; i < listViewSessions.Items.Count; i++)
                {
                    if (Int32.Parse(listViewSessions.Items[i].SubItems[listViewSessions.Columns.Count].Text) == _selectedSessionIndex)
                        listViewSessions.Items[i].Selected = true;

                }
                ShowScheduledSessionControlButtons();
                session_Details_Control.ReadOnlyNotes = false;
            }
            else
                create_Edit_Session_Control.ShowDetailedTaskPreview = true;
        }

        void Create_Edit_Session_Control_Cancel()
        {
            create_Edit_Session_Control.Visible = false;
            session_Details_Control.Visible = true;
        }

        void _runReviewButtonTimer_Tick(object sender, EventArgs e)
        {
            //Run button
            if (_remote_DataManager.RecordingStatus)
            {
                buttonRunSession.Text = "Stop";
            }
            else
                buttonRunSession.Text = "Run";


            //Review button
            if (_remote_DataManager.PlaybackStatus)
            {
                buttonReviewSession.Text = "Stop";
            }
            else
                buttonReviewSession.Text = "Review";
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current clinician - used to determine permissions and to get the name of the last person to make an edit
        /// </summary>
        public Clinician CurrentClinician { get; set; }

        public int PatientIndex
        {
            get { return _selectedPatientIndex; }
            set
            {
                _selectedPatientIndex = value;
                _patient = _remote_DataManager.PatientList[_selectedPatientIndex];
                session_Details_Control.Visible = false;
                patient_Detials_Control.Visible = true;
                FillInFields();
            }
        }

        #endregion

        #region Events
        private void OnHideForm(bool value)
        {
            if (HideForm != null)
                HideForm(value);
        }
        public delegate void HideFormEventHandler(bool value);
        public static event HideFormEventHandler HideForm;
        private void OnClosePatient()
        {
            if (ClosePatient != null)
                ClosePatient();
        }
        public delegate void ClosePatientEventHandler();
        public static event ClosePatientEventHandler ClosePatient;

        private void OnSetStatusLabelValue(string value)
        {
            if (SetStatusLabelValue != null)
                SetStatusLabelValue(value);
        }
        public delegate void SetStatusLabelValueEventHandler(string value);
        public static event SetStatusLabelValueEventHandler SetStatusLabelValue;


        private void OnSetProgressBarValue(int value)
        {
            if (SetProgressBarValue != null)
                SetProgressBarValue(value);
        }
        public delegate void SetProgressBarValueEventHandler(int value);
        public static event SetProgressBarValueEventHandler SetProgressBarValue;

        #endregion

        #region Methods
        /// <summary>
        /// Fills in all of the forms on the control with the current patients info
        /// </summary>
        private void FillInFields()
        {
            labelTitle.Text = "Patient : " +  _patient.Title + " " + _patient.FirstName + " " + _patient.LastName;
            patient_Detials_Control.Patient = _patient;

            FillInListViewSessions();

            ShowPatientControlButtons();
        }  

        #region CreateNewSession
        /// <summary>
        /// Creates a new blank session
        /// </summary>
        private void CreateNewSession()
        {
            CreateNewSession(new Task());
        }
        /// <summary>
        /// Creates a new session based on the supplied task
        /// </summary>
        /// <param name="task"></param>
        private void CreateNewSession(Task task)
        {
            if (CurrentClinician.ScheduleSessions)
            {
                Session newSession = new Session();

                newSession.Task = task;
                newSession.Clinician = CurrentClinician.NameString;

                // Add
                _patient.AddSession(newSession);
                _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
                _remote_DataManager.ClientRequestUpdatedPatientList();
                FillInListViewSessions();

                if(session_Details_Control.Visible)
                    buttonBack_Click(null, null);
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        #endregion

        #region ShowControlButtons
        private void ShowSessionBrowseControlButtons()
        {
            //first set
            buttonOpen.Enabled = true;
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = false;

            //second set
            buttonReport.Visible = false;
            buttonRunSession.Visible = false;
            buttonReviewSession.Visible = false;
            buttonDuplicate.Visible = false;
        }
        private void ShowPatientControlButtons()
        {
            //first set
            buttonOpen.Enabled = false;
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = false;

            //second set
            buttonReport.Enabled = true;
            buttonReport.Visible = true;
            buttonRunSession.Visible = false;
            buttonReviewSession.Visible = false;
            buttonDuplicate.Visible = false;
        }
        private void ShowScheduledSessionControlButtons()
        {
            //first set
            buttonOpen.Enabled = false;
            buttonDelete.Enabled = true;
            buttonEdit.Enabled = true;

            //second set
            buttonRunSession.Visible = true;
            buttonRunSession.Enabled = true;
            buttonReport.Enabled = false;
            buttonReport.Visible = true;
            buttonReviewSession.Visible = false;
            buttonDuplicate.Visible = true;
            buttonDuplicate.Enabled = true;
        }
        private void ShowSessionControlButtons()
        {
            //first set
            buttonOpen.Enabled = false;
            buttonDelete.Enabled = true;
            buttonEdit.Enabled = true;

            //second set
            buttonReviewSession.Visible = true;
            buttonReviewSession.Enabled = true;
            buttonReport.Enabled = false;
            buttonReport.Visible = true;
            buttonRunSession.Visible = false;
            buttonDuplicate.Visible = true;
            buttonDuplicate.Enabled = true;

        }
        private void ShowSessionEditButtons()
        {
            //first set
            buttonOpen.Enabled = true;
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = true;

            //second set
            buttonReport.Visible = false;
            buttonDuplicate.Visible = false;
            buttonRunSession.Visible = false;

        }
        #endregion

        #region ShowScreen
        /// <summary>
        /// Shows the patient details screen
        /// </summary>
        private void ShowPatientDetailsScreen()
        {
            patient_Detials_Control.Patient = _patient;
            listViewSessions.SelectedItems.Clear();
            session_Details_Control.Visible = false;
            patient_Detials_Control.Visible = true;
            create_Edit_Session_Control.Visible = false;
            ShowPatientControlButtons();
        }
        /// <summary>
        /// Closes the patient screen
        /// </summary>
        private void ClosePatientScreen()
        {
            ShowSessionControlButtons();
            create_Edit_Session_Control.Visible = false;
            session_Details_Control.Visible = false;
            patient_Detials_Control.Visible = true;
            OnSetStatusLabelValue("Saving Patient...");
            OnSetProgressBarValue(0);
            _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
            OnSetProgressBarValue(100);
            OnSetStatusLabelValue("Ready");
            OnClosePatient();
        }
        /// <summary>
        /// Shows the session details screen
        /// </summary>
        private void ShowSessionDetailsScreen()
        {
            session_Details_Control.Visible = true;
            patient_Detials_Control.Visible = false;
            create_Edit_Session_Control.Visible = false;

            session_Details_Control.Session = _patient.Sessions[_selectedSessionIndex];
            session_Details_Control.ReadOnlyNotes = false;
            if (_patient.Sessions[_selectedSessionIndex].ScheduledSession)
                ShowScheduledSessionControlButtons();
            else
                ShowSessionControlButtons();

            if (_patient.Sessions[_selectedSessionIndex].MotionCaptureDataRecorded)
                buttonReviewSession.Enabled = true;
            else
                buttonReviewSession.Enabled = false;
        }
        #endregion

        #region ListView
        /// <summary>
        /// Fills in the list view session with the current patients info
        /// </summary>
        private void FillInListViewSessions()
        {
            labelNoSessions.Visible = false;
            if (_patient.Sessions.Count == 0)
            {
                labelNoSessions.Visible = true;
            }
            listViewSessions.Items.Clear();
            listViewSessions.SelectedItems.Clear();
            listViewSessions.Items.Add(new ListViewItem(new string[] { "New Session", "", "", "", "-1" }, listViewGroup_New));
            listViewSessions.Items[0].ForeColor = Properties.Settings.Default.ListViewNewEntryColor;
            listViewSessions.Items[0].Font = Properties.Settings.Default.ListViewNewEntryFont;
            listViewSessions.Items[0].ToolTipText = "Click to create a new session";

            for (int i = 0; i < _patient.Sessions.Count; i++)
            {
                listViewSessions.Items.Add(GenerateListViewSessionItem(_patient, i));
            }
        }
        /// <summary>
        /// Generates a ListViewItem for listViewSessions
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private ListViewItem GenerateListViewSessionItem(Patient patient, int index)
        {
            if (patient.Sessions[index].ScheduledSession && patient.Sessions[index].Task.FileName != "")
                return new ListViewItem(new string[] {
                    "Scheduled",
                    "",
                    "",
                    patient.Sessions[index].Task.FileName + " - " + Task.GetEnumDescription(patient.Sessions[index].Task.TaskType),
                    index.ToString()}, listViewGroup_Scheduled);
            else if (patient.Sessions[index].ScheduledSession && patient.Sessions[index].Task.FileName == "")
                return new ListViewItem(new string[] {
                    "Scheduled",
                    "",
                    "",
                    "Not Selected",
                    index.ToString()}, listViewGroup_Scheduled);
            else
                return new ListViewItem(new string[] {
                    patient.Sessions[index].SessionStartTime.ToShortDateString(),
                    patient.Sessions[index].SessionStartTime.ToShortTimeString(),
                    patient.Sessions[index].Duration.Minutes + ":" + patient.Sessions[index].Duration.Seconds,
                    patient.Sessions[index].Task.FileName + " - " + Task.GetEnumDescription(patient.Sessions[index].Task.TaskType),
                    index.ToString()
            }, listViewGroup_Completed);

        }

        /// <summary>
        /// Returns the session ID of the selected session in the list of sessions
        /// </summary>
        /// <returns></returns>
        public int GetSelectedSessionID()
        {
            //get the hidden id number embeded after the last column (rember it is a zero based index)
            return Int32.Parse(listViewSessions.SelectedItems[0].SubItems[listViewSessions.Columns.Count].Text);
        }
        #endregion
        #endregion

        #region Actions
        private void listViewSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
            if (listViewSessions.SelectedItems.Count == 1)
            {
                if (GetSelectedSessionID() == -1)//new session
                    buttonNewSession_Click(null, null);
                else
                {
                    _selectedSessionIndex = GetSelectedSessionID();
                    ShowSessionDetailsScreen();
                    ShowSessionBrowseControlButtons();
                    session_Details_Control.ReadOnlyNotes = true;
                }
            }

        }

        private void listViewSessions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _sessionSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_sessionSorter.Order == SortOrder.Ascending)
                {
                    _sessionSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _sessionSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _sessionSorter.SortColumn = e.Column;
                _sessionSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewSessions.Sort();
        }
        private void listViewSessions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSessions.SelectedItems.Count == 1)
            {
                _selectedSessionIndex = GetSelectedSessionID();
                if (_selectedSessionIndex == -1)
                    buttonNewSession_Click(null, null);
                else
                    buttonOpen_Click(null, null);
            }
        }
        /// <summary>
        /// Goes back to the previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (patient_Detials_Control.Visible)
            {
                ClosePatientScreen();
            }
            else if (session_Details_Control.Visible)
            {
                ShowPatientDetailsScreen();
            }
            else if (create_Edit_Session_Control.Visible)
            {
                if (create_Edit_Session_Control.ShowDetailedTaskPreview)
                    create_Edit_Session_Control.ShowDetailedTaskPreview = false;
                else
                    ShowSessionDetailsScreen();
            }
        }
        /// <summary>
        /// Opens a session or selects a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (create_Edit_Session_Control.Visible)
            {
                Create_Edit_Session_Control_TaskSelect(create_Edit_Session_Control.SelectedTask);
            }
            else
                ShowSessionDetailsScreen();
        }
        /// <summary>
        /// Duplicates the current session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            CreateNewSession(_patient.Sessions[GetSelectedSessionID()].Task);
        }
        /// <summary>
        /// Creates a new session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewSession_Click(object sender, EventArgs e)
        {
            CreateNewSession();
        }
        /// <summary>
        /// Deletes a session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (CurrentClinician.EditSessions)
            {
                if (MessageBox.Show("Are you sure you want to delete the session?", "StroMoHab Client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _selectedSessionIndex = GetSelectedSessionID();
                    if (_patient.Sessions[_selectedSessionIndex].MotionCaptureDataRecorded) // If it has MC data - remove it from disk
                        _remote_DataManager.ClientDeleteSession(_selectedPatientIndex, _selectedSessionIndex);
                    _patient.Sessions.RemoveAt(_selectedSessionIndex);
                    _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
                    _remote_DataManager.ClientRequestUpdatedPatientList();
                    session_Details_Control.Visible = false;
                    patient_Detials_Control.Visible = true;
                    FillInFields();
                }
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        /// <summary>
        /// Generates a report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReport_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Runs a session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRunSession_Click(object sender, EventArgs e)
        {
            if (CurrentClinician.RunSessions)
            {
                _selectedSessionIndex = GetSelectedSessionID();
                if (_patient.Sessions[_selectedSessionIndex].Task.FileName != "")
                {
                    //Todo instead of calling Start and stop here, open up Run_Review_Session_Form, set the task and then let it control things
                    if (_remote_DataManager.RecordingStatus)
                    {

                        StopRecording();
                    }
                    else
                    {
                        _patient.Sessions[_selectedSessionIndex].Clinician = CurrentClinician.NameString;
                        _remote_DataManager.ClientUpdatePatient(_patient, _selectedPatientIndex);
                        _remote_DataManager.ClientPrepareToRecordNewSession(_selectedPatientIndex, _selectedSessionIndex);

                        //Hide the main form, load the run_review_session form and wait for it to return
                        OnHideForm(true);
                        Run_Review_Session_Form form = new Run_Review_Session_Form();
                        form.CurrentSession = _patient.Sessions[_selectedSessionIndex];
                        if (form.ShowDialog() == DialogResult.Cancel) // showdialog rather than show so that we can get data back from it using form.Something
                        {
                            StopRecording();
                        }
                        else
                        {

                            _remote_DataManager.ClientRequestUpdatedPatientList();
                            _patient = _remote_DataManager.PatientList[_selectedPatientIndex];
                            FillInFields();
                        }
                        form.Dispose();
                        OnHideForm(false);
                    }
                }
                else
                    MessageBox.Show("You must select a Task before you can run the session.\nSelect the session you want to run, click Edit, and then select a task", "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StopRecording()
        {
            _remote_DataManager.ClientStopRecord();
            _remote_DataManager.ClientRequestUpdatedPatientList();
            _patient = _remote_DataManager.PatientList[_selectedPatientIndex];
            FillInFields();
        }
        /// <summary>
        /// Reviews a session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReviewSession_Click(object sender, EventArgs e)
        {
            if (_patient.Sessions[GetSelectedSessionID()].MotionCaptureDataRecorded && _patient.Sessions[GetSelectedSessionID()].SubSessions.Count != 0)
            {
                //Todo instead of calling Start and stop here, open up Run_Review_Session_Form, set the task and then let it control things
                if (_remote_DataManager.PlaybackStatus)
                {
                    StopPlayback();
                }
                else
                {
                    _remote_DataManager.ClientOpenSessionForPlayback(_selectedPatientIndex, GetSelectedSessionID(), 0);
                    _remote_DataManager.ClientRequestUpdatedPatientList();
                    _patient = _remote_DataManager.PatientList[_selectedPatientIndex];
                    if (_patient.Sessions[GetSelectedSessionID()].MotionCaptureDataRecorded == false)
                        FillInFields();
                    else
                    {
                        OnHideForm(true);
                        Run_Review_Session_Form form = new Run_Review_Session_Form();
                        form.CurrentSession = _patient.Sessions[_selectedSessionIndex];
                        if (form.ShowDialog() == DialogResult.Cancel) // showdialog rather than show so that we can get data back from it using form.Something
                        {
                            StopPlayback();
                        }
                        form.Dispose();
                        OnHideForm(false);
                    }
                    
                }
            }

        }

        private void StopPlayback()
        {
            _remote_DataManager.ClientStopPlayback();
        }
        /// <summary>
        /// Edits a session or task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {

            if (session_Details_Control.Visible)//session mode
            {
                if (listViewSessions.SelectedItems.Count != 0)
                {
                    if (CurrentClinician.EditSessions)
                    {
                        ShowSessionEditButtons();

                        //Edit session
                        create_Edit_Session_Control.LoadInTasks();
                        session_Details_Control.Visible = false;
                        create_Edit_Session_Control.Reset();
                        _selectedSessionIndex = GetSelectedSessionID();
                        if (_patient.Sessions[_selectedSessionIndex].Task.FileName != "")
                            create_Edit_Session_Control.DelayedSelectTask(_patient.Sessions[_selectedSessionIndex].Task);

                        create_Edit_Session_Control.Visible = true;


                        //Save and update
                    }
                    else
                        MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (create_Edit_Session_Control.Visible) // session edit
            {
                if (CurrentClinician.EditTasks)
                {
                    LoadTaskDeisnger(create_Edit_Session_Control.SelectedTask);
                }
                else
                    MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else // patient mode
            {
                if (CurrentClinician.EditPatients)
                {



                    //edit patient
                }
                else
                    MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        #region Task Designer
        private void LoadTaskDeisnger(Task task)
        {
            MessageBox.Show("Edit the task in the Task Desinger.\nOnce finished, save the task and close the Task Designer.", "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //Todo - connect to task designer and pass it task (if task==null then it should make a new task based on the default task)
        }

        private void TaskDesignerFinsished()
        {
            //todo write code to work out when the task designer has finished and then call these methods to update the list of tasks
            create_Edit_Session_Control.Reset();
            create_Edit_Session_Control.LoadInTasks();
            create_Edit_Session_Control.ShowDetailedTaskPreview = false;
        }
        #endregion
    }
}
