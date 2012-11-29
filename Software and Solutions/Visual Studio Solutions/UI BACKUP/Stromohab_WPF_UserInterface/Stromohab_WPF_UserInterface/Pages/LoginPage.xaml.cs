using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

using Stromohab_DataAccessLayer;
using Stromohab_WPF_UserInterface.Pages;

namespace Stromohab_WPF_UserInterface
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!DataAccessLayer.AuthenticateUser(txtUserName.Text, txtPassword.Password))
            {
                MessageBox.Show("Incorrect Username or Password. Please check and enter again.", "Login Failed");
                
                txtUserName.Clear();
                txtPassword.Clear();

                txtUserName.Focus();

            }
            else
            {
                CookieManager.UserName = txtUserName.Text;
                if (NavigationService != null)
                {
                    NavigationService.Navigate(new CliniciansPatientsPage());
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }

    }
}
