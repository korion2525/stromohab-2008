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
using Stromohab_DataAccessLayer;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string _authenticatedUsername;


        public string AuthenticatedUserName
        {
            get { return (_authenticatedUsername); }
        }

        public LoginWindow()
        {
            _authenticatedUsername = null;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           //hack to avoid continually logging in
            //_authenticatedUsername = "admin";


            if (!DataAccessLayer.AuthenticateUser(txtUserName.Text, txtPassword.Password))
            {
                MessageBox.Show("Incorrect Username or Password. Please check and enter again.", "Login Failed");
                DialogResult = false;
            }
            else
            {
                _authenticatedUsername = txtUserName.Text;
                DialogResult = true;
            }
        }


    }
}
