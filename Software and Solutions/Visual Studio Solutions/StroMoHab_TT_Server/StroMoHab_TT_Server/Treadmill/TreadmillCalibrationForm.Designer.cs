namespace StroMoHab_TT_Server.Treadmill
{
    /// <summary>
    /// The form definition
    /// </summary>
    partial class TreadmillCalibrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreadmillCalibrationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelRealSpeed = new System.Windows.Forms.Label();
            this.labelRatio = new System.Windows.Forms.Label();
            this.numericUpDownRatio = new System.Windows.Forms.NumericUpDown();
            this.textBoxRealSpeed = new System.Windows.Forms.TextBox();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(68, 79);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(85, 13);
            this.labelSpeed.TabIndex = 1;
            this.labelSpeed.Text = "Detected Speed";
            // 
            // labelRealSpeed
            // 
            this.labelRealSpeed.AutoSize = true;
            this.labelRealSpeed.Location = new System.Drawing.Point(68, 112);
            this.labelRealSpeed.Name = "labelRealSpeed";
            this.labelRealSpeed.Size = new System.Drawing.Size(63, 13);
            this.labelRealSpeed.TabIndex = 2;
            this.labelRealSpeed.Text = "Real Speed";
            // 
            // labelRatio
            // 
            this.labelRatio.AutoSize = true;
            this.labelRatio.Location = new System.Drawing.Point(68, 142);
            this.labelRatio.Name = "labelRatio";
            this.labelRatio.Size = new System.Drawing.Size(66, 13);
            this.labelRatio.TabIndex = 3;
            this.labelRatio.Text = "Speed Ratio";
            // 
            // numericUpDownRatio
            // 
            this.numericUpDownRatio.DecimalPlaces = 2;
            this.numericUpDownRatio.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownRatio.Location = new System.Drawing.Point(172, 140);
            this.numericUpDownRatio.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            131072});
            this.numericUpDownRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownRatio.Name = "numericUpDownRatio";
            this.numericUpDownRatio.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownRatio.TabIndex = 4;
            this.numericUpDownRatio.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numericUpDownRatio.ValueChanged += new System.EventHandler(this.numericUpDownRatio_ValueChanged);
            // 
            // textBoxRealSpeed
            // 
            this.textBoxRealSpeed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxRealSpeed.Location = new System.Drawing.Point(172, 109);
            this.textBoxRealSpeed.Name = "textBoxRealSpeed";
            this.textBoxRealSpeed.ReadOnly = true;
            this.textBoxRealSpeed.Size = new System.Drawing.Size(59, 20);
            this.textBoxRealSpeed.TabIndex = 5;
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxSpeed.Location = new System.Drawing.Point(172, 76);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.ReadOnly = true;
            this.textBoxSpeed.Size = new System.Drawing.Size(59, 20);
            this.textBoxSpeed.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(70, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TreadmillCalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 202);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSpeed);
            this.Controls.Add(this.textBoxRealSpeed);
            this.Controls.Add(this.numericUpDownRatio);
            this.Controls.Add(this.labelRatio);
            this.Controls.Add(this.labelRealSpeed);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TreadmillCalibrationForm";
            this.Text = "Treadmill Speed Calibration";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelRealSpeed;
        private System.Windows.Forms.Label labelRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownRatio;
        private System.Windows.Forms.TextBox textBoxRealSpeed;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}