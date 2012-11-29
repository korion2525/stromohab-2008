using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StroMoHab_Objects.Communication.Forms
{
    /// <summary>
    /// A form designed to resolve the problem of not being able to find the IP address of the server and connect to it
    /// </summary>
    public partial class FindServer : Form
    {
        /// <summary>
        /// Displays a form to resolve the problem of not being able
        /// to find the IP address of the server
        /// Gives the user the change to ask for a retry, to manually enter
        /// and IP address or to cancel and exit.
        /// </summary>
        public FindServer(string defaultIP)
        {
            
            InitializeComponent();

            //Hide unused items
            textBoxIP.Hide();
            buttonIPCancel.Hide();
            buttonIPOK.Hide();
            labelError.Hide();

            textBoxIP.Text = defaultIP;
            //set the returned result incase it is exited
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            //User asked to retry
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void buttonEnterIP_Click(object sender, EventArgs e)
        {
            // Prepare for an IP to be entered
            textBoxIP.Show();
            buttonIPCancel.Show();
            buttonIPOK.Show();
            buttonEnterIP.Hide();
            buttonExit.Hide();
            buttonRetry.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // User wanted to cancel
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonIPCancel_Click(object sender, EventArgs e)
        {
            // User didn't want to enter an ip any more
            textBoxIP.Hide();
            buttonIPCancel.Hide();
            buttonIPOK.Hide();
            buttonEnterIP.Show();
            buttonExit.Show();
            buttonRetry.Show();
            labelError.Hide();
        }

        private void buttonIPOK_Click(object sender, EventArgs e)
        {
            //user entered an IP - validate it

            labelError.Hide();
            if (IsValidIP(textBoxIP.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                labelError.Show();

            }

        }

        /// <summary>
        /// Validates the given IP address
        /// Taken from http://www.dreamincode.net/code/snippet1378.htm
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        /// <summary>
        /// The manually entered IP address
        /// </summary>
        public string ServerIP
        {
            get
            {
                return textBoxIP.Text;
            }
        }

   
    }
}
