namespace StroMoHab_Client
{
    partial class Task_Preview_Control
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxTask = new System.Windows.Forms.PictureBox();
            this.labelTName = new System.Windows.Forms.Label();
            this.labelTType = new System.Windows.Forms.Label();
            this.labelTDistance = new System.Windows.Forms.Label();
            this.labelTNumObjects = new System.Windows.Forms.Label();
            this.toolTipDescription = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTask)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.pictureBoxTask);
            this.flowLayoutPanel.Controls.Add(this.labelTName);
            this.flowLayoutPanel.Controls.Add(this.labelTType);
            this.flowLayoutPanel.Controls.Add(this.labelTDistance);
            this.flowLayoutPanel.Controls.Add(this.labelTNumObjects);
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(186, 343);
            this.flowLayoutPanel.TabIndex = 6;
            this.toolTipDescription.SetToolTip(this.flowLayoutPanel, "Description");
            // 
            // pictureBoxTask
            // 
            this.pictureBoxTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTask.ErrorImage = global::StroMoHab_Client.Properties.Resources.TaskPreview;
            this.pictureBoxTask.Image = global::StroMoHab_Client.Properties.Resources.TaskPreview;
            this.pictureBoxTask.InitialImage = global::StroMoHab_Client.Properties.Resources.TaskPreview;
            this.pictureBoxTask.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxTask.Name = "pictureBoxTask";
            this.pictureBoxTask.Size = new System.Drawing.Size(180, 240);
            this.pictureBoxTask.TabIndex = 6;
            this.pictureBoxTask.TabStop = false;
            this.toolTipDescription.SetToolTip(this.pictureBoxTask, "Description");
            // 
            // labelTName
            // 
            this.labelTName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTName.Location = new System.Drawing.Point(5, 246);
            this.labelTName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.labelTName.Name = "labelTName";
            this.labelTName.Size = new System.Drawing.Size(175, 20);
            this.labelTName.TabIndex = 7;
            this.labelTName.Text = "Name : ";
            this.toolTipDescription.SetToolTip(this.labelTName, "Description");
            // 
            // labelTType
            // 
            this.labelTType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTType.Location = new System.Drawing.Point(5, 271);
            this.labelTType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.labelTType.Name = "labelTType";
            this.labelTType.Size = new System.Drawing.Size(175, 20);
            this.labelTType.TabIndex = 8;
            this.labelTType.Text = "Type : ";
            this.toolTipDescription.SetToolTip(this.labelTType, "Description");
            // 
            // labelTDistance
            // 
            this.labelTDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTDistance.Location = new System.Drawing.Point(5, 296);
            this.labelTDistance.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.labelTDistance.Name = "labelTDistance";
            this.labelTDistance.Size = new System.Drawing.Size(175, 20);
            this.labelTDistance.TabIndex = 10;
            this.labelTDistance.Text = "Distance : ";
            this.toolTipDescription.SetToolTip(this.labelTDistance, "Description");
            // 
            // labelTNumObjects
            // 
            this.labelTNumObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTNumObjects.Location = new System.Drawing.Point(5, 321);
            this.labelTNumObjects.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.labelTNumObjects.Name = "labelTNumObjects";
            this.labelTNumObjects.Size = new System.Drawing.Size(175, 20);
            this.labelTNumObjects.TabIndex = 9;
            this.labelTNumObjects.Text = "Number of Objects : ";
            this.toolTipDescription.SetToolTip(this.labelTNumObjects, "Description");
            // 
            // toolTipDescription
            // 
            this.toolTipDescription.ToolTipTitle = "Task Description";
            // 
            // Task_Preview_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Task_Preview_Control";
            this.Size = new System.Drawing.Size(188, 345);
            this.flowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBoxTask;
        private System.Windows.Forms.Label labelTName;
        private System.Windows.Forms.Label labelTType;
        private System.Windows.Forms.Label labelTDistance;
        private System.Windows.Forms.Label labelTNumObjects;
        private System.Windows.Forms.ToolTip toolTipDescription;
    }
}
