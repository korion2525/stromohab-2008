using System.Windows;
using System.Windows.Controls;
using Stromohab_DataAccessLayer;


namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private string _userName = null;
        private string _patientId = null;

        public PatientWindow(string userName)
        {
            InitializeComponent();

            _userName = userName;
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string PatientId
        {
            get { return _patientId; }
        }

        private void LoadPatients(string userName)
        {
            datagridPatientsOfClinician.ItemsSource = DataAccessLayer.GetCliniciansPatients(userName);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPatients(_userName);
        }

        private void datagridPatientsOfClinician_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            DataGridRow selectedRow =
                (DataGridRow)
                datagridPatientsOfClinician.ItemContainerGenerator.ContainerFromIndex(
                    datagridPatientsOfClinician.SelectedIndex);

            if (selectedRow != null)
            {
                _patientId = (datagridPatientsOfClinician.Columns[0].GetCellContent(selectedRow) as TextBlock).Text;
            }
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWindow addPatientWindow = new AddPatientWindow(_userName);
            addPatientWindow.ShowDialog();

            LoadPatients(_userName);
        }
    }
}
