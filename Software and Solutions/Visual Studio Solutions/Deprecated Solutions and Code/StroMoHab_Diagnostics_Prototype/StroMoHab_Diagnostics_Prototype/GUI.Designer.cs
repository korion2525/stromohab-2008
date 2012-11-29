
namespace StroMoHab_Diagnostics_Prototype
{
    partial class GUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.buttonShowTracked = new System.Windows.Forms.Button();
            this.buttonCalibrate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanelDisplayJoints = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelDisplayTrackables = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBoxJoints = new System.Windows.Forms.GroupBox();
            this.checkedListBoxJoints = new System.Windows.Forms.CheckedListBox();
            this.groupBoxTrackables = new System.Windows.Forms.GroupBox();
            this.checkedListBoxTrackables = new System.Windows.Forms.CheckedListBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBoxJoints.SuspendLayout();
            this.groupBoxTrackables.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageData);
            this.tabControl.Controls.Add(this.tabPageOptions);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(779, 671);
            this.tabControl.TabIndex = 10;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.label2);
            this.tabPageData.Controls.Add(this.label1);
            this.tabPageData.Controls.Add(this.buttonReset);
            this.tabPageData.Controls.Add(this.buttonShowAll);
            this.tabPageData.Controls.Add(this.buttonShowTracked);
            this.tabPageData.Controls.Add(this.buttonCalibrate);
            this.tabPageData.Controls.Add(this.pictureBox1);
            this.tabPageData.Controls.Add(this.flowLayoutPanelDisplayJoints);
            this.tabPageData.Controls.Add(this.flowLayoutPanelDisplayTrackables);
            this.tabPageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(771, 645);
            this.tabPageData.TabIndex = 0;
            this.tabPageData.Text = "Data";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(46, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "Joints";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Body Parts";
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.Location = new System.Drawing.Point(234, 582);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(126, 52);
            this.buttonReset.TabIndex = 13;
            this.buttonReset.Text = "Reset Calibration";
            this.toolTip.SetToolTip(this.buttonReset, "Removes any calibration offsetts");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonShowAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowAll.Location = new System.Drawing.Point(572, 582);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(126, 52);
            this.buttonShowAll.TabIndex = 12;
            this.buttonShowAll.Text = "Show All";
            this.toolTip.SetToolTip(this.buttonShowAll, "Shows All Items");
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // buttonShowTracked
            // 
            this.buttonShowTracked.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonShowTracked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowTracked.Location = new System.Drawing.Point(420, 582);
            this.buttonShowTracked.Name = "buttonShowTracked";
            this.buttonShowTracked.Size = new System.Drawing.Size(126, 52);
            this.buttonShowTracked.TabIndex = 11;
            this.buttonShowTracked.Text = "Show Tracked Items Only";
            this.toolTip.SetToolTip(this.buttonShowTracked, "Hides any Body Parts (Trackables) and Joints that aren\'t currently visiable");
            this.buttonShowTracked.UseVisualStyleBackColor = true;
            this.buttonShowTracked.Click += new System.EventHandler(this.buttonShowTracked_Click);
            // 
            // buttonCalibrate
            // 
            this.buttonCalibrate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCalibrate.Location = new System.Drawing.Point(84, 582);
            this.buttonCalibrate.Name = "buttonCalibrate";
            this.buttonCalibrate.Size = new System.Drawing.Size(126, 52);
            this.buttonCalibrate.TabIndex = 10;
            this.buttonCalibrate.Text = "Calibrate";
            this.toolTip.SetToolTip(this.buttonCalibrate, "Calibrates the system by offsetting the orientation to display 0 at the current p" +
                    "ositions");
            this.buttonCalibrate.UseVisualStyleBackColor = true;
            this.buttonCalibrate.Click += new System.EventHandler(this.buttonCalibrate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(779, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanelDisplayJoints
            // 
            this.flowLayoutPanelDisplayJoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelDisplayJoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelDisplayJoints.Location = new System.Drawing.Point(37, 339);
            this.flowLayoutPanelDisplayJoints.Name = "flowLayoutPanelDisplayJoints";
            this.flowLayoutPanelDisplayJoints.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelDisplayJoints.Size = new System.Drawing.Size(683, 174);
            this.flowLayoutPanelDisplayJoints.TabIndex = 8;
            // 
            // flowLayoutPanelDisplayTrackables
            // 
            this.flowLayoutPanelDisplayTrackables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelDisplayTrackables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelDisplayTrackables.Location = new System.Drawing.Point(37, 125);
            this.flowLayoutPanelDisplayTrackables.Name = "flowLayoutPanelDisplayTrackables";
            this.flowLayoutPanelDisplayTrackables.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelDisplayTrackables.Size = new System.Drawing.Size(683, 174);
            this.flowLayoutPanelDisplayTrackables.TabIndex = 6;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.pictureBox2);
            this.tabPageOptions.Controls.Add(this.groupBoxJoints);
            this.tabPageOptions.Controls.Add(this.groupBoxTrackables);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(771, 645);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-4, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(779, 135);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // groupBoxJoints
            // 
            this.groupBoxJoints.Controls.Add(this.checkedListBoxJoints);
            this.groupBoxJoints.Location = new System.Drawing.Point(263, 181);
            this.groupBoxJoints.Name = "groupBoxJoints";
            this.groupBoxJoints.Size = new System.Drawing.Size(190, 203);
            this.groupBoxJoints.TabIndex = 1;
            this.groupBoxJoints.TabStop = false;
            this.groupBoxJoints.Text = "Joints";
            // 
            // checkedListBoxJoints
            // 
            this.checkedListBoxJoints.FormattingEnabled = true;
            this.checkedListBoxJoints.Location = new System.Drawing.Point(6, 28);
            this.checkedListBoxJoints.Name = "checkedListBoxJoints";
            this.checkedListBoxJoints.Size = new System.Drawing.Size(178, 154);
            this.checkedListBoxJoints.TabIndex = 0;
            // 
            // groupBoxTrackables
            // 
            this.groupBoxTrackables.Controls.Add(this.checkedListBoxTrackables);
            this.groupBoxTrackables.Location = new System.Drawing.Point(54, 181);
            this.groupBoxTrackables.Name = "groupBoxTrackables";
            this.groupBoxTrackables.Size = new System.Drawing.Size(190, 203);
            this.groupBoxTrackables.TabIndex = 0;
            this.groupBoxTrackables.TabStop = false;
            this.groupBoxTrackables.Text = "Body Parts";
            // 
            // checkedListBoxTrackables
            // 
            this.checkedListBoxTrackables.FormattingEnabled = true;
            this.checkedListBoxTrackables.Location = new System.Drawing.Point(6, 28);
            this.checkedListBoxTrackables.Name = "checkedListBoxTrackables";
            this.checkedListBoxTrackables.Size = new System.Drawing.Size(178, 154);
            this.checkedListBoxTrackables.TabIndex = 0;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 1000;
            this.toolTip.ReshowDelay = 500;
            this.toolTip.ShowAlways = true;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 668);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 637);
            this.Name = "GUI";
            this.Text = "StroMoHab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.Resize += new System.EventHandler(this.GUI_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            this.tabPageData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBoxJoints.ResumeLayout(false);
            this.groupBoxTrackables.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.GroupBox groupBoxTrackables;
        private System.Windows.Forms.CheckedListBox checkedListBoxTrackables;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDisplayTrackables;
        private System.Windows.Forms.GroupBox groupBoxJoints;
        private System.Windows.Forms.CheckedListBox checkedListBoxJoints;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDisplayJoints;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonCalibrate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.Button buttonShowTracked;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;



    }
}

