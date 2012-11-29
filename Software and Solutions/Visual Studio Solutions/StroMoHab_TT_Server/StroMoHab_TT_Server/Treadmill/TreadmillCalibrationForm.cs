using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StroMoHab_TT_Server.Treadmill
{
    public partial class TreadmillCalibrationForm : Form
    {
        private float treadmillToVisuliserConversionValue = 1;
        private double speed = 0;
        /// <summary>
        /// The form used to calibrate the speed of the treadmill
        /// </summary>
        public TreadmillCalibrationForm()
        {
            InitializeComponent();

            //gui update timer to keep things refreshed
            Timer guiUpdate = new Timer();
            guiUpdate.Interval = 10;
            guiUpdate.Tick += new EventHandler(guiUpdate_Tick);
            guiUpdate.Start();

            //validate the conversion value
            if(double.IsNaN(treadmillToVisuliserConversionValue))
                treadmillToVisuliserConversionValue = 1f;
            if (treadmillToVisuliserConversionValue < 0.1)
                treadmillToVisuliserConversionValue = 0.1f;
            if (treadmillToVisuliserConversionValue > 3)
                treadmillToVisuliserConversionValue = 3f;
            numericUpDownRatio.Value = (decimal)treadmillToVisuliserConversionValue;
        }

        void guiUpdate_Tick(object sender, EventArgs e)
        {
            textBoxSpeed.Text = Speed.ToString();
            textBoxRealSpeed.Text = Math.Round(speed * treadmillToVisuliserConversionValue, 1).ToString();
        }

        /// <summary>
        /// The ratio between speed and real speed
        /// </summary>
        public float TreadmillToVisualiserConversionValue
        {
            get { return treadmillToVisuliserConversionValue; }
            set { treadmillToVisuliserConversionValue = value; }
        }

        /// <summary>
        /// The speed of the treadmill
        /// </summary>
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        private void numericUpDownRatio_ValueChanged(object sender, EventArgs e)
        {
            treadmillToVisuliserConversionValue = (float)numericUpDownRatio.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //test the new ratio by sending out the real speed to all client applications
            TreadmillController.SetSpeed((float)Math.Round(speed * treadmillToVisuliserConversionValue, 1));
        }
    }
}
