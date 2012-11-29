using System;
using System.Threading;
using System.Windows.Forms;

using SdlDotNet.Core;

namespace StromoLight_Visualiser.Forms
{
    /// <summary>
    /// Form displayed while program is loading
    /// </summary>
    public partial class Form_SplashScreen : Form
    {
        #region Member Variables

        //bool m_startDesigner = false;

        #endregion Member Variables

        #region Delegate Declarations

        #endregion Delegation Declarations

        #region Event Declarations

        #endregion Event Declarations

        /// <summary>
        /// Loading Form
        /// </summary>
        public Form_SplashScreen()
        {
            InitializeComponent();      
        }

        /// <summary>
        /// Starts countdown
        /// </summary>
        public void StartCountdown()
        {
            timer1.Enabled = true;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Hide();
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //m_startDesigner = false;
            StartDesigner();
        }

        private void lblLoading_Click(object sender, EventArgs e)
        {
            //m_startDesigner = false;
            StartDesigner();
        }

        private void StartDesigner()
        {
            timer1.Enabled = false;
            DialogResult result = MessageBox.Show("Start Designer?", "Start Designer?", MessageBoxButtons.YesNo);
            
            if (result == DialogResult.No)
            {
                this.Hide();
            }
            else
            {
                this.Hide();

            }
        }

    }
}
