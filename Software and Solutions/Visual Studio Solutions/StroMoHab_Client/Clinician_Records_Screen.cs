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
    /// A control to manage clinicians
    /// </summary>
    public partial class Clinician_Records_Screen : UserControl
    {
        Clinician_Remote_DateManager _remote_DataManager = new Clinician_Remote_DateManager();
        private bool _justAdded = false;

        public Clinician_Records_Screen()
        {
            InitializeComponent();

            listViewClinicians.Groups.Add(listViewGroup_New);
            listViewClinicians.Groups.Add(listViewGroup_Special);
            listViewClinicians.Groups.Add(listViewGroup_Clinicians);

            if(TCPProcessor.ConnectedToServer)
            {
                _remote_DataManager = (Clinician_Remote_DateManager)Activator.GetObject(typeof(Clinician_Remote_DateManager), TCPProcessor.BuildServerRemotingString(8005, "ClinicianRemoteDataManagerConnection"));
            }
        }

        

        #region Events
        private void OnLogout()
        {
            if (Logout != null)
                Logout();
        }
        public delegate void LogoutEventHandler();
        public static event LogoutEventHandler Logout;
        #endregion

       

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout.", "StroMoHab Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listViewClinicians.SelectedItems.Clear();
                OnLogout();
            }
        }

        /// <summary>
        /// Updates the list of clinicians
        /// </summary>
        public void UpdateClinicians()
        {
            _remote_DataManager.ClientRequestUpdatedClinicianList();
            listViewClinicians.SelectedItems.Clear();
            listViewClinicians.Items.Clear();
            listViewClinicians.Items.Add(new ListViewItem(new string[] { "New Clinician", "", "", "", "", "", "", "","-1" },listViewGroup_New));
            listViewClinicians.Items[0].ForeColor = Properties.Settings.Default.ListViewNewEntryColor;
            listViewClinicians.Items[0].Font = Properties.Settings.Default.ListViewNewEntryFont;
            listViewClinicians.Items[0].ToolTipText = "Click to create a new clinician";

            int i = 0;
            foreach(Clinician clinician in _remote_DataManager.Clinicians)
            {
                listViewClinicians.Items.Add(GenerateListViewClinicianItem(clinician,i));
                i++;
            }

        }
        /// <summary>
        /// Gets the id of the selected clinician
        /// </summary>
        /// <returns></returns>
        private int GetSelectedClinicianID()
        {
            return Int32.Parse(listViewClinicians.SelectedItems[0].SubItems[8].Text);
        }
        /// <summary>
        /// Generates a list view item for the given clinician
        /// </summary>
        /// <param name="clinician"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private ListViewItem GenerateListViewClinicianItem(Clinician clinician, int index)
        {
            if (clinician.EditClinicians)
            {
                return new ListViewItem(new string[] {
                    clinician.LoginName,
                    clinician.NameString,
                    clinician.EditClinicians.ToString(),
                    clinician.EditPatients.ToString(),
                    clinician.EditSessions.ToString(),
                    clinician.EditTasks.ToString(),
                    clinician.ScheduleSessions.ToString(),
                    clinician.RunSessions.ToString(),
                    index.ToString(),
                }, listViewGroup_Special);
            }
            else
            {
                return new ListViewItem(new string[] {
                    clinician.LoginName,
                    clinician.NameString,
                    clinician.EditClinicians.ToString(),
                    clinician.EditPatients.ToString(),
                    clinician.EditSessions.ToString(),
                    clinician.EditTasks.ToString(),
                    clinician.ScheduleSessions.ToString(),
                    clinician.RunSessions.ToString(),
                    index.ToString(),
                }, listViewGroup_Clinicians);
            }
        }

        /// <summary>
        /// deals with an item being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewClinicians_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_justAdded == false) // this method is getting called twice when adding and i dont know why! so for now check to see if a clinican has just been added
            { //it might be todo with clearing selected items but it doesn't normally do it
                buttonDelete.Enabled = false;
                buttonEdit.Enabled = false;
                if (listViewClinicians.SelectedItems.Count == 1)
                {
                    if (GetSelectedClinicianID() == -1)
                    {
                        buttonNew_Click(null, null);
                    }
                    else
                    {
                        if (_remote_DataManager.Clinicians[GetSelectedClinicianID()].EditClinicians == false) // can't delete admin account
                            buttonDelete.Enabled = true;
                        buttonEdit.Enabled = true;
                    }
                }
            }
            else
                _justAdded = false;
        }
        /// <summary>
        /// Creates a new clinician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Create_Edit_Clinician_Form form = new Create_Edit_Clinician_Form();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Clinician clinician = form.CurrentClinician;
                _remote_DataManager.ClientSaveNewClinician(clinician);
                UpdateClinicians();                
            }
            else
                listViewClinicians.SelectedItems.Clear();
            _justAdded = true;
            form.Dispose();
            buttonEdit.Enabled = false;
            buttonDelete.Enabled = false;
        }
        /// <summary>
        /// edits a clinician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Create_Edit_Clinician_Form form = new Create_Edit_Clinician_Form();
            form.CurrentClinician = _remote_DataManager.Clinicians[GetSelectedClinicianID()];
            if (form.ShowDialog() == DialogResult.OK)
            {
                Clinician clinician = form.CurrentClinician;
                _remote_DataManager.ClientUpdateClinician(clinician);
                _remote_DataManager.ClientRequestUpdatedClinicianList();
                UpdateClinicians();
            }
        }
        /// <summary>
        /// deletes a clinician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the clinician.\nThis can not be un-done.", "StroMoHab Client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _remote_DataManager.ClientDeleteClinician(_remote_DataManager.Clinicians[GetSelectedClinicianID()]);
                _remote_DataManager.ClientRequestUpdatedClinicianList();
                UpdateClinicians();
            }
        }
        /// <summary>
        /// deals with double clicking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewClinicians_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewClinicians.SelectedItems.Count == 1)
            {
                if (GetSelectedClinicianID() == -1)
                    buttonNew_Click(null, null);
                else
                    buttonEdit_Click(null, null);
            }
        }


    }
}
