using System;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using StroMoHab_Objects.Objects;
using StroMoHab_Objects.Graphics;
using Win32Utilities;

namespace StromoLight_Visualiser.Forms
{
    /// <summary>
    /// Main Task Designer Form
    /// </summary>
    public partial class Form_TaskDesigner_0 : Form
    {
        #region Member Variables

        IntPtr m_parentWindowHandle;

        #endregion Member Variables

        #region Delegates

        /// <summary>
        /// Delegate for event "CurrentDistanceChanged"
        /// </summary>
        /// <param name="newDistance"></param>
        public delegate void CurrentDistanceChangedHandler(float newDistance);

        #endregion Delegates

        #region Events
        /// <summary>
        /// Triggered when the distance is changed in the task designer
        /// </summary>
        public event CurrentDistanceChangedHandler CurrentDistanceChanged;

        #endregion Events

        /// <summary>
        /// Constructor
        /// </summary>
        public Form_TaskDesigner_0(IntPtr parentWindowHandle)
        {
            InitializeComponent();
            m_parentWindowHandle = parentWindowHandle;

        }

        private void customNumericUpDownCurrentPosition_ValueChanged(object sender, EventArgs e)
        {   
            if (CurrentDistanceChanged != null)
            {
                CurrentDistanceChanged((float)this.customNumericUpDownCurrentPosition.Value);
            }
        }

        private void Form_TaskDesigner_0_Shown(object sender, EventArgs e)
        {
            SetWindowPosition();
            this.customNumericUpDownCurrentPosition.Focus();
        }

        private void SetWindowPosition()
        {
            Win32.RECT rct;

            Win32.GetWindowRect(m_parentWindowHandle, out rct);
   
            int x = rct.Right;
            int y = rct.Top;

            Win32.SetWindowPos(this.Handle, Win32.HWND_NOTOPMOST, x, y, this.Width, this.Height, Win32.SWP_SHOWWINDOW);
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void Form_TaskDesigner_0_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Escape || (Keys)e.KeyValue == Keys.F12)
            {
                this.Hide();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void comboBxObjectToAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBxObjectToAdd.Text == "Cube")
            {
                groupBxCube.Show();
                timer1.Enabled = true;
            }
        }

        private void groupBxCube_VisibleChanged(object sender, EventArgs e)
        {
            txtBxLeftRightPosition.Text = ((float)(trackBarLeftRightPosition.Value - 150) / 100).ToString();

            pictureBxTextureImage.Image = imageList1.Images[0];
        }

        private void trackBarLeftRightPosition_ValueChanged(object sender, EventArgs e)
        {
            txtBxLeftRightPosition.Text = ((float)(trackBarLeftRightPosition.Value - 150) / 100).ToString();
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            float xMin = ((float)Convert.ToDouble(txtBxLeftRightPosition.Text))-((float)numericUpDownCubeSize.Value/2);
            float xMax = xMin + ((float)numericUpDownCubeSize.Value);

            float yMin = (float)numericUpDownDistanceFromGround.Value;
            float yMax = yMin + (float)numericUpDownCubeSize.Value;

            float zMin = -(float)this.customNumericUpDownCurrentPosition.Value;
            float zMax = zMin + (float)numericUpDownCubeSize.Value;

            Cube objectToAdd = new Cube(4, 1, 1, xMin, yMin, zMin, xMax, yMax, zMax);

            SDL_Main.AddObjectToDrawList(objectToAdd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (groupBxCube.Height < 169)
            {
                groupBxCube.Height += 3;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void numericUpDownImageNumber_ValueChanged(object sender, EventArgs e)
        {
            pictureBxTextureImage.Image = imageList1.Images[(int)(numericUpDownImageNumber.Value-1)];
        }

    }
}
