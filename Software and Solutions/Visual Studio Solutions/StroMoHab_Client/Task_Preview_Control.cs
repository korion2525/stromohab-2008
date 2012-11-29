using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Client
{
    /// <summary>
    /// A control to show a task including an image, name, type and other details
    /// </summary>
    public partial class Task_Preview_Control : UserControl
    {
        #region Member Variables
        /// <summary>
        /// The task
        /// </summary>
        private Task _task;
        /// <summary>
        /// Indicates if it has been selected
        /// </summary>
        private bool _selected = false;
        /// <summary>
        /// Graphics used to show if a task is selected
        /// </summary>
        private Graphics g;
        private Pen pen = new Pen(Color.Red, 6F);
#endregion

        #region Constructor
        /// <summary>
        /// The constructor
        /// </summary>
        public Task_Preview_Control()
        {
            InitializeComponent();
            SimplePreviewMode = true;

            //If anything is clicked, say that the control has been clicked
            foreach (Control c in flowLayoutPanel.Controls)
            {
                c.Click += new EventHandler(c_Click);
            }
            flowLayoutPanel.Click += new EventHandler(c_Click);


            //double click
            foreach(Control c in flowLayoutPanel.Controls)
            {
                c.DoubleClick +=new EventHandler(c_DoubleClick);
            }
            flowLayoutPanel.DoubleClick += new EventHandler(c_DoubleClick);
        }
        #endregion

        #region Actions
        //detect double clicks and pass it on
        void c_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }
        //When anything is clicked, this method is called which then fires the UserControl's Click event
        void  c_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
        #endregion

        #region Overides
        protected override void OnPaint(PaintEventArgs e)
        {
            //When selected re-draw the rectangle 
            if (_selected)
            {
                g = flowLayoutPanel.CreateGraphics();
                g.DrawRectangle(pen, flowLayoutPanel.DisplayRectangle);
            }
            base.OnPaint(e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a bool indicating if the control is in simple or detailed preview mode
        /// In simple display a tooltip showing the description
        /// </summary>
        protected bool SimplePreviewMode { get; set; }
        /// <summary>
        /// Gets or sets a bool indicating if the current control is selected
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if (_selected)
                {
                    //if it is add a rectangle to it
                    g = flowLayoutPanel.CreateGraphics();
                    g.DrawRectangle(pen, flowLayoutPanel.DisplayRectangle);
                }
                else
                {//refresh it to clear the graphics
                    flowLayoutPanel.Refresh();
                }

            }
        }
        /// <summary>
       /// Gets or sets the Task
       /// </summary>
        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                if (_task != null)
                    FillInFields();
            }
        }
        #endregion

        #region GUI Update
        /// <summary>
        /// Fills in all the fields
        /// </summary>
        protected virtual void FillInFields()
        {
            //Task Section
            if (_task.PreviewImageAsImage != null)
                pictureBoxTask.Image = _task.PreviewImageAsImage;
            else
                pictureBoxTask.Image = pictureBoxTask.ErrorImage;
            if (_task.FileName != "")
            {
                labelTName.Text = _task.FileName;
                labelTType.Text = Task.GetEnumDescription(_task.TaskType);
                labelTNumObjects.Text = "Objects : " + _task.NumberOfObjects;
                if(_task.Distance == 0)
                    labelTDistance.Text = "Distance : Infinite";
                else
                    labelTDistance.Text = "Distance : " + _task.Distance;

                if (SimplePreviewMode) // In simple preview mode display the description as a tooltip
                {
                    //Setup the tooltip to show the tasks description
                    this.toolTipDescription.RemoveAll();
                    foreach (Control c in flowLayoutPanel.Controls)
                    {
                        this.toolTipDescription.SetToolTip(c, _task.TaskDescription);
                    }
                    this.toolTipDescription.SetToolTip(flowLayoutPanel, _task.TaskDescription);
                }
            }
            else
            {
                labelTName.Text = "         Not Selected";
                labelTType.Text = "";
                labelTNumObjects.Text = "";
                labelTDistance.Text = "";
            }

        }
#endregion
    }
}
