using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;


namespace StroMoHab_Client
{
    /// <summary>
    /// The main interface
    /// </summary>
    public partial class GUI : Form
    {
        #region Member Variables
        private FormWindowState _previousState = FormWindowState.Normal;
        private Clinician _clinician;
        delegate void SetProgressBarValueCallback(int value);
        delegate void SetStatusLabelValueCallback(string value);
        Clinician_Login_Screen clinican_Login_Screen = new Clinician_Login_Screen();
        Patient_Records_Screen patient_Records_Screen; //= new Patient_Records_Screen();
        Patient_Screen patient_Screen; //= new Patient_Screen();
        Clinician_Records_Screen clinician_Records_Screen;
        Timer _progressBarHiderTimer = new Timer();
        #endregion

        /// <summary>
        /// The contructor
        /// </summary>
        public GUI()
        {
            InitializeComponent();

            SendMCECommand.StartCameras();

            //Set screen docks, add and hide them
            clinican_Login_Screen.Dock = DockStyle.Fill;
            this.Controls.Add(clinican_Login_Screen);


            _progressBarHiderTimer.Interval = 500;
            _progressBarHiderTimer.Tick += new EventHandler(_progressBarHiderTimer_Tick);

            Patient_Screen.HideForm += new Patient_Screen.HideFormEventHandler(Patient_Screen_HideForm);

            //Register for events for screen load and close
            Clinician_Login_Screen.Login += new Clinician_Login_Screen.LoginEventHandler(Clinician_Login_Screen_Login);
            Patient_Records_Screen.Logout += new Patient_Records_Screen.LogoutEventHandler(Patient_Records_Screen_Logout);
            Patient_Records_Screen.OpenPatient += new Patient_Records_Screen.OpenPatientEventHandler(Patient_Records_Screen_OpenPatient);
            Patient_Screen.ClosePatient += new Patient_Screen.ClosePatientEventHandler(Patient_Screen_ClosePatient);
            Clinician_Records_Screen.Logout += new Clinician_Records_Screen.LogoutEventHandler(Clinician_Records_Screen_Logout);

            //register for events for status bar updates
            Patient_Records_Screen.SetProgressBarValue += new Patient_Records_Screen.SetProgressBarValueEventHandler(Patient_Records_Screen_SetProgressBarValue);
            Patient_Records_Screen.SetStatusLabelValue += new Patient_Records_Screen.SetStatusLabelValueEventHandler(Patient_Records_Screen_SetStatusLabelValue);
            Patient_Screen.SetProgressBarValue += new Patient_Screen.SetProgressBarValueEventHandler(Patient_Screen_SetProgressBarValue);
            Patient_Screen.SetStatusLabelValue += new Patient_Screen.SetStatusLabelValueEventHandler(Patient_Screen_SetStatusLabelValue);
        }

        void Patient_Screen_HideForm(bool value)
        {
            if (value)
            {
                _previousState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = _previousState;
            }
        }


        #region Status Bar Events, Callback, Methods
        /// <summary>
        /// Hides the progress bar after a short while
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _progressBarHiderTimer_Tick(object sender, EventArgs e)
        {
           toolStripProgressBar.Visible = false;
           _progressBarHiderTimer.Stop();
        }
        void Patient_Screen_SetStatusLabelValue(string value)
        {
            SetStatusLabelValueThreadSafe(value);
        }

        void Patient_Screen_SetProgressBarValue(int value)
        {
            SetProgressBarValueThreadSafe(value);
        }
        
