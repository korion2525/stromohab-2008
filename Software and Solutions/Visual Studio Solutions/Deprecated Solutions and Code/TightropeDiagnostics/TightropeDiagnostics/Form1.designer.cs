namespace StromoLight_Diagnostics
{
    partial class frmUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUI));
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnStartTreadmill = new System.Windows.Forms.Button();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.nudElevation = new System.Windows.Forms.NumericUpDown();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblElevation = new System.Windows.Forms.Label();
            this.btnEndSession = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDisengage = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCalibrateGroundPlane = new System.Windows.Forms.Button();
            this.chkDelayStart = new System.Windows.Forms.CheckBox();
            this.btnStopData = new System.Windows.Forms.Button();
            this.chkLink = new System.Windows.Forms.CheckBox();
            this.btnStartData = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbLeftToe = new System.Windows.Forms.CheckBox();
            this.cbLeftHeel = new System.Windows.Forms.CheckBox();
            this.cbRightToe = new System.Windows.Forms.CheckBox();
            this.cbRightHeel = new System.Windows.Forms.CheckBox();
            this.cbSonify = new System.Windows.Forms.CheckBox();
            this.titleSwingStance = new System.Windows.Forms.Label();
            this.dataSwingStance = new System.Windows.Forms.Label();
            this.titleCycleDuration = new System.Windows.Forms.Label();
            this.dataCycleDuration = new System.Windows.Forms.Label();
            this.titleStepInterval = new System.Windows.Forms.Label();
            this.dataStepInterval = new System.Windows.Forms.Label();
            this.titleDoubleSupport = new System.Windows.Forms.Label();
            this.dataDoubleSupport = new System.Windows.Forms.Label();
            this.titleStrideLength = new System.Windows.Forms.Label();
            this.dataStrideLength = new System.Windows.Forms.Label();
            this.titleStrideDuration = new System.Windows.Forms.Label();
            this.dataStrideDuration = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudElevation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnConnect.Location = new System.Drawing.Point(29, 141);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 55);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Start session";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnStartTreadmill
            // 
            this.btnStartTreadmill.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnStartTreadmill.Location = new System.Drawing.Point(9, 128);
            this.btnStartTreadmill.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartTreadmill.Name = "btnStartTreadmill";
            this.btnStartTreadmill.Size = new System.Drawing.Size(218, 126);
            this.btnStartTreadmill.TabIndex = 2;
            this.btnStartTreadmill.Text = "Start Treadmill";
            this.btnStartTreadmill.UseVisualStyleBackColor = true;
            this.btnStartTreadmill.Click += new System.EventHandler(this.btnStartTreadmill_Click);
            // 
            // nudSpeed
            // 
            this.nudSpeed.DecimalPlaces = 1;
            this.nudSpeed.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.nudSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSpeed.Location = new System.Drawing.Point(149, 39);
            this.nudSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.nudSpeed.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(75, 23);
            this.nudSpeed.TabIndex = 3;
            this.nudSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSpeed.ValueChanged += new System.EventHandler(this.nudSpeed_ValueChanged);
            // 
            // nudElevation
            // 
            this.nudElevation.DecimalPlaces = 1;
            this.nudElevation.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.nudElevation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudElevation.Location = new System.Drawing.Point(148, 76);
            this.nudElevation.Margin = new System.Windows.Forms.Padding(4);
            this.nudElevation.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudElevation.Name = "nudElevation";
            this.nudElevation.Size = new System.Drawing.Size(75, 23);
            this.nudElevation.TabIndex = 4;
            this.nudElevation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudElevation.ValueChanged += new System.EventHandler(this.nudElevation_ValueChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.lblSpeed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSpeed.Location = new System.Drawing.Point(6, 38);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(57, 17);
            this.lblSpeed.TabIndex = 5;
            this.lblSpeed.Text = "Speed:";
            // 
            // lblElevation
            // 
            this.lblElevation.AutoSize = true;
            this.lblElevation.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.lblElevation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblElevation.Location = new System.Drawing.Point(6, 80);
            this.lblElevation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElevation.Name = "lblElevation";
            this.lblElevation.Size = new System.Drawing.Size(80, 17);
            this.lblElevation.TabIndex = 6;
            this.lblElevation.Text = "Elevation:";
            // 
            // btnEndSession
            // 
            this.btnEndSession.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnEndSession.Location = new System.Drawing.Point(29, 206);
            this.btnEndSession.Margin = new System.Windows.Forms.Padding(4);
            this.btnEndSession.Name = "btnEndSession";
            this.btnEndSession.Size = new System.Drawing.Size(112, 55);
            this.btnEndSession.TabIndex = 8;
            this.btnEndSession.Text = "Clear data";
            this.btnEndSession.UseVisualStyleBackColor = true;
            this.btnEndSession.Click += new System.EventHandler(this.btnEndSession_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDisengage);
            this.groupBox1.Controls.Add(this.lblElevation);
            this.groupBox1.Controls.Add(this.btnStartTreadmill);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.nudElevation);
            this.groupBox1.Controls.Add(this.nudSpeed);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(189, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(486, 264);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Treadmill control";
            // 
            // btnDisengage
            // 
            this.btnDisengage.Enabled = false;
            this.btnDisengage.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnDisengage.Location = new System.Drawing.Point(252, 128);
            this.btnDisengage.Margin = new System.Windows.Forms.Padding(4);
            this.btnDisengage.Name = "btnDisengage";
            this.btnDisengage.Size = new System.Drawing.Size(218, 58);
            this.btnDisengage.TabIndex = 8;
            this.btnDisengage.Text = "Disengage belt";
            this.btnDisengage.UseVisualStyleBackColor = true;
            this.btnDisengage.Click += new System.EventHandler(this.btnDisengage_Click);
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnReset.Location = new System.Drawing.Point(252, 196);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(218, 58);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset treadmill";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCalibrateGroundPlane);
            this.groupBox2.Controls.Add(this.chkDelayStart);
            this.groupBox2.Controls.Add(this.btnStopData);
            this.groupBox2.Controls.Add(this.chkLink);
            this.groupBox2.Controls.Add(this.btnStartData);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(682, 16);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(415, 264);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data collection";
            // 
            // btnCalibrateGroundPlane
            // 
            this.btnCalibrateGroundPlane.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnCalibrateGroundPlane.Location = new System.Drawing.Point(185, 21);
            this.btnCalibrateGroundPlane.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalibrateGroundPlane.Name = "btnCalibrateGroundPlane";
            this.btnCalibrateGroundPlane.Size = new System.Drawing.Size(154, 58);
            this.btnCalibrateGroundPlane.TabIndex = 6;
            this.btnCalibrateGroundPlane.Text = "Calibrate Ground Plane";
            this.btnCalibrateGroundPlane.UseVisualStyleBackColor = true;
            this.btnCalibrateGroundPlane.Click += new System.EventHandler(this.btnCalibrateGroundPlane_Click);
            // 
            // chkDelayStart
            // 
            this.chkDelayStart.AutoSize = true;
            this.chkDelayStart.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.chkDelayStart.Location = new System.Drawing.Point(9, 89);
            this.chkDelayStart.Margin = new System.Windows.Forms.Padding(4);
            this.chkDelayStart.Name = "chkDelayStart";
            this.chkDelayStart.Size = new System.Drawing.Size(122, 21);
            this.chkDelayStart.TabIndex = 5;
            this.chkDelayStart.Text = "Delayed start";
            this.chkDelayStart.UseVisualStyleBackColor = true;
            // 
            // btnStopData
            // 
            this.btnStopData.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnStopData.Location = new System.Drawing.Point(8, 193);
            this.btnStopData.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopData.Name = "btnStopData";
            this.btnStopData.Size = new System.Drawing.Size(105, 58);
            this.btnStopData.TabIndex = 2;
            this.btnStopData.Text = "Stop data";
            this.btnStopData.UseVisualStyleBackColor = true;
            this.btnStopData.Click += new System.EventHandler(this.btnStopData_Click);
            // 
            // chkLink
            // 
            this.chkLink.AutoSize = true;
            this.chkLink.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.chkLink.Location = new System.Drawing.Point(9, 31);
            this.chkLink.Margin = new System.Windows.Forms.Padding(4);
            this.chkLink.Name = "chkLink";
            this.chkLink.Size = new System.Drawing.Size(147, 38);
            this.chkLink.TabIndex = 0;
            this.chkLink.Text = "Link to treadmill\r\nStart/Stop";
            this.chkLink.UseVisualStyleBackColor = true;
            this.chkLink.CheckedChanged += new System.EventHandler(this.chkLink_CheckedChanged);
            // 
            // btnStartData
            // 
            this.btnStartData.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.btnStartData.Location = new System.Drawing.Point(8, 124);
            this.btnStartData.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartData.Name = "btnStartData";
            this.btnStartData.Size = new System.Drawing.Size(105, 58);
            this.btnStartData.TabIndex = 1;
            this.btnStartData.Text = "Start data";
            this.btnStartData.UseVisualStyleBackColor = true;
            this.btnStartData.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbLeftToe
            // 
            this.cbLeftToe.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbLeftToe.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.cbLeftToe.Location = new System.Drawing.Point(1357, 104);
            this.cbLeftToe.Margin = new System.Windows.Forms.Padding(4);
            this.cbLeftToe.Name = "cbLeftToe";
            this.cbLeftToe.Size = new System.Drawing.Size(87, 44);
            this.cbLeftToe.TabIndex = 14;
            this.cbLeftToe.Text = "Left toe";
            this.cbLeftToe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbLeftToe.UseVisualStyleBackColor = true;
            // 
            // cbLeftHeel
            // 
            this.cbLeftHeel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbLeftHeel.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.cbLeftHeel.Location = new System.Drawing.Point(1357, 156);
            this.cbLeftHeel.Margin = new System.Windows.Forms.Padding(4);
            this.cbLeftHeel.Name = "cbLeftHeel";
            this.cbLeftHeel.Size = new System.Drawing.Size(87, 43);
            this.cbLeftHeel.TabIndex = 15;
            this.cbLeftHeel.Text = "Left heel";
            this.cbLeftHeel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbLeftHeel.UseVisualStyleBackColor = true;
            // 
            // cbRightToe
            // 
            this.cbRightToe.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbRightToe.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.cbRightToe.Location = new System.Drawing.Point(1452, 104);
            this.cbRightToe.Margin = new System.Windows.Forms.Padding(4);
            this.cbRightToe.Name = "cbRightToe";
            this.cbRightToe.Size = new System.Drawing.Size(87, 42);
            this.cbRightToe.TabIndex = 16;
            this.cbRightToe.Text = "Right toe";
            this.cbRightToe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbRightToe.UseVisualStyleBackColor = true;
            // 
            // cbRightHeel
            // 
            this.cbRightHeel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbRightHeel.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.cbRightHeel.Location = new System.Drawing.Point(1452, 154);
            this.cbRightHeel.Margin = new System.Windows.Forms.Padding(4);
            this.cbRightHeel.Name = "cbRightHeel";
            this.cbRightHeel.Size = new System.Drawing.Size(87, 44);
            this.cbRightHeel.TabIndex = 17;
            this.cbRightHeel.Text = "Right heel";
            this.cbRightHeel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbRightHeel.UseVisualStyleBackColor = true;
            // 
            // cbSonify
            // 
            this.cbSonify.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbSonify.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.cbSonify.Location = new System.Drawing.Point(1340, 37);
            this.cbSonify.Margin = new System.Windows.Forms.Padding(4);
            this.cbSonify.Name = "cbSonify";
            this.cbSonify.Size = new System.Drawing.Size(218, 56);
            this.cbSonify.TabIndex = 18;
            this.cbSonify.Text = "Sound On";
            this.cbSonify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbSonify.UseVisualStyleBackColor = true;
            // 
            // titleSwingStance
            // 
            this.titleSwingStance.AutoSize = true;
            this.titleSwingStance.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleSwingStance.Location = new System.Drawing.Point(1143, 51);
            this.titleSwingStance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleSwingStance.Name = "titleSwingStance";
            this.titleSwingStance.Size = new System.Drawing.Size(149, 17);
            this.titleSwingStance.TabIndex = 19;
            this.titleSwingStance.Text = "Swing stance SI (%):";
            // 
            // dataSwingStance
            // 
            this.dataSwingStance.AutoSize = true;
            this.dataSwingStance.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataSwingStance.Location = new System.Drawing.Point(1309, 51);
            this.dataSwingStance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataSwingStance.Name = "dataSwingStance";
            this.dataSwingStance.Size = new System.Drawing.Size(17, 17);
            this.dataSwingStance.TabIndex = 20;
            this.dataSwingStance.Text = "0";
            // 
            // titleCycleDuration
            // 
            this.titleCycleDuration.AutoSize = true;
            this.titleCycleDuration.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleCycleDuration.Location = new System.Drawing.Point(1131, 80);
            this.titleCycleDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleCycleDuration.Name = "titleCycleDuration";
            this.titleCycleDuration.Size = new System.Drawing.Size(161, 17);
            this.titleCycleDuration.TabIndex = 21;
            this.titleCycleDuration.Text = "Cycle duration SI (%):";
            // 
            // dataCycleDuration
            // 
            this.dataCycleDuration.AutoSize = true;
            this.dataCycleDuration.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataCycleDuration.Location = new System.Drawing.Point(1309, 80);
            this.dataCycleDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataCycleDuration.Name = "dataCycleDuration";
            this.dataCycleDuration.Size = new System.Drawing.Size(17, 17);
            this.dataCycleDuration.TabIndex = 22;
            this.dataCycleDuration.Text = "0";
            // 
            // titleStepInterval
            // 
            this.titleStepInterval.AutoSize = true;
            this.titleStepInterval.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleStepInterval.Location = new System.Drawing.Point(1145, 110);
            this.titleStepInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleStepInterval.Name = "titleStepInterval";
            this.titleStepInterval.Size = new System.Drawing.Size(147, 17);
            this.titleStepInterval.TabIndex = 23;
            this.titleStepInterval.Text = "Step interval SI (%):";
            // 
            // dataStepInterval
            // 
            this.dataStepInterval.AutoSize = true;
            this.dataStepInterval.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataStepInterval.Location = new System.Drawing.Point(1309, 110);
            this.dataStepInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataStepInterval.Name = "dataStepInterval";
            this.dataStepInterval.Size = new System.Drawing.Size(17, 17);
            this.dataStepInterval.TabIndex = 24;
            this.dataStepInterval.Text = "0";
            // 
            // titleDoubleSupport
            // 
            this.titleDoubleSupport.AutoSize = true;
            this.titleDoubleSupport.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleDoubleSupport.Location = new System.Drawing.Point(1125, 139);
            this.titleDoubleSupport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleDoubleSupport.Name = "titleDoubleSupport";
            this.titleDoubleSupport.Size = new System.Drawing.Size(167, 17);
            this.titleDoubleSupport.TabIndex = 25;
            this.titleDoubleSupport.Text = "Double support SI (%):";
            // 
            // dataDoubleSupport
            // 
            this.dataDoubleSupport.AutoSize = true;
            this.dataDoubleSupport.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataDoubleSupport.Location = new System.Drawing.Point(1309, 139);
            this.dataDoubleSupport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataDoubleSupport.Name = "dataDoubleSupport";
            this.dataDoubleSupport.Size = new System.Drawing.Size(17, 17);
            this.dataDoubleSupport.TabIndex = 26;
            this.dataDoubleSupport.Text = "0";
            // 
            // titleStrideLength
            // 
            this.titleStrideLength.AutoSize = true;
            this.titleStrideLength.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleStrideLength.Location = new System.Drawing.Point(1105, 185);
            this.titleStrideLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleStrideLength.Name = "titleStrideLength";
            this.titleStrideLength.Size = new System.Drawing.Size(187, 17);
            this.titleStrideLength.TabIndex = 27;
            this.titleStrideLength.Text = "Mean stride length (mm):";
            // 
            // dataStrideLength
            // 
            this.dataStrideLength.AutoSize = true;
            this.dataStrideLength.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataStrideLength.Location = new System.Drawing.Point(1309, 185);
            this.dataStrideLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataStrideLength.Name = "dataStrideLength";
            this.dataStrideLength.Size = new System.Drawing.Size(17, 17);
            this.dataStrideLength.TabIndex = 28;
            this.dataStrideLength.Text = "0";
            // 
            // titleStrideDuration
            // 
            this.titleStrideDuration.AutoSize = true;
            this.titleStrideDuration.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.titleStrideDuration.Location = new System.Drawing.Point(1107, 209);
            this.titleStrideDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titleStrideDuration.Name = "titleStrideDuration";
            this.titleStrideDuration.Size = new System.Drawing.Size(195, 17);
            this.titleStrideDuration.TabIndex = 29;
            this.titleStrideDuration.Text = "Mean stride duration (ms):";
            // 
            // dataStrideDuration
            // 
            this.dataStrideDuration.AutoSize = true;
            this.dataStrideDuration.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.dataStrideDuration.Location = new System.Drawing.Point(1309, 209);
            this.dataStrideDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dataStrideDuration.Name = "dataStrideDuration";
            this.dataStrideDuration.Size = new System.Drawing.Size(17, 17);
            this.dataStrideDuration.TabIndex = 30;
            this.dataStrideDuration.Text = "0";
            // 
            // frmUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1589, 293);
            this.Controls.Add(this.dataStrideDuration);
            this.Controls.Add(this.titleStrideDuration);
            this.Controls.Add(this.dataStrideLength);
            this.Controls.Add(this.cbSonify);
            this.Controls.Add(this.titleStrideLength);
            this.Controls.Add(this.dataDoubleSupport);
            this.Controls.Add(this.titleDoubleSupport);
            this.Controls.Add(this.dataStepInterval);
            this.Controls.Add(this.titleStepInterval);
            this.Controls.Add(this.dataCycleDuration);
            this.Controls.Add(this.titleCycleDuration);
            this.Controls.Add(this.dataSwingStance);
            this.Controls.Add(this.titleSwingStance);
            this.Controls.Add(this.cbRightToe);
            this.Controls.Add(this.cbLeftHeel);
            this.Controls.Add(this.cbLeftToe);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEndSession);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbRightHeel);
            this.Font = new System.Drawing.Font("Lucida Sans", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "StromoLight Diagnostics";
            this.Load += new System.EventHandler(this.frmUI_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudElevation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnStartTreadmill;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.NumericUpDown nudElevation;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblElevation;
        private System.Windows.Forms.Button btnEndSession;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDisengage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkLink;
        private System.Windows.Forms.Button btnStartData;
        private System.Windows.Forms.Button btnStopData;
        private System.Windows.Forms.CheckBox chkDelayStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbLeftToe;
        private System.Windows.Forms.CheckBox cbLeftHeel;
        private System.Windows.Forms.CheckBox cbRightToe;
        private System.Windows.Forms.CheckBox cbRightHeel;
        private System.Windows.Forms.CheckBox cbSonify;
        private System.Windows.Forms.Label titleSwingStance;
        private System.Windows.Forms.Label dataSwingStance;
        private System.Windows.Forms.Label titleCycleDuration;
        private System.Windows.Forms.Label dataCycleDuration;
        private System.Windows.Forms.Label titleStepInterval;
        private System.Windows.Forms.Label dataStepInterval;
        private System.Windows.Forms.Label titleDoubleSupport;
        private System.Windows.Forms.Label dataDoubleSupport;
        private System.Windows.Forms.Label titleStrideLength;
        private System.Windows.Forms.Label dataStrideLength;
        private System.Windows.Forms.Label titleStrideDuration;
        private System.Windows.Forms.Label dataStrideDuration;
        private System.Windows.Forms.Button btnCalibrateGroundPlane;

    }
}

