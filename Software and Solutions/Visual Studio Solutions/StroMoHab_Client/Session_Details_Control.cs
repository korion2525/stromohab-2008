using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    public partial class Session_Details_Control : UserControl
    {
        private Session _session = new Session();
   
        /// <summary>
        /// A control to display details about a Session
        /// </summary>
        public Session_Details_Control()
        {
            InitializeComponent();

        }
        public bool ReadOnlyNotes
        {
            set
            { 
                richTextBoxNotes.ReadOnly = value;
                if (value)
                    richTextBoxNotes.BackColor = Color.LightGray;
                else
                    richTextBoxNotes.BackColor = Color.White;
            
            }
        }

        public Session Session
        {
            set {
                _session = value;
                if(_session !=null)
                    FillInFields();
            }
            get
            {
                return _session;
            }
        }
        private void FillInFields()
        {
            task_Preview_Control.Task = _session.Task;
            //Details Section

            if(_session.Clinician != null)
                labelClinician.Text = "Clinician : " + _session.Clinician;

            if (_session.ScheduledSession)
            {
                labelDuration.Text = "Duration : N/A";
                labelDate.Text = "Scheduled on : " + _session.SessionSecheduledTime;
                labelTime.Text = "Time : N/A";
                labelMCData.Text = "MC Data Available : N/A";
            }
            else
            {
                if (_session.MotionCaptureDataRecorded)
                    labelMCData.Text = "MC Data Available : Yes";
                else
                    labelMCData.Text = "MC Data Available : No";
                labelDate.Text = "Date : " + _session.SessionStartTime.ToLongDateString();
                labelTime.Text = "Time : " + _session.SessionStartTime.ToShortTimeString();
                labelDuration.Text = "Duration : " + _session.Duration.Minutes + " Min " + _session.Duration.Seconds +" Seconds";
            }


            //Notes Sesion
            richTextBoxNotes.Text = _session.Notes;
        }

        private void richTextBoxNotes_Validating(object sender, CancelEventArgs e)
        {
            _session.Notes = richTextBoxNotes.Text;
        }
    }
}
