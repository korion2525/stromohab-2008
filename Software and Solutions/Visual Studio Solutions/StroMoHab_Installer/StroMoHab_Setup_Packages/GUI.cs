using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StroMoHab_Setup_Packages
{
    public partial class GUI : Form
    {
        /// <summary>
        /// Represents three different install states of an application or windows feature
        /// </summary>
        public enum InstallState { Installed, InstalledButOld, NotInstalled };


        string targetDir = null;
        InstallState ttInstallState = InstallState.NotInstalled;
        InstallState msmqInstallState = InstallState.NotInstalled;


        public GUI(string input)
        {
            if (input != null) // been giving the dir by the installer
            {
                // Get and cleanup the target directory
                targetDir = input;
                targetDir = targetDir.Replace("\"", "");

                installMessage = "Please run the setups for the below packages if you need to install, update, or repair them before continuing to install StroMoHab.";

                // Try and silently check
                CheckInstalledTTVersion();
                CheckMSMQ();

                // If everything is already installed then silently exit, else present the user with a gui to sort out the problems
                if (ttInstallState == InstallState.Installed && msmqInstallState == InstallState.Installed)
                    Environment.Exit(0);

            }
            else // Haven't been given a target dir so running from start menu
            {
                targetDir = Environment.CurrentDirectory;
                installMessage = "Please run the setups for the below packages if you need to install, update, or repair them.";
            }


            // Setup and start a timer to keep checking the install state of applications
            System.Windows.Forms.Timer statusUpdate = new Timer();
            statusUpdate.Interval = 500;
            statusUpdate.Tick += new EventHandler(statusUpdate_Tick);
            statusUpdate.Start();


            InitializeComponent();


            labelMessage.Text = installMessage;
            
        }

        void statusUpdate_Tick(object sender, EventArgs e)
        {
            //Cehck and then update
            CheckInstalledTTVersion();
            CheckMSMQ();
            UpdateGUI();
        }

       
        private void CheckInstalledTTVersion()
        {
            string softwareNode = null;

            // Check for 32 or 64 bit OS
            Microsoft.Win32.RegistryKey osSubKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\NaturalPoint\\TrackingTools");
            
            if (osSubKey != null) // 64-bit OS
            {
                softwareNode = "Software\\Wow6432Node\\";
            }
            else // 32-bit OS
            {
                softwareNode = "Software\\";
            }

            //Build the tracking tools node string
            string ttNode = softwareNode + "NaturalPoint\\TrackingTools";

            //See if its there yet
            Microsoft.Win32.RegistryKey subKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ttNode);
            if (subKey != null)
            {
                // If its there - see if the versions match
                Microsoft.Win32.RegistryKey ttVersionKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ttNode + "\\" + StroMoHab_Setup_Packages.Properties.Settings.Default.TrackingToolsVersion + ".0000");
                if (ttVersionKey != null)
                {
                    // Versions match
                    ttInstallState = InstallState.Installed;
                }
                else
                {
                    // old version
                    ttInstallState = InstallState.InstalledButOld;
                }

            }
            else // not installed
            {
                ttInstallState = InstallState.NotInstalled;

            }

        }
        /// <summary>
        /// Checks a registry value to see if MSMQ is installed
        /// </summary>
        private void CheckMSMQ()
        {
            // Checks to see if MSMQ is installed
            Microsoft.Win32.RegistryKey subKey =Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\MSMQ\\Setup");
            if (subKey != null)
            {   //BUG FIX ! subKey.GetValue is null - statement commented out for now
                //if ((int)subKey.GetValue("msmq_CoreInstalled") == 1)
                //{
                    msmqInstallState = InstallState.Installed;
                /*}
                else
                {
                    msmqInstallState = InstallState.NotInstalled;
                }*/
            }
        }

        private void UpdateGUI()
        {
            if (msmqInstallState == InstallState.Installed)
            {
                MSMQButton.Text = "MSMQ is already Installed";
                MSMQButton.Enabled = false;
            }
            else
            {
                MSMQButton.Text = "Install";
                MSMQButton.Enabled = true;
            }


            if (ttInstallState == InstallState.Installed)
            {
                ttButton.Text = "Tracking Tools is already installed - Press to Repair";
            }
            if (ttInstallState == InstallState.InstalledButOld)
            {
                ttButton.Text = "Tracking Tools is already installed but out of date - Press to Update";
            }
            if (ttInstallState == InstallState.NotInstalled)
            {
                ttButton.Text = "Tracking Tools is not installed - Press to Install";
            }
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Calls trackingtools isntaller
            System.Diagnostics.Process ttInstaller = new System.Diagnostics.Process();
            ttInstaller.StartInfo.FileName = targetDir + "\\Setup Files\\" + StroMoHab_Setup_Packages.Properties.Settings.Default.TrackingToolsInstallName;
            ttInstaller.Start();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MSMQButton_Click(object sender, EventArgs e)
        {
            // Shows the user how to install MSMQ
            MSMQReadMe msmqreadme = new MSMQReadMe(targetDir);
            msmqreadme.Show();
        }

    }
}
