using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StroMoHab_Client
{
    /// <summary>
    /// This control extends Task_Preview_Control and adds extra information. It is shown when the user selects a task
    /// </summary>
    public partial class Task_Detailed_Preview_Control : Task_Preview_Control
    {
        public Task_Detailed_Preview_Control()
        {
            InitializeComponent();
            SimplePreviewMode = false; // Move to detailed preview mode
        }


        protected override void FillInFields()
        {
            //override the fill in fields method to add in extra labels
            if (Task.FileName != "")
            {
                labelDescription.Text = "Description : " + Task.TaskDescription;

            }

            //Then call the original method to fill in the original fields as well
            base.FillInFields();
        }
    }
}
