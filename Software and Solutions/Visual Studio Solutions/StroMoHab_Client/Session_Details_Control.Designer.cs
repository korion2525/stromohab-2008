namespace StroMoHab_Client
{
    partial class Session_Details_Control
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
            this.groupBoxNotes = new System.Windows.Forms.GroupBox();
            this.richTextBoxNotes = new System.Windows.Forms.RichTextBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelClinician = new System.Windows.Forms.Label();
            this.labelMCData = new System.Windows.Forms.Label();
            this.labelAsymetry = new System.Windows.Forms.Label();
            this.labelPercentObjectsAvoided = new System.Windows.Forms.Label();
            this.labelStrideLength = new System.Windows.Forms.Label();
            this.task_Preview_Control = new StroMoHab_Client.Task_Preview_Control();
            this.groupBoxNotes.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNotes
            // 
            this.groupBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNotes.Controls.Add(this.richTextBoxNotes);
            this.groupBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNotes.Location = new System.Drawing.Point(10, 384);
            this.groupBoxNotes.Name = "groupBoxNotes";
            this.groupBoxNotes.Size = new System.Drawing.Size(584, 148);
            this.groupBoxNotes.TabIndex = 12;
            this.groupBoxNotes.TabStop = false;
            this.groupBoxNotes.Text = "Session Notes";
            // 
            // richTextBoxNotes
            // 
            this.richTextBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxNotes.Location = new System.Drawing.Point(6, 21);
            this.richTextBoxNotes.Name = "richTextBoxNotes";
            this.richTextBoxNotes.Size = new System.Drawing.Size(573, 121);
            this.richTextBoxNotes.TabIndex = 1;
            this.richTextBoxNotes.Text = "";
            this.richTextBoxNotes.Validating += new System.ComponentModel.CancelEventHandler(this.richTextBoxNotes_Validating);
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDetails.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDetails.Location = new System.Drawing.Point(10, 0);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(390, 384);
            this.groupBoxDetails.TabIndex = 11;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Session Details";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelDate);
            this.flowLayoutPanel1.Controls.Add(this.labelTime);
            this.flowLayoutPanel1.Controls.Add(this.labelDuration);
            this.flowLayoutPanel1.Controls.Add(this.labelClinician);
            this.flowLayoutPanel1.Controls.Add(this.labelMCData);
            this.flowLayoutPanel1.Controls.Add(this.labelAsymetry);
            this.flowLayoutPanel1.Controls.Add(this.labelPercentObjectsAvoided);
            this.flowLayoutPanel1.Controls.Add(this.labelStrideLength);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(384, 359);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelDate
            // 
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(5, 0);
            this.labelDate.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(250, 20);
            this.labelDate.TabIndex = 8;
            this.labelDate.Text = "Date :";
            // 
            // labelTime
            // 
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(5, 25);
            this.labelTime.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(250, 20);
            this.labelTime.TabIndex = 9;
            this.labelTime.Text = "Time : ";
            // 
            // labelDuration
            // 
            this.labelDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDuration.Location = new System.Drawing.Point(5, 50);
            this.labelDuration.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(250, 20);
            this.labelDuration.TabIndex = 7;
            this.labelDuration.Text = "Duration : ";
            // 
            // labelClinician
            // 
            this.labelClinician.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClinician.Location = new System.Drawing.Point(5, 75);
            this.labelClinician.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelClinician.Name = "labelClinician";
            this.labelClinician.Size = new System.Drawing.Size(250, 20);
            this.labelClinician.TabIndex = 11;
            this.labelClinician.Text = "Clinician : ";
            // 
            // labelMCData
            // 
            this.labelMCData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMCData.Location = new System.Drawing.Point(5, 100);
            this.labelMCData.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelMCData.Name = "labelMCData";
            this.labelMCData.Size = new System.Drawing.Size(250, 20);
            this.labelMCData.TabIndex = 10;
            this.labelMCData.Text = "MC Data Available : No";
            // 
            // labelAsymetry
            // 
            this.labelAsymetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsymetry.Location = new System.Drawing.Point(5, 125);
            this.labelAsymetry.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelAsymetry.Name = "labelAsymetry";
            this.labelAsymetry.Size = new System.Drawing.Size(250, 20);
            this.labelAsymetry.TabIndex = 6;
            this.labelAsymetry.Text = "Left/Right Asymetry :  N/A";
            // 
            // labelPercentObjectsAvoided
            // 
            this.labelPercentObjectsAvoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPercentObjectsAvoided.Location = new System.Drawing.Point(5, 150);
            this.labelPercentObjectsAvoided.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelPercentObjectsAvoided.Name = "labelPercentObjectsAvoided";
            this.labelPercentObjectsAvoided.Size = new System.Drawing.Size(250, 20);
            this.labelPercentObjectsAvoided.TabIndex = 5;
            this.labelPercentObjectsAvoided.Text = "% of Objects Avoided :  N/A";
            // 
            // labelStrideLength
            // 
            this.labelStrideLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStrideLength.Location = new System.Drawing.Point(5, 175);
            this.labelStrideLength.Margin = new System.Windows.Forms.Padding(5, 0, 15, 5);
            this.labelStrideLength.Name = "labelStrideLength";
            this.labelStrideLength.Size = new System.Drawing.Size(250, 20);
            this.labelStrideLength.TabIndex = 4;
            this.labelStrideLength.Text = "Stride Length :  N/A";
            // 
            // task_Preview_Control
            // 
            this.task_Preview_Control.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.task_Preview_Control.BackColor = System.Drawing.Color.White;
            this.task_Preview_Control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.task_Preview_Control.Location = new System.Drawing.Point(405, 9);
            this.task_Preview_Control.Margin = new System.Windows.Forms.Padding(7);
            this.task_Preview_Control.Name = "task_Preview_Control";
            this.task_Preview_Control.Selected = false;
            this.task_Preview_Control.Size = new System.Drawing.Size(188, 375);
            this.task_Preview_Control.TabIndex = 13;
            this.task_Preview_Control.Task = null;
            // 
            // Session_Details_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.task_Preview_Control);
            this.Controls.Add(this.groupBoxNotes);
            this.Controls.Add(this.groupBoxDetails);
            this.Name = "Session_Details_Control";
            this.Size = new System.Drawing.Size(602, 535);
            this.groupBoxNotes.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNotes;
        private System.Windows.Forms.RichTextBox richTextBoxNotes;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelAsymetry;
        private System.Windows.Forms.Label labelPercentObjectsAvoided;
        private System.Windows.Forms.Label labelStrideLength;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelMCData;
        private Task_Preview_Control task_Preview_Control;
        private System.Windows.Forms.Label labelClinician;
    }
}
