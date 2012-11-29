using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZedGraphDiagnosticsControl
{
    public partial class GraphDataDisplayControlForm : Form
    {
 
        public bool DisplayMediolateral_Left
        {
            get
            {
                return (checkBoxML.Checked);
            }
        }

        public bool DisplayMediolateral_Right
        {
            get
            {
                return (checkBoxMR.Checked);
            }
        }

        public bool DisplayLongitudinal_Left
        {
            get
            {
                return (checkBoxLL.Checked);
            }
        }

        public bool DisplayLongitudinal_Right
        {
            get
            {
                return (checkBoxLR.Checked);
            }
        }

        public bool DisplayAnteroposterior_Left
        {
            get
            {
                return (checkBoxAL.Checked);
            }
        }

        public bool DisplayAnteroposterior_Right
        {
            get
            {
                return (checkBoxAR.Checked);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// Initialises a new instance of a control form for the diagnostic graphs.
        /// </summary>
        public GraphDataDisplayControlForm(bool displayMediolateral_Left, bool displayMediolateral_Right, bool displayLongitudinal_Left, bool displayLongitudinal_Right, bool displayAnteroposterior_Left, bool displayAnteroposterior_Right)
        {
            InitializeComponent();

            if (displayMediolateral_Left)
            {
                checkBoxML.Checked = true;
            }
            if (displayMediolateral_Right)
            {
                checkBoxMR.Checked = true;
            }
            if (displayLongitudinal_Left)
            {
                checkBoxLL.Checked = true;
            }
            if (displayLongitudinal_Right)
            {
                checkBoxLR.Checked = true;
            }
            if (displayAnteroposterior_Left)
            {
                checkBoxAL.Checked = true;
            }
            if (displayAnteroposterior_Right)
            {
                checkBoxAR.Checked = true;
            }

        }

    }
}
