using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StroMoHab_Client
{
    /// <summary>
    /// A form to create and edit a clincian
    /// </summary>
    public partial class Create_Edit_Clinician_Form : Form
    {
        private StroMoHab_Objects.Objects.Clinician _clinician = new StroMoHab_Objects.Objects.Clinician();
        public Create_Edit_Clinician_Form()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            
        }

        public StroMoHab_Objects.Objects.Clinician CurrentClinician
        {
            get { return _clinician; }
            set
            {
                _clinician = value;
                if (_clinician != null)
                    FillInFields();
            }
        }

        private void FillInFields()
        {
            this.Text = "Edit Clinician";

            textBoxUserName.Text = _clinician.LoginName;
            textBoxUserName.Enabled = false;
            textBoxFirstName.Text = _clinician.FirstName;
            textBoxLastName.Text = _clinician.LastName;
            textBoxPassword.Text = "EDITCLINICIANMODE";
            textBoxPasswordCheck.Text = "EDITCLINICIANMODE";
            checkBoxC.Checked = _clinician.EditClinicians;
            checkBoxP.Checked = _clinician.EditPatients;
            checkBoxS.Checked = _clinician.EditSessions;
            checkBoxT.Checked = _clinician.EditTasks;
            checkBoxSS.Checked = _clinician.ScheduleSessions;
            checkBoxRS.Checked = _clinician.RunSessions;

            if (_clinician.CanEditDetails == false)
            {
                textBoxFirstName.Enabled = false;
                textBoxLastName.Enabled = false;
                checkBoxT.Enabled = false;
                checkBoxP.Enabled = false;
                checkBoxS.Enabled = false;
                checkBoxC.Enabled = false;
                checkBoxRS.Enabled = false;
                checkBoxSS.Enabled = false;
            }

        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (textBoxUserName.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxUserName, "You must fill in this field");
                return;
            }
            
            if (textBoxUserName.Text.Contains(" "))
            {
                errorProvider.SetError(textBoxUserName, "User Names cannot contain spaces");
                return;
            }

            if (textBoxFirstName.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxFirstName, "You must fill in this field");
                return;
            }

            if (textBoxLastName.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxLastName, "You must fill in this field");
                return;
            }

            if (textBoxPassword.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxPassword, "You must fill in this field");
                return;
            }

            if (textBoxPasswordCheck.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxPasswordCheck, "You must fill in this field");
                return;
            }
            if (textBoxPasswordCheck.Text.CompareTo(textBoxPassword.Text) != 0)
            {
                errorProvider.SetError(textBoxPasswordCheck, "Passwords must match");
                return;
            }


            _clinician.LoginName = textBoxUserName.Text;
            _clinician.FirstName = textBoxFirstName.Text;
            _clinician.LastName = textBoxLastName.Text;
            if (textBoxPassword.Text.Equals("EDITCLINICIANMODE") == false)
                _clinician.SetPassword(textBoxPassword.Text);
            _clinician.EditClinicians = checkBoxC.Checked;
            _clinician.EditPatients = checkBoxP.Checked;
            _clinician.EditSessions = checkBoxS.Checked;
            _clinician.EditTasks = checkBoxT.Checked;
            _clinician.RunSessions = checkBoxRS.Checked;
            _clinician.ScheduleSessions = checkBoxSS.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBoxS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxS.Checked == false)
                checkBoxSS.Checked = false;
        }

        private void checkBoxSS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSS.Checked)
                checkBoxS.Checked = true;
        }



    }
}