        void Patient_Records_Screen_SetStatusLabelValue(string value)
        {
            SetStatusLabelValueThreadSafe(value);
        }
        void Patient_Records_Screen_SetProgressBarValue(int value)
        {
            SetProgressBarValueThreadSafe(value);
        }
        /// <summary>
        /// Sets the status bar progress bar value in a threadsafe way
        /// </summary>
        /// <param name="value"></param>
        private void SetProgressBarValueThreadSafe(int value)
        {
            SetProgressBarValueCallback d = new SetProgressBarValueCallback(SetProgressBarValue);
            this.Invoke(d, new object[] { value });
        }
        /// <summary>
        /// Sets the status bar progress bar value
        /// </summary>
        /// <param name="value"></param>
        private void SetProgressBarValue(int value)
        {
            if (value != toolStripProgressBar.Value)
            {
                if (value == 100)
                    _progressBarHiderTimer.Start();
                else
                {
                    _progressBarHiderTimer.Stop();
                    toolStripProgressBar.Visible = true;
                }

                toolStripProgressBar.Value = value;
            }
        }
        /// <summary>
        /// Sets the status bar text label in a threadsafe way
        /// </summary>
        /// <param name="value"></param>
        private void SetStatusLabelValueThreadSafe(string value)
        {
            SetStatusLabelValueCallback d = new SetStatusLabelValueCallback(SetStatusLabelValue);
            this.Invoke(d, new object[] { value });
        }
        /// <summary>
        /// Sets the status bar text label
        /// </summary>
        /// <param name="value"></param>
        private void SetStatusLabelValue(string value)
        {
            toolStripStatusLabel.Text = value;
        }
        #endregion

        #region Screen Load and Close

        void Patient_Screen_ClosePatient()
        {
            patient_Records_Screen.Visible = true;
            clinican_Login_Screen.Visible = false;
            patient_Screen.Visible = false;
            patient_Records_Screen.RefreshPatientDetails();
        }

        void Clinician_Records_Screen_Logout()
        {
            clinican_Login_Screen.ClearFields();
            clinican_Login_Screen.Visible = true;
            clinician_Records_Screen.Visible = false;
        }

        void Patient_Records_Screen_OpenPatient(int patientIndex)
        {
            if (patient_Screen == null)
            {
                patient_Screen = new Patient_Screen();
                patient_Screen.Dock = DockStyle.Fill;
                this.Controls.Add(patient_Screen);
            }
            
            patient_Records_Screen.Visible = false;
            patient_Screen.Visible = true;
            clinican_Login_Screen.Visible = false;
            patient_Screen.CurrentClinician = _clinician;
            patient_Screen.PatientIndex = patientIndex;

        }

        void Patient_Records_Screen_Logout()
        {
            clinican_Login_Screen.ClearFields();
            clinican_Login_Screen.Visible = true;
            patient_Records_Screen.Visible = false;
        }

        void Clinician_Login_Screen_Login()
        {
            _clinician = clinican_Login_Screen.CurrentClinician;
            if (_clinician.EditClinicians) //if they can edit clinicians
            {
                if (clinician_Records_Screen == null)
                {
                    clinician_Records_Screen = new Clinician_Records_Screen();
                    clinician_Records_Screen.Dock = DockStyle.Fill;
                    this.Controls.Add(clinician_Records_Screen);
                }
                clinician_Records_Screen.UpdateClinicians();
                clinican_Login_Screen.Visible = false;
                clinician_Records_Screen.Visible = true;

            }
            else
            {

                if (patient_Records_Screen == null)
                {
                    SetStatusLabelValue("Loading Patients From Server...");
                    patient_Records_Screen = new Patient_Records_Screen();
                    patient_Records_Screen.Dock = DockStyle.Fill;
                    this.Controls.Add(patient_Records_Screen);

                }
                patient_Records_Screen.CurrentClinician = _clinician;
                clinican_Login_Screen.Visible = false;
                patient_Records_Screen.Visible = true;

                if (patient_Screen != null)
                {
                    patient_Screen.CurrentClinician = _clinician;
                    patient_Screen.Visible = false;
                }
            }
        }
        #endregion

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (patient_Screen != null)
            {
                if (patient_Screen.Visible)
                {
                    if (MessageBox.Show("Are you sure you want to exit?\nIf you have moddifed the current patient, please close the patient first.", "A Patient is still open", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
