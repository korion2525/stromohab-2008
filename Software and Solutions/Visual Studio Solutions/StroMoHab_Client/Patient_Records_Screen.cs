using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Communication;
using StroMoHab_Remote_DataManager;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    /// <summary>
    /// Provides a screen for the clinician to manage patients
    /// </summary>
    public partial class Patient_Records_Screen : UserControl
    {
        #region Member Variables        
        /// <summary>
        /// Delegate to add items to the List from the worker thread
        /// </summary>
        /// <param name="patient"></param>
        delegate void AddFoundPatientCallback(ListViewItem patient);
        /// <summary>
        /// Used for sorting the list
        /// </summary>
        private ListViewColumnSorter _patientSorter;
        /// <summary>
        /// Contains all of the items
        /// </summary>
        private List<ListViewItem> _listViewItems = new List<ListViewItem>();
        /// <summary>
        /// Remote connection to the server
        /// </summary>
        private Patient_Remote_DataManager _Remote_Data_Manager = new Patient_Remote_DataManager();
        /// <summary>
        /// Selected patient
        /// </summary>
        private int _selectedPatientIndex = 0;
        #endregion

        #region Constructor
        public Patient_Records_Screen()
        {
            InitializeComponent();

            if (TCPProcessor.ConnectedToServer)
                _Remote_Data_Manager = (Patient_Remote_DataManager)Activator.GetObject(typeof(Patient_Remote_DataManager), TCPProcessor.BuildServerRemotingString(8005, "PatientRemoteDataManagerConnection"));
           


            //Setup the sorter
            _patientSorter = new ListViewColumnSorter();
            this.listViewPatients.ListViewItemSorter = _patientSorter;

            listViewPatients.Groups.Add(listViewGroup_New);
            listViewPatients.Groups.Add(listViewGroup_Patients);


            //Setup background worker events
            backgroundWorkerSearcher.DoWork += new DoWorkEventHandler(backgroundWorkerSearcher_DoWork);
            backgroundWorkerSearcher.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerSearcher_RunWorkerCompleted);
            backgroundWorkerLoader.DoWork += new DoWorkEventHandler(backgroundWorkerLoader_DoWork);
            backgroundWorkerLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerLoader_RunWorkerCompleted);

            //Request an updated patient list and then retreive it
            _Remote_Data_Manager.ClientRequestUpdatedPatientList();
            //Load in the patient data into the open patient list view
            LoadInPatientData();

        }

 
        #endregion
        

        #region Events
        private void OnLogout()
        {
            if (Logout != null)
                Logout();
        }
        public delegate void LogoutEventHandler();
        public static event LogoutEventHandler Logout;
        private void OnOpenPatient(int patientIndex)
        {
            if (OpenPatient != null)
                OpenPatient(patientIndex);
        }
        public delegate void OpenPatientEventHandler(int patientIndex);
        public static event OpenPatientEventHandler OpenPatient;

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

        #region Background Worker
        //load
        void backgroundWorkerLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSetStatusLabelValue("Ready");
            OnSetProgressBarValue(100);
            listViewPatients.EndUpdate();
            listViewPatients.Visible = true;
            listViewPatients.ListViewItemSorter = _patientSorter;
            listViewPatients.SelectedItems.Clear();
            listViewPatients_SelectedIndexChanged(null, null);
            listViewPatients.Items.Add(new ListViewItem(new string[] { "New Patient", "", "", "", "", "", "", "", "-1" }, listViewGroup_New));
            listViewPatients.Items[listViewPatients.Items.Count -1].ForeColor = Properties.Settings.Default.ListViewNewEntryColor;
            listViewPatients.Items[listViewPatients.Items.Count - 1].Font = Properties.Settings.Default.ListViewNewEntryFont;
            listViewPatients.Items[listViewPatients.Items.Count - 1].ToolTipText = "Click to create a new patient";
        }

        void backgroundWorkerLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            AddFoundPatientCallback d = new AddFoundPatientCallback(AddFoundPatient);

            OnSetStatusLabelValue("Loading Patients From Server...");

            for (int j = 0; j < _Remote_Data_Manager.PatientList.Count; j++)
            {
                _listViewItems.Add(GenerateListViewPatientItem(_Remote_Data_Manager.PatientList[j], j));
                OnSetProgressBarValue(j * 100 / _Remote_Data_Manager.PatientList.Count);
            }


            OnSetStatusLabelValue("Processing Patients...");
            for (int i = 0; i < _listViewItems.Count; i++)
            {
                this.Invoke(d, new object[] { _listViewItems[i] });
                OnSetProgressBarValue(i * 100 / _listViewItems.Count);
            }

        }
        void backgroundWorkerLoader_DoWorkJustAddToList(object sender, DoWorkEventArgs e)
        {

            AddFoundPatientCallback d = new AddFoundPatientCallback(AddFoundPatient);
            OnSetStatusLabelValue("Processing Patients...");
            for (int i = 0; i < _listViewItems.Count; i++)
            {
                this.Invoke(d, new object[] { _listViewItems[i] });
                OnSetProgressBarValue(i * 100 / _listViewItems.Count);
            }

        }
        //search
        void backgroundWorkerSearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonSearch.Enabled = true;
            OnSetProgressBarValue(100);
            OnSetStatusLabelValue("Ready");
            this.listViewPatients.EndUpdate();
            this.listViewPatients.ListViewItemSorter = _patientSorter;         

        }

        void backgroundWorkerSearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            OnSetStatusLabelValue("Searching...");
            this.listViewPatients.ListViewItemSorter = null;
            AddFoundPatientCallback d = new AddFoundPatientCallback(AddFoundPatient);
            List<ListViewItem> _temp = new List<ListViewItem>();


            //build up the search terms
            string split = " ";
            string[] initialSsearchTerms = textBoxSearch.Text.Split(split.ToCharArray());
            List<string> tempSearchTerms = new List<string>();
            string tempSearchTerm = "";
            Postcode postcode = new Postcode();
            //check all but the last for a post code
            for (int i = 0; i < initialSsearchTerms.Length -1; i++)
            {
                //If its a valid
                if (Postcode.TryParse(initialSsearchTerms[i] + initialSsearchTerms[i + 1], out postcode, true))
                {
                    tempSearchTerm = initialSsearchTerms[i] + " "+ initialSsearchTerms[i+1];
                    i++;
                }
                else
                    tempSearchTerm = initialSsearchTerms[i];
                tempSearchTerms.Add(tempSearchTerm);
            }
            //if a post code has not been found the add the last term
            if (postcode.InCode == null)
            {
                tempSearchTerm = initialSsearchTerms[initialSsearchTerms.Length - 1];
                tempSearchTerms.Add(tempSearchTerm);
            }
            else if (!postcode.InCode.StartsWith(initialSsearchTerms[initialSsearchTerms.Length - 1])) // if one has been found but doesn't match then add last term
            {
                tempSearchTerm = initialSsearchTerms[initialSsearchTerms.Length - 1];
                tempSearchTerms.Add(tempSearchTerm);
            }
            string[] searchTerms = tempSearchTerms.ToArray();


            //search for the terms, each term has to be found in an item
            bool matchedTerm = false;
            for (int i = 0; i < _listViewItems.Count; i++)
            {
                foreach (string term in searchTerms)
                {
                    matchedTerm = false;
                    for (int j = 0; j < _listViewItems[i].SubItems.Count; j++)
                    {
                        if (_listViewItems[i].SubItems[j].Text.StartsWith(term))
                        {
                            matchedTerm = true;
                        }
                    }
                    if (!matchedTerm)
                        break;
                }
                if (matchedTerm)
                {
                    this.Invoke(d, new object[] { _listViewItems[i] });
                    OnSetProgressBarValue(i * 100 / _listViewItems.Count);
                }
                
            }
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads in the patient data into the list view table used to open a patient
        /// </summary>
        public void LoadInPatientData()
        {
            _listViewItems.Clear();
            listViewPatients.Items.Clear();
            listViewPatients.BeginUpdate();
            listViewPatients.Visible = false;
            backgroundWorkerLoader.RunWorkerAsync();
        }

        public void RefreshPatientDetails()
        {
            patient_Detials_Control.Patient = _Remote_Data_Manager.PatientList[_selectedPatientIndex];
        }

        /// <summary>
        /// Converts patient data into a form suitable for a ListView
        /// </summary>
        /// <param name="patient">The Patient</param>
        /// <param name="ID">An ID number to trace the item back to the patient</param>
        /// <returns></returns>
        private ListViewItem GenerateListViewPatientItem(Patient patient, int ID)
        {
            return new ListViewItem(new string[] {
                patient.PatientID,
                patient.Title.ToString(),
                patient.LastName,
                patient.FirstName,
                patient.Gender.ToString(),
                patient.DOB.ToShortDateString(),
                patient.ContactNumber.Number,
                patient.Address.Postcode.ToString(),
                ID.ToString() // Hide the id number after the main data so that the item can be traced back
            },listViewGroup_Patients);

        }

        /// <summary>
        /// gets the hidden patient id from the selected patient
        /// </summary>
        /// <returns></returns>
        public int GetSelectedPatientID()
        {
            //get the hidden id number embeded after the last column (rember it is a zero based index)
            return Int32.Parse(listViewPatients.SelectedItems[0].SubItems[8].Text);
        }

        private bool StartsWith(string mainString, string subString)
        {
            if (subString == "")
                return true;
            if (mainString.Length >= subString.Length)
            {
                for (int i = 0; i < subString.Length; i++)
                {
                    if (mainString[i] != subString[i])
                        return false;
                }
                return true;
            }
            else
                return false;

        }
        /// <summary>
        /// Clears any selected patients in the list
        /// </summary>
        public void ClearSelected()
        {
            listViewPatients.SelectedItems.Clear();
        }

        private void AddFoundPatient(ListViewItem patient)
        {
            patient.Group = listViewGroup_Patients;
            listViewPatients.Items.Add(patient);
        }
        private void DeleteListViewItem(ListViewItem value)
        {
            //_hiddenItems.Add(value);
            listViewPatients.Items.Remove(value);
        }
        #endregion

        #region Actions

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout.", "StroMoHab Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listViewPatients.SelectedItems.Clear();
                OnLogout();
            }
        }
        /// <summary>
        /// Performs sorting when the patient column headers are clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewPatients_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _patientSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_patientSorter.Order == SortOrder.Ascending)
                {
                    _patientSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _patientSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _patientSorter.SortColumn = e.Column;
                _patientSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewPatients.Sort();

        }

        private void listViewPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOpen.Enabled = false;
            buttonDelete.Enabled = false;
            buttonEdit.Enabled = false;
            patient_Detials_Control.ShowDetails = false;
            if (listViewPatients.SelectedItems.Count == 1)
            {
                if (GetSelectedPatientID() == -1)
                {
                    buttonNew_Click(null, null);
                }
                else
                {

                    buttonOpen.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonEdit.Enabled = true;
                    patient_Detials_Control.Patient = _Remote_Data_Manager.PatientList[GetSelectedPatientID()];
                    patient_Detials_Control.ShowDetails = true;
                }
            }
            if (listViewPatients.SelectedItems.Count > 1)
            {
                buttonOpen.Enabled = true;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            _selectedPatientIndex = GetSelectedPatientID();
            OnOpenPatient(_selectedPatientIndex);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (!textBoxSearch.Text.StartsWith("Search"))
            {
                if (textBoxSearch.Text.Trim() != "")
                {
                    buttonSearch.Enabled = false;
                    listViewPatients.Items.Clear();
                    this.listViewPatients.BeginUpdate();

                    backgroundWorkerSearcher.RunWorkerAsync();
                }
                else
                {
                    //switch to quick loader
                    backgroundWorkerLoader.DoWork -= new DoWorkEventHandler(backgroundWorkerLoader_DoWork);
                    backgroundWorkerLoader.DoWork += new DoWorkEventHandler(backgroundWorkerLoader_DoWorkJustAddToList);
                    listViewPatients.Items.Clear();
                    this.listViewPatients.BeginUpdate();

                    backgroundWorkerLoader.RunWorkerAsync();
                    System.Threading.Thread.Sleep(20);
                    //switch back
                    backgroundWorkerLoader.DoWork += new DoWorkEventHandler(backgroundWorkerLoader_DoWork);
                    backgroundWorkerLoader.DoWork -= new DoWorkEventHandler(backgroundWorkerLoader_DoWorkJustAddToList);
                }
            }

        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            if(CurrentClinician.EditPatients)
            {
                Create_Edit_Patient_Form form = new Create_Edit_Patient_Form();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Patient newPatient = form.Patient;
                    _Remote_Data_Manager.ClientSaveNewPatient(newPatient);
                    _Remote_Data_Manager.ClientRequestUpdatedPatientList();

                    //Load in the patient data into the open patient list view
                    LoadInPatientData();
                }
                form.Dispose();
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonSearch_Click(null, null);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (CurrentClinician.EditPatients)
            {
                _selectedPatientIndex = GetSelectedPatientID();
                string patientDetails = listViewPatients.SelectedItems[0].SubItems[1].Text + " " + listViewPatients.SelectedItems[0].SubItems[3].Text + " " + listViewPatients.SelectedItems[0].SubItems[2].Text + "\nNHS Number : " + listViewPatients.SelectedItems[0].Text;
                if (MessageBox.Show("Are you sure you want to delete the patient.\nThis can not be un-done.\n\n" + patientDetails, "StroMoHab Client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _Remote_Data_Manager.ClientDeletePatient(_selectedPatientIndex);
                    _Remote_Data_Manager.ClientRequestUpdatedPatientList();
                    LoadInPatientData();
                }
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (CurrentClinician.EditPatients)
            {
                Create_Edit_Patient_Form form = new Create_Edit_Patient_Form();
                _selectedPatientIndex = GetSelectedPatientID();
                form.Patient = _Remote_Data_Manager.PatientList[_selectedPatientIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {

                    Patient updatedPatient = form.Patient;
                    _Remote_Data_Manager.ClientUpdatePatient(updatedPatient, _selectedPatientIndex);
                    _Remote_Data_Manager.ClientRequestUpdatedPatientList();

                    LoadInPatientData();
                }
                form.Dispose();
            }
            else
                MessageBox.Show(StroMoHab_Client.Properties.Settings.Default.InvalidPermissionsString, "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void listViewPatients_DoubleClick(object sender, EventArgs e)
        {
            if (listViewPatients.SelectedItems.Count == 1)
            {
                if (GetSelectedPatientID() == -1)
                    buttonNew_Click(null, null);
                else
                    buttonOpen_Click(null, null);
            }
        }
        private void textBoxSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSearch.Text.StartsWith("Search"))
                textBoxSearch.Text = "";
        }
        #endregion

        #region Propterties
        /// <summary>
        /// Gets or sets the current clinician - used to determine permissions and to get the name of the last person to make an edit
        /// </summary>
        public Clinician CurrentClinician { get; set; }

        /// <summary>
        /// The list view containing all the patients
        /// </summary>
        public ListView PatientListView
        {
            get
            {
                return this.listViewPatients;
            }
            set
            {
                this.listViewPatients = value;
            }
        }
        public int PatientIndex
        {
            get { return _selectedPatientIndex; }
        }
        #endregion      

       
    }
}
