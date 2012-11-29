using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Stromohab_DataAccessLayer;

namespace Stromohab_WPF_UserInterface.Pages
{
    /// <summary>
    /// Interaction logic for CliniciansPatientsPage.xaml
    /// </summary>
    public partial class CliniciansPatientsPage
    {
        public CliniciansPatientsPage()
        {
            InitializeComponent();
        }

// ReSharper disable InconsistentNaming
        private void Page_Loaded(object sender, RoutedEventArgs e)
// ReSharper restore InconsistentNaming
        {
            string userName = Application.GetCookie(new Uri(Environment.CurrentDirectory)).Split('=')[1];

            LoadPatients(userName);
        }

        private void LoadPatients(string userName)
        {
            datagridPatientsOfClinician.ItemsSource = DataAccessLayer.GetCliniciansPatients(userName);
        }

// ReSharper disable InconsistentNaming
        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
// ReSharper restore InconsistentNaming
        {
            Application.SetCookie(new Uri(Environment.CurrentDirectory), "userName=");
            NavigationService.GoBack();
        }

        private void btnGoToSessions_Click(object sender, RoutedEventArgs e)
        {
            int patientId = SelectPatientIdFromSelectedDatagridRow();

            if (patientId != -99999)
            {
                NavigationService.Navigate(new PatientSessionsPage(patientId));
            }
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatientPage());
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            int patientToEditId = SelectPatientIdFromSelectedDatagridRow();
            if (patientToEditId != -99999)
            {
                NavigationService.Navigate(new EditPatientPage(DataAccessLayer.SelectPatientFromPatientId(patientToEditId)));
            }
        }

        private int SelectPatientIdFromSelectedDatagridRow()
        {
            int patientId = -99999;

            if (datagridPatientsOfClinician.SelectedIndex != -1)
            {

                DataGridRow selectedRow =
                    (DataGridRow)
                    datagridPatientsOfClinician.ItemContainerGenerator.ContainerFromIndex(
                        datagridPatientsOfClinician.SelectedIndex);

                if (selectedRow != null && datagridPatientsOfClinician.Columns[0] != null)
                {
                    try
                    {
                        patientId =
                            Convert.ToInt32(
                                (datagridPatientsOfClinician.Columns[0].GetCellContent(selectedRow) as TextBlock).Text);
                    }
                    catch (NullReferenceException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            "Exception thrown in CliniciansPatientsPage.xmal.cs. Message: " +
                            ex.Message);
                    }

                }
            }
            return patientId;
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            int patientId = SelectPatientIdFromSelectedDatagridRow();
            
            if (patientId != -99999)
            {
                string patientFullName = DataAccessLayer.PatientFirstNamesFromPatientId(patientId) + " " +
                                         DataAccessLayer.PatientLastNameFromPatientId(patientId);
                
                DataAccessLayer.SelectPatientFromPatientId(patientId);

                if (MessageBox.Show(
                    "Are you sure you wish to remove " + patientFullName + " from the Stromohab system? This operation cannot be undone.",
                    "Remove " + patientFullName + "?", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataAccessLayer.DeletePatientById(patientId);
                    LoadPatients(CookieManager.UserName);
                }
                
            }
        }
    }
}

