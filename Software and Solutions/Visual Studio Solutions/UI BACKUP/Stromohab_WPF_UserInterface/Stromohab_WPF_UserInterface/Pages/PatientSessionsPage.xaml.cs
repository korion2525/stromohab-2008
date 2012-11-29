using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for PatientSessionsPage.xaml
    /// </summary>
    public partial class PatientSessionsPage : Page
    {
        private int _patientId;

        public PatientSessionsPage(int patientId)
        {
            InitializeComponent();

            _patientId = patientId;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datagridSessionsOfPatient.ItemsSource = Stromohab_DataAccessLayer.DataAccessLayer.PatientSessions(
                _patientId, CookieManager.UserName);
        }

        private void btnBackToPatientScreen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        private void btnRunSession_Click(object sender, RoutedEventArgs e)
        {
            DiagnosticsWindow diagWindow = new DiagnosticsWindow();

            diagWindow.Show();
        }
    }
}
