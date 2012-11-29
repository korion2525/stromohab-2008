using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Communication;
using System.Reflection;
using StroMoHab_Remote_DataManager;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    /// <summary>
    /// Provides a screen for the clinician to login
    /// </summary>
    public partial class Clinician_Login_Screen : UserControl
    {
        private string _userName = "";
        private Clinician_Remote_DateManager _remote_Data_Manager = new Clinician_Remote_DateManager();
        public Clinician_Login_Screen()
        {
            InitializeComponent();

            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            string version = assemName.Version.ToString();

            string statusMsg = "";
#if(DEBUG)
            statusMsg = "Running in DEBUG Mode!\n";
#endif
            statusMsg = statusMsg + "StroMoHab Client Version : " + version + "\n";
            if (TCPProcessor.ConnectedToServer)
                labelServer.Text = statusMsg + "Connected to the StroMoHab Server - IP Address : " + TCPProcessor.ServerIPAddress;
            else
            {
                labelServer.Text = statusMsg + "Not Connected to the StroMoHab Server!\nFunctionality will be severely limited!";
                labelServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }


            //login service
            if (TCPProcessor.ConnectedToServer)
            {
                _remote_Data_Manager = (Clinician_Remote_DateManager)Activator.GetObject(typeof(Clinician_Remote_DateManager), TCPProcessor.BuildServerRemotingString(8005, "ClinicianRemoteDataManagerConnection"));
            }
        }

        public void ClearFields()
        {
            textBoxPassword.Text = "";
            textBoxUser.Text = "";
        }

        public string UserName
        {
            get { return _userName; }
        }

        private void OnLogin()
        {
            if (Login != null)
                Login();
        }
        public delegate void LoginEventHandler();
        public static event LoginEventHandler Login;

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                // Set focus on control
                control.Focus();
                // Validate causes the control's Validating event to be fired,
                // if CausesValidation is True
                if (!Validate())
                {
                    return;
                }
            }

            if (ValidateUser(textBoxUser.Text, textBoxPassword.Text))
                OnLogin();
            else
            {
                errorProvider.SetError(textBoxPassword, "Invalid User Name or Password");
                errorProvider.SetError(textBoxUser, "Invalid User Name or Password");
            }
        }

        private void textBoxUser_Validating(object sender, CancelEventArgs e)
        {
            // Name is required
              if(textBoxUser.Text.Trim() == "" ) {
                errorProvider.SetError(textBoxUser,"A User Name is required");
                e.Cancel = true;
                return;
              }    
              // Name is Valid
              errorProvider.SetError(textBoxUser, "");

        }

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            // password is required
            if (textBoxPassword.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxPassword, "A Password is required");
                e.Cancel = true;
                return;
            }
            // Name is Valid
            errorProvider.SetError(textBoxPassword, "");
        }

        private void textBoxUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Control c = GetNextControl((Control)sender, true);
                if (c != null)
                    c.Focus();
            }

        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonLogin_Click(null, null);
            }
        }

        private void buttonManageUsers_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                // Set focus on control
                control.Focus();
                // Validate causes the control's Validating event to be fired,
                // if CausesValidation is True
                if (!Validate())
                {
                    return;
                }
            }
            
            if (ValidateUser(textBoxUser.Text,textBoxPassword.Text))
                ManageClinicians();
            else
                errorProvider.SetError(textBoxPassword, "Invalid Password");
        }

        private bool ValidateUser(string userName, string password)
        {
            _remote_Data_Manager.ClientRequestLogin(userName, password);
            if (_remote_Data_Manager.ValidatedClinician == null)
                return false;
            else
            {
                CurrentClinician = _remote_Data_Manager.ValidatedClinician;
                return true;
            }

        }

        /// <summary>
        /// Gets or sets the current clinician - used to determine permissions and to get the name of the last person to make an edit
        /// </summary>
        public Clinician CurrentClinician { get; set; }

        private void ManageClinicians()
        {
            MessageBox.Show("Not Yet Supported");

        }


       
    }
}
