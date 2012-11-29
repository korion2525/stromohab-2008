using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    /// <summary>
    /// A form to create and edit a patient
    /// </summary>
    public partial class Create_Edit_Patient_Form : Form
    {
        private Patient _patient;
        public Create_Edit_Patient_Form()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;

            foreach (Patient.GenderType type in Enum.GetValues(typeof(Patient.GenderType)))
            {
                comboBoxGender.Items.Add(type);
            }

            foreach (Patient.TitleType type in Enum.GetValues(typeof(Patient.TitleType)))
            {
                comboBoxTitle.Items.Add(type);
            }

        }
        /// <summary>
        /// Gets or sets the patient
        /// </summary>
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                this.Text = "Edit Patient";
                FillInFieldsFromPatient();

            }
        }

        private void FillInFieldsFromPatient()
        {
            textBoxFirstName.Text = _patient.FirstName;
            textBoxLastName.Text = _patient.LastName;
            textBoxID.Text = _patient.PatientID;
            textBoxPostcode.Text = _patient.Address.Postcode.ToString();
            dateTimePicker.Value = _patient.DOB;
            comboBoxTitle.SelectedIndex = (int)_patient.Title;
            comboBoxGender.SelectedIndex = (int)_patient.Gender;
            textBoxContactNumber.Text = _patient.ContactNumber.Number;
        }
        

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string errorString = "You must fill in this field";
            errorProvider.Clear();

            //create the patient
            _patient = new Patient();
            
            

            //validate and add fields
                      

            

           
            if (comboBoxTitle.SelectedIndex != -1)
            {
                _patient.Title = (Patient.TitleType)comboBoxTitle.SelectedIndex;
            }
            else
            {
                errorProvider.SetError(comboBoxTitle, errorString);
                return;
            }
            if (textBoxLastName.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxLastName, errorString);
                return;
            }
            else
                _patient.LastName = textBoxLastName.Text;
            if (textBoxFirstName.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxFirstName, errorString);
                return;
            }
            else
                _patient.FirstName = textBoxFirstName.Text;

            if (textBoxID.Text.Trim() == "")
            {
                errorProvider.SetError(textBoxID, errorString);
                return;
            }
            else
                _patient.PatientID = textBoxID.Text;

            
            if (comboBoxGender.SelectedIndex != -1)
            {
                _patient.Gender = (Patient.GenderType)comboBoxGender.SelectedIndex;
            }
            else
            {
                errorProvider.SetError(comboBoxGender, errorString);
                return;
            }

            if (dateTimePicker.Value.Date == DateTime.Today)
            {
                errorProvider.SetError(dateTimePicker, errorString);
                return;
            }

            _patient.DOB = dateTimePicker.Value;

            
           

            Postcode validatedPostcode;
            bool postcodeResult = Postcode.TryParse(textBoxPostcode.Text, out validatedPostcode, true);
            if (postcodeResult == false)
            {
                errorProvider.SetError(textBoxPostcode, "Invalid Post Code");
                return;
            }
            else
                _patient.Address.Postcode = validatedPostcode;

            if (!_patient.ContactNumber.TryParse(textBoxContactNumber.Text))
            {
                errorProvider.SetError(textBoxContactNumber, "Invalid Number");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
