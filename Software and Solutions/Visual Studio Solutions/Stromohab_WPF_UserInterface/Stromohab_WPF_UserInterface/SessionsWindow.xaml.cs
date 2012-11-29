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
using System.Data;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for Sessions.xaml
    /// </summary>
    public partial class SessionsWindow : Window
    {
        private string _patientId;
        private string _userName;
        public string VisualiserCommandLineArguments = null;


        public SessionsWindow(string patientId, string userName)
        {
            InitializeComponent();

            _patientId = patientId;
            _userName = userName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //datagridSessionsOfPatient.ItemsSource =
            //    Stromohab_DataAccessLayer.DataAccessLayer.SessionDatasetForCliniciansPatient(_patientId, _userName).
            //        Tables[0].DefaultView;
        }

        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            AddSessionWindow addSessionWindow = new AddSessionWindow();
            addSessionWindow.ShowDialog();

        }

        private int VideoOrVirtual()
        {
            if (datagridSessionsOfPatient.SelectedIndex != -1)
            {
                DataGridRow tempRow = (DataGridRow)datagridSessionsOfPatient.ItemContainerGenerator.ContainerFromIndex(datagridSessionsOfPatient.SelectedIndex);
                DataRowView selectedRow = tempRow.Item as DataRowView;
                return ((int)selectedRow.Row[0]);
            }
            else
            {
                return -1;
            }
        }

        private void btnRunSession_Click(object sender, RoutedEventArgs e)
        {
            int vidOrVirt = VideoOrVirtual();
            switch (vidOrVirt)
            {
                case 1:
                    {
                        VisualiserCommandLineArguments = "f v";
                        ShowDiagnosticsWindow();
                        break;
                    }
                case 2:
                    {
                        VisualiserCommandLineArguments = "f c";
                        ShowDiagnosticsWindow();
                        break;
                    }
                default:
                    {
                        VisualiserCommandLineArguments = null;
                        break;
                    }
            }            

            
        }

        private void ShowDiagnosticsWindow()
        {
            DiagnosticsWindow diagnosticsWindow = new DiagnosticsWindow();
            diagnosticsWindow = new DiagnosticsWindow();
            diagnosticsWindow.Show();
            System.Threading.Thread.Sleep(40);
            diagnosticsWindow.Close();

            System.Threading.Thread.Sleep(40);

            diagnosticsWindow = new DiagnosticsWindow();
            diagnosticsWindow.Show();
        }




    }
}
