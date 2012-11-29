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
    /// <summary>
    /// A control to display details about a Patient
    /// </summary>
    public partial class Patient_Detials_Control : UserControl
    {

        private Patient _patient;        
        private bool _showTextBox = true;

        /// <summary>
        /// The constructor
        /// </summary>
        public Patient_Detials_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a bool indicating if the text box should be shown
        /// </summary>
        public bool ShowTextBox
        {
            get { return _showTextBox; }
            set
            {
                _showTextBox = value;
                if (_showTextBox) // show the text box
                {
                    groupBoxNotes.Visible = true;

                }
                else // hide it an expand the details box to take up all the space
                {
                    groupBoxNotes.Visible = false; 
                    groupBoxDetails.Height = (groupBoxNotes.Location.Y + groupBoxNotes.Height) -groupBoxDetails.Location.Y;
                    groupBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                         | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
                }
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the details of the patient should be shown
        /// </summary>
        public bool ShowDetails
        {
            get { return groupBoxDetails.Controls[0].Visible; }
            set
            {
                foreach (Control c in groupBoxDetails.Controls)
                    c.Visible = value;
            }
        }
        /// <summary>
        /// Gets or sets the patient
        /// </summary>
        public Patient Patient
        {
            set
            {
                _patient = value;
                if(_patient!= null)
                    FillFields();
            }
            get
            {
                return _patient;
            }
        }

        /// <summary>
        /// Fills in the fields based on the patient set using the Patient property
        /// </summary>
        private void FillFields()
        {
           labelTitleName.Text = _patient.Title + " " + _patient.FirstName + " " + _patient.LastName;
           labelID.Text = "NHS Number : " + _patient.PatientID + "      Gender : " + _patient.Gender.ToString();
           labelAge.Text = "Date of Birth : " + _patient.DOB.ToShortDateString() + " (Aged " + _patient.AgeInYears + ")";
           labelAddress.Text = "Postcode : " + _patient.Address.Postcode;
           labelContact.Text = "Phone Number : " + _patient.ContactNumber.Number;
           labelSessions.Text = "Sessions Completed: " + _patient.NumberOfCompletedSessions + "/" + _patient.NumberOfSessions;
           labelLastSession.Text = "Last Session : " + _patient.LastSessionString;
           richTextBoxNotes.Text = _patient.Notes;
        }

        /// <summary>
        /// Saves the text box content when it looses focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBoxNotes_Validating(object sender, CancelEventArgs e)
        {
            _patient.Notes = richTextBoxNotes.Text;
        }

       
    }
}
