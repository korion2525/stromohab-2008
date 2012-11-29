using System.Linq;
using System.Windows;
using Stromohab_DataAccessLayer;

namespace Stromohab_WPF_UserInterface.Pages
{
    /// <summary>
    /// Interaction logic for EditPatientPage.xaml
    /// </summary>
    public partial class EditPatientPage
    {
        private readonly patient _patientToEdit;
        public EditPatientPage(patient patientToEdit)
        {
            InitializeComponent();
            _patientToEdit = patientToEdit;

            gridPatientToEdit.DataContext = _patientToEdit;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.GoBack();
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            DataAccessLayer.UpdatePatient(_patientToEdit);

            if (NavigationService != null)
            {
                NavigationService.GoBack();
            }
        }
    }
}
