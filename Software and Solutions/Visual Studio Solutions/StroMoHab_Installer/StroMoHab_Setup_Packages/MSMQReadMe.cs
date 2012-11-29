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
    public partial class MSMQReadMe : Form
    {
        public MSMQReadMe(string targetDir)
        {
            InitializeComponent();

            // Load in the help file
            richTextBox1.LoadFile(targetDir + "\\Setup Files\\MSMQ.rtf");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
