using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        private string _userName;

        public AddPatientWindow(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDateOfBirth.SelectedDate != null)
            {
                Stromohab_DataAccessLayer.DataAccessLayer.AddPatient(txtFirstNames.Text, txtLastName.Text,
                                                                     datePickerDateOfBirth.SelectedDate.Value,
                                                                     comboBxGender.Text, txtContactTelephone.Text,
                                                                     _userName);
            }
            //PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();

            //patientsTableAdapter.Insert(txtFirstNames.Text, txtLastName.Text,
            //                            Convert.ToDateTime(datePickerDateOfBirth.Text), comboBxGender.Text,
            //                            txtContactTelephone.Text, _userName);

            Close();
        }
    }
}
