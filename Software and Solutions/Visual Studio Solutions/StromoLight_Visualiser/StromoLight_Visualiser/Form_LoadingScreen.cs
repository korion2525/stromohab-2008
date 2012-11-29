using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StromoLight_Visualiser
{
    /// <summary>
    /// Form displayed while program is loading
    /// </summary>
    public partial class Form_LoadingScreen : Form
    {
        int m_state;

        /// <summary>
        /// Loading Form
        /// </summary>
        public Form_LoadingScreen()
        {
            InitializeComponent();
            m_state = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (m_state)
            {
                case 0:
                    lblLoading.Text = "Loading.";
                    m_state++;
                    break;
                case 1:
                    lblLoading.Text = "Loading..";
                    m_state++;
                    break;
                case 2:
                    lblLoading.Text = "Loading...";
                    m_state = 0;
                    break;
            }
        }
    }
}
