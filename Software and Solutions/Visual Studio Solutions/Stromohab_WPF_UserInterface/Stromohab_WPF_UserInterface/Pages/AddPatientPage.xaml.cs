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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDateOfBirth.SelectedDate != null)
            {
                Stromohab_DataAccessLayer.DataAccessLayer.AddPatient(txtFirstNames.Text, txtLastName.Text,
                                                                     datePickerDateOfBirth.SelectedDate.Value,
                                                                     comboBxGender.Text, txtContactTelephone.Text,
                                                                     CookieManager.UserName);
            }
            NavigationService.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
