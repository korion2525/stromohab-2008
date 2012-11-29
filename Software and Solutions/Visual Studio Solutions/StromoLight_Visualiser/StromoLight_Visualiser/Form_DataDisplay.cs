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
    /// Displays Visualiser Data
    /// </summary>
    public partial class Form_DataDisplay : Form
    {
        #region Member Variables

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// X label value
        /// </summary>
        public float XLabelValue
        {
            get
            {
                return (float)Convert.ToDouble(lblAvatarX.Text);
            }
            set
            {
                lblAvatarX.Text = value.ToString();
            }
        }

        #endregion Properties



        /// <summary>
        /// Constructor
        /// </summary>
        public Form_DataDisplay()
        {
            InitializeComponent();
        }
    }
}
