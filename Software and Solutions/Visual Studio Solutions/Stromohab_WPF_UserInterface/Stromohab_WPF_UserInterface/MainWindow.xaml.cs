using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Stromohab_DataAccessLayer;
using StromoLight_Visualiser;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccessLayer _dataAccessLayer = null;
        private PatientWindow _patientWindow = null;
        private SessionsWindow _sessionsWindow = null;
        

        public MainWindow()
        {
            InitializeComponent();

            _dataAccessLayer = DataAccessLayer.CreateInstance();
        }

        private void DisplayLoginScreen()
        {
            LoginWindow loginWindow = new LoginWindow {Owner = this};
            loginWindow.btnCancel.Click += new RoutedEventHandler(btnLoginCancel_Click);
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult.HasValue && loginWindow.DialogResult.Value == false)
            {
                DisplayLoginScreen();
            }
            else
            {
                _patientWindow = new PatientWindow(loginWindow.AuthenticatedUserName);
                _patientWindow.btnBackToLogin.Click += new RoutedEventHandler(btnBackToLogin_Click);
                _patientWindow.btnGoToSessions.Click += new RoutedEventHandler(btnGoToSessions_Click);
                loginWindow.Close();
                _patientWindow.Show();
            }
        }

        void btnGoToSessions_Click(object sender, RoutedEventArgs e)
        {
            if (_patientWindow.PatientId != null && _patientWindow.UserName != null)
            { 
                _sessionsWindow = new SessionsWindow(_patientWindow.PatientId, _patientWindow.UserName);
                _sessionsWindow.btnBackToPatientScreen.Click += new RoutedEventHandler(btnBackToPatientScreen_Click);
                _sessionsWindow.btnRunSession.Click += new RoutedEventHandler(btnRunSession_Click);
                _patientWindow.Hide();
                _sessionsWindow.Show();
            }

        }

        private Process _visualiserProcess;
        void btnRunSession_Click(object sender, RoutedEventArgs e)
        {
            if (_sessionsWindow.VisualiserCommandLineArguments != null)
            {
                _visualiserProcess = Process.Start(@"C:\Users\mag501\Desktop\WC_stromohab_08_VideoVisualiser\Visual Studio Solutions\StromoLight_Visualiser\StromoLight_Visualiser\bin\Debug\StromoLight_Visualiser.exe", _sessionsWindow.VisualiserCommandLineArguments);
            }
        }

        void btnBackToPatientScreen_Click(object sender, RoutedEventArgs e)
        {
            _patientWindow.Show();
            _sessionsWindow.Hide();
        }

        void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            _patientWindow.Close();
            DisplayLoginScreen();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DisplayLoginScreen();
            //Hide();
            _mainFrame.Navigate(new LoginPage());
            Left = SystemParameters.PrimaryScreenWidth/2 - (double) GetValue(WidthProperty)*2;
            Top = SystemParameters.PrimaryScreenHeight/2 - (double) GetValue(HeightProperty)*3;
        }

        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_visualiserProcess != null)
            {
                if (!_visualiserProcess.HasExited)
                {
                    _visualiserProcess.Kill();
                }
            }
            Close();
            Environment.Exit(0);
        }
    }

}
