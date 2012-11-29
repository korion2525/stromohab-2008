namespace StroMoHab_Client
{
    partial class Run_Review_Session_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Run_Review_Session_Form));
            this.buttonRun = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBoxInfoActions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonReview = new System.Windows.Forms.Button();
            this.buttonSwitchVMode = new System.Windows.Forms.Button();
            this.stroMoHab_Panel = new CustomControls.StroMoHab_Panel();
            this.groupBoxDiagnostics = new System.Windows.Forms.GroupBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxInfoActions.SuspendLayout();
            this.flowLayoutPanelControls.SuspendLayout();
            this.groupBoxDiagnostics.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRun.Location = new System.Drawing.Point(129, 5);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(120, 60);
            this.buttonRun.TabIndex = 2;
            this.buttonRun.Text = "Start";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(34, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(500, 35);
            this.labelTitle.TabIndex = 38;
            this.labelTitle.Text = "Session Control";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxInfoActions
            // 
            this.groupBoxInfoActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfoActions.Controls.Add(this.flowLayoutPanelControls);
            this.groupBoxInfoActions.Location = new System.Drawing.Point(18, 48);
            this.groupBoxInfoActions.Name = "groupBoxInfoActions";
            this.groupBoxInfoActions.Size = new System.Drawing.Size(533, 86);
            this.groupBoxInfoActions.TabIndex = 39;
            this.groupBoxInfoActions.TabStop = false;
            // 
            // flowLayoutPanelControls
            // 
            this.flowLayoutPanelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelControls.Controls.Add(this.buttonBack);
            this.flowLayoutPanelControls.Controls.Add(this.buttonRun);
            this.flowLayoutPanelControls.Controls.Add(this.buttonReview);
            this.flowLayoutPanelControls.Controls.Add(this.buttonSwitchVMode);
            this.flowLayoutPanelControls.Location = new System.Drawing.Point(2, 10);
            this.flowLayoutPanelControls.Name = "flowLayoutPanelControls";
            this.flowLayoutPanelControls.Size = new System.Drawing.Size(525, 72);
            this.flowLayoutPanelControls.TabIndex = 36;
            // 
            // buttonReview
            // 
            this.buttonReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReview.Location = new System.Drawing.Point(256, 5);
            this.buttonReview.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonReview.Name = "buttonReview";
            this.buttonReview.Size = new System.Drawing.Size(120, 60);
            this.buttonReview.TabIndex = 3;
            this.buttonReview.Text = "Start";
            this.buttonReview.UseVisualStyleBackColor = true;
            this.buttonReview.Click += new System.EventHandler(this.buttonReview_Click);
            // 
            // buttonSwitchVMode
            // 
            this.buttonSwitchVMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSwitchVMode.Location = new System.Drawing.Point(383, 5);
            this.buttonSwitchVMode.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonSwitchVMode.Name = "buttonSwitchVMode";
            this.buttonSwitchVMode.Size = new System.Drawing.Size(120, 60);
            this.buttonSwitchVMode.TabIndex = 4;
            this.buttonSwitchVMode.Text = "Full Screen";
            this.buttonSwitchVMode.UseVisualStyleBackColor = true;
            this.buttonSwitchVMode.Click += new System.EventHandler(this.buttonSwitchVMode_Click);
            // 
            // stroMoHab_Panel
            // 
            this.stroMoHab_Panel.AutoSize = true;
            this.stroMoHab_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stroMoHab_Panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stroMoHab_Panel.Location = new System.Drawing.Point(0, 0);
            this.stroMoHab_Panel.Name = "stroMoHab_Panel";
            this.stroMoHab_Panel.Size = new System.Drawing.Size(569, 592);
            this.stroMoHab_Panel.TabIndex = 0;
            // 
            // groupBoxDiagnostics
            // 
            this.groupBoxDiagnostics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDiagnostics.Controls.Add(this.labelSpeed);
            this.groupBoxDiagnostics.Controls.Add(this.groupBox1);
            this.groupBoxDiagnostics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDiagnostics.Location = new System.Drawing.Point(18, 140);
            this.groupBoxDiagnostics.Name = "groupBoxDiagnostics";
            this.groupBoxDiagnostics.Size = new System.Drawing.Size(533, 338);
            this.groupBoxDiagnostics.TabIndex = 40;
            this.groupBoxDiagnostics.TabStop = false;
            this.groupBoxDiagnostics.Text = "Real-Time Diagnostics";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.Location = new System.Drawing.Point(6, 22);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(68, 20);
            this.labelSpeed.TabIndex = 1;
            this.labelSpeed.Text = "Speed : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 232);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graphs";
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.Location = new System.Drawing.Point(2, 5);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 60);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // Run_Review_Session_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(569, 592);
            this.Controls.Add(this.groupBoxDiagnostics);
            this.Controls.Add(this.groupBoxInfoActions);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.stroMoHab_Panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Run_Review_Session_Form";
            this.Text = "Run Session";
            this.groupBoxInfoActions.ResumeLayout(false);
            this.flowLayoutPanelControls.ResumeLayout(false);
            this.groupBoxDiagnostics.ResumeLayout(false);
            this.groupBoxDiagnostics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.StroMoHab_Panel stroMoHab_Panel;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox groupBoxInfoActions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControls;
        private System.Windows.Forms.Button buttonReview;
        private System.Windows.Forms.Button buttonSwitchVMode;
        private System.Windows.Forms.GroupBox groupBoxDiagnostics;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Button buttonBack;
    }
}