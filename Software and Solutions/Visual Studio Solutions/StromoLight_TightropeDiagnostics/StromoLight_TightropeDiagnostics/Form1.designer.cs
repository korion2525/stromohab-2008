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
            this.btnStartTreadmill = new System.Windows.Forms.Button();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudTaskTime = new System.Windows.Forms.NumericUpDown();
            this.btnSavePersonData = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.lblPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInitials = new System.Windows.Forms.TextBox();
            this.nudAge = new System.Windows.Forms.NumericUpDown();
            this.rbF = new System.Windows.Forms.RadioButton();
            this.rbM = new System.Windows.Forms.RadioButton();
            this.gbGender = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTestID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbFootPref = new System.Windows.Forms.GroupBox();
            this.rbNoPref = new System.Windows.Forms.RadioButton();
            this.rbRight = new System.Windows.Forms.RadioButton();
            this.rbLeft = new System.Windows.Forms.RadioButton();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTestNumber = new System.Windows.Forms.TextBox();
            this.lbFileNames = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPlaySound = new System.Windows.Forms.Button();
            this.cbFrequency = new System.Windows.Forms.CheckBox();
            this.cbDiscrete = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkLink = new System.Windows.Forms.CheckBox();
            this.chkDelayStart = new System.Windows.Forms.CheckBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtStride = new System.Windows.Forms.TextBox();
            this.txtCDSI = new System.Windows.Forms.TextBox();
            this.txtSISI = new System.Windows.Forms.TextBox();
            this.txtDSSI = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSSSI = new System.Windows.Forms.TextBox();
            this.btnVisualFeedback = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAge)).BeginInit();
            this.gbGender.SuspendLayout();
            this.gbFootPref.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartTreadmill
            // 
            this.btnStartTreadmill.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnStartTreadmill.Location = new System.Drawing.Point(8, 99);
            this.btnStartTreadmill.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartTreadmill.Name = "btnStartTreadmill";
            this.btnStartTreadmill.Size = new System.Drawing.Size(235, 34);
            this.btnStartTreadmill.TabIndex = 2;
            this.btnStartTreadmill.Text = "Start task";
            this.btnStartTreadmill.UseVisualStyleBackColor = true;
            this.btnStartTreadmill.Click += new System.EventHandler(this.btnStartTreadmill_Click);
            // 
            // nudSpeed
            // 
            this.nudSpeed.DecimalPlaces = 1;
            this.nudSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.nudSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSpeed.Location = new System.Drawing.Point(169, 38);
            this.nudSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.nudSpeed.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(75, 22);
            this.nudSpeed.TabIndex = 3;
            this.nudSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSpeed.ValueChanged += new System.EventHandler(this.nudSpeed_ValueChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSpeed.Location = new System.Drawing.Point(6, 38);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(97, 18);
            this.lblSpeed.TabIndex = 5;
            this.lblSpeed.Text = "Speed (mph):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnVisualFeedback);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.nudTaskTime);
            this.groupBox1.Controls.Add(this.btnStartTreadmill);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.nudSpeed);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(18, 97);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(257, 203);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Treadmill control";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "Task time (secs.)";
            // 
            // nudTaskTime
            // 
            this.nudTaskTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTaskTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTaskTime.Location = new System.Drawing.Point(169, 69);
            this.nudTaskTime.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudTaskTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTaskTime.Name = "nudTaskTime";
            this.nudTaskTime.Size = new System.Drawing.Size(75, 22);
            this.nudTaskTime.TabIndex = 6;
            this.nudTaskTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTaskTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTaskTime.ValueChanged += new System.EventHandler(this.nudTaskTime_ValueChanged);
            // 
            // btnSavePersonData
            // 
            this.btnSavePersonData.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSavePersonData.Location = new System.Drawing.Point(274, 173);
            this.btnSavePersonData.Name = "btnSavePersonData";
            this.btnSavePersonData.Size = new System.Drawing.Size(85, 81);
            this.btnSavePersonData.TabIndex = 48;
            this.btnSavePersonData.Text = "New patient";
            this.btnSavePersonData.UseVisualStyleBackColor = true;
            this.btnSavePersonData.Click += new System.EventHandler(this.btnSavePersonData_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtFileName.Location = new System.Drawing.Point(16, 303);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(132, 24);
            this.txtFileName.TabIndex = 31;
            this.txtFileName.Text = "TestID";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(306, 543);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(100, 25);
            this.btnFolder.TabIndex = 33;
            this.btnFolder.Text = "Save path";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Visible = false;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // fbdPath
            // 
            this.fbdPath.SelectedPath = "\\\\APPC05\\Users\\Public\\Documents\\Stromohab";
            this.fbdPath.HelpRequest += new System.EventHandler(this.fbdPath_HelpRequest);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(194, 513);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(282, 13);
            this.lblPath.TabIndex = 34;
            this.lblPath.Text = "\\\\Appc05\\Users\\Public\\Documents\\Stromohab\\";
            this.lblPath.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(13, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "Patient ID";
            // 
            // txtInitials
            // 
            this.txtInitials.Location = new System.Drawing.Point(86, 51);
            this.txtInitials.Name = "txtInitials";
            this.txtInitials.Size = new System.Drawing.Size(62, 24);
            this.txtInitials.TabIndex = 36;
            this.txtInitials.TextChanged += new System.EventHandler(this.txtInitials_TextChanged);
            // 
            // nudAge
            // 
            this.nudAge.Location = new System.Drawing.Point(317, 119);
            this.nudAge.Name = "nudAge";
            this.nudAge.Size = new System.Drawing.Size(42, 24);
            this.nudAge.TabIndex = 37;
            this.nudAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudAge.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // rbF
            // 
            this.rbF.AutoSize = true;
            this.rbF.Checked = true;
            this.rbF.Location = new System.Drawing.Point(7, 26);
            this.rbF.Name = "rbF";
            this.rbF.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbF.Size = new System.Drawing.Size(36, 22);
            this.rbF.TabIndex = 38;
            this.rbF.TabStop = true;
            this.rbF.Tag = "";
            this.rbF.Text = "F";
            this.rbF.UseVisualStyleBackColor = true;
            // 
            // rbM
            // 
            this.rbM.AutoSize = true;
            this.rbM.Location = new System.Drawing.Point(49, 26);
            this.rbM.Name = "rbM";
            this.rbM.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbM.Size = new System.Drawing.Size(40, 22);
            this.rbM.TabIndex = 39;
            this.rbM.Tag = "";
            this.rbM.Text = "M";
            this.rbM.UseVisualStyleBackColor = true;
            // 
            // gbGender
            // 
            this.gbGender.Controls.Add(this.rbM);
            this.gbGender.Controls.Add(this.rbF);
            this.gbGender.Location = new System.Drawing.Point(178, 99);
            this.gbGender.Name = "gbGender";
            this.gbGender.Size = new System.Drawing.Size(101, 53);
            this.gbGender.TabIndex = 40;
            this.gbGender.TabStop = false;
            this.gbGender.Text = "Gender";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(314, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 18);
            this.label2.TabIndex = 41;
            this.label2.Text = "Age";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(83, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 42;
            this.label3.Text = "Initials";
            // 
            // cbTestID
            // 
            this.cbTestID.FormattingEnabled = true;
            this.cbTestID.Items.AddRange(new object[] {
            "Baseline",
            "Vision",
            "Sound"});
            this.cbTestID.Location = new System.Drawing.Point(18, 51);
            this.cbTestID.Name = "cbTestID";
            this.cbTestID.Size = new System.Drawing.Size(117, 26);
            this.cbTestID.TabIndex = 43;
            this.cbTestID.Text = "Baseline";
            this.cbTestID.SelectedIndexChanged += new System.EventHandler(this.cbTestID_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(15, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 18);
            this.label4.TabIndex = 44;
            this.label4.Text = "Task feedback";
            // 
            // gbFootPref
            // 
            this.gbFootPref.Controls.Add(this.rbNoPref);
            this.gbFootPref.Controls.Add(this.rbRight);
            this.gbFootPref.Controls.Add(this.rbLeft);
            this.gbFootPref.Location = new System.Drawing.Point(318, 570);
            this.gbFootPref.Name = "gbFootPref";
            this.gbFootPref.Size = new System.Drawing.Size(174, 53);
            this.gbFootPref.TabIndex = 45;
            this.gbFootPref.TabStop = false;
            this.gbFootPref.Text = "Foot preference";
            this.gbFootPref.Visible = false;
            // 
            // rbNoPref
            // 
            this.rbNoPref.AutoSize = true;
            this.rbNoPref.Checked = true;
            this.rbNoPref.Location = new System.Drawing.Point(96, 25);
            this.rbNoPref.Name = "rbNoPref";
            this.rbNoPref.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbNoPref.Size = new System.Drawing.Size(62, 22);
            this.rbNoPref.TabIndex = 2;
            this.rbNoPref.TabStop = true;
            this.rbNoPref.Text = "None";
            this.rbNoPref.UseVisualStyleBackColor = true;
            this.rbNoPref.Visible = false;
            // 
            // rbRight
            // 
            this.rbRight.AutoSize = true;
            this.rbRight.Location = new System.Drawing.Point(51, 25);
            this.rbRight.Name = "rbRight";
            this.rbRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbRight.Size = new System.Drawing.Size(37, 22);
            this.rbRight.TabIndex = 1;
            this.rbRight.Text = "R";
            this.rbRight.UseVisualStyleBackColor = true;
            this.rbRight.Visible = false;
            // 
            // rbLeft
            // 
            this.rbLeft.AutoSize = true;
            this.rbLeft.Location = new System.Drawing.Point(6, 25);
            this.rbLeft.Name = "rbLeft";
            this.rbLeft.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbLeft.Size = new System.Drawing.Size(34, 22);
            this.rbLeft.TabIndex = 0;
            this.rbLeft.Text = "L";
            this.rbLeft.UseVisualStyleBackColor = true;
            this.rbLeft.Visible = false;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(16, 173);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(239, 81);
            this.txtNotes.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(13, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 47;
            this.label5.Text = "Notes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(167, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 18);
            this.label7.TabIndex = 50;
            this.label7.Text = "Task number";
            // 
            // txtTestNumber
            // 
            this.txtTestNumber.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTestNumber.Location = new System.Drawing.Point(203, 53);
            this.txtTestNumber.Name = "txtTestNumber";
            this.txtTestNumber.ReadOnly = true;
            this.txtTestNumber.Size = new System.Drawing.Size(29, 24);
            this.txtTestNumber.TabIndex = 51;
            this.txtTestNumber.Text = "0";
            this.txtTestNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbFileNames
            // 
            this.lbFileNames.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbFileNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileNames.FormattingEnabled = true;
            this.lbFileNames.Location = new System.Drawing.Point(178, 303);
            this.lbFileNames.Name = "lbFileNames";
            this.lbFileNames.Size = new System.Drawing.Size(191, 43);
            this.lbFileNames.TabIndex = 52;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(177, 282);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 18);
            this.label8.TabIndex = 53;
            this.label8.Text = "Task history";
            // 
            // btnPlaySound
            // 
            this.btnPlaySound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaySound.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPlaySound.Location = new System.Drawing.Point(9, 61);
            this.btnPlaySound.Name = "btnPlaySound";
            this.btnPlaySound.Size = new System.Drawing.Size(80, 27);
            this.btnPlaySound.TabIndex = 54;
            this.btnPlaySound.Text = "Play sound";
            this.btnPlaySound.UseVisualStyleBackColor = true;
            this.btnPlaySound.Click += new System.EventHandler(this.btnPlaySound_Click);
            // 
            // cbFrequency
            // 
            this.cbFrequency.AutoSize = true;
            this.cbFrequency.Checked = true;
            this.cbFrequency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrequency.Location = new System.Drawing.Point(148, 64);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(96, 22);
            this.cbFrequency.TabIndex = 55;
            this.cbFrequency.Text = "Frequency";
            this.cbFrequency.UseVisualStyleBackColor = true;
            // 
            // cbDiscrete
            // 
            this.cbDiscrete.AutoSize = true;
            this.cbDiscrete.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDiscrete.Location = new System.Drawing.Point(148, 36);
            this.cbDiscrete.Name = "cbDiscrete";
            this.cbDiscrete.Size = new System.Drawing.Size(82, 22);
            this.cbDiscrete.TabIndex = 56;
            this.cbDiscrete.Text = "Discrete";
            this.cbDiscrete.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(145, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 18);
            this.label10.TabIndex = 57;
            this.label10.Text = "Sound type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btnSavePersonData);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lbFileNames);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtNotes);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.gbGender);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.nudAge);
            this.groupBox3.Controls.Add(this.txtInitials);
            this.groupBox3.Controls.Add(this.txtFileName);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 435);
            this.groupBox3.TabIndex = 58;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Patient details";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Dr",
            "Mr",
            "Ms",
            "Mrs",
            "Miss",
            "Professor",
            "Sir",
            "Lady"});
            this.comboBox1.Location = new System.Drawing.Point(16, 48);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(61, 26);
            this.comboBox1.TabIndex = 54;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(13, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 18);
            this.label13.TabIndex = 53;
            this.label13.Text = "Preferred name";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(16, 120);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(132, 24);
            this.textBox3.TabIndex = 52;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(13, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 18);
            this.label12.TabIndex = 51;
            this.label12.Text = "Title";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(151, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 18);
            this.label11.TabIndex = 49;
            this.label11.Text = "Family name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(154, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 24);
            this.textBox1.TabIndex = 48;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPlaySound);
            this.groupBox4.Controls.Add(this.cbFrequency);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.cbDiscrete);
            this.groupBox4.Location = new System.Drawing.Point(18, 319);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 100);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Acoustic feedback";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.txtTestNumber);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cbTestID);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(415, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(289, 435);
            this.groupBox5.TabIndex = 60;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Task control";
            // 
            // chkLink
            // 
            this.chkLink.AutoSize = true;
            this.chkLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.chkLink.Location = new System.Drawing.Point(0, 34);
            this.chkLink.Margin = new System.Windows.Forms.Padding(4);
            this.chkLink.Name = "chkLink";
            this.chkLink.Size = new System.Drawing.Size(130, 40);
            this.chkLink.TabIndex = 0;
            this.chkLink.Text = "Link to treadmill\r\nStart/Stop";
            this.chkLink.UseVisualStyleBackColor = true;
            this.chkLink.CheckedChanged += new System.EventHandler(this.chkLink_CheckedChanged);
            // 
            // chkDelayStart
            // 
            this.chkDelayStart.AutoSize = true;
            this.chkDelayStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.chkDelayStart.Location = new System.Drawing.Point(7, 70);
            this.chkDelayStart.Margin = new System.Windows.Forms.Padding(4);
            this.chkDelayStart.Name = "chkDelayStart";
            this.chkDelayStart.Size = new System.Drawing.Size(113, 22);
            this.chkDelayStart.TabIndex = 5;
            this.chkDelayStart.Text = "Delayed start";
            this.chkDelayStart.UseVisualStyleBackColor = true;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(114, 13);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(23, 23);
            this.btnLoadData.TabIndex = 6;
            this.btnLoadData.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(7, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Load saved data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnLoadData);
            this.groupBox2.Controls.Add(this.chkDelayStart);
            this.groupBox2.Controls.Add(this.chkLink);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(44, 530);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(165, 98);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data collection";
            this.groupBox2.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtStride);
            this.groupBox6.Controls.Add(this.txtCDSI);
            this.groupBox6.Controls.Add(this.txtSISI);
            this.groupBox6.Controls.Add(this.txtDSSI);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.txtSSSI);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(719, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(290, 268);
            this.groupBox6.TabIndex = 61;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Gait  metrics";
            // 
            // txtStride
            // 
            this.txtStride.Location = new System.Drawing.Point(173, 30);
            this.txtStride.Name = "txtStride";
            this.txtStride.Size = new System.Drawing.Size(100, 24);
            this.txtStride.TabIndex = 11;
            // 
            // txtCDSI
            // 
            this.txtCDSI.Location = new System.Drawing.Point(173, 222);
            this.txtCDSI.Name = "txtCDSI";
            this.txtCDSI.Size = new System.Drawing.Size(100, 24);
            this.txtCDSI.TabIndex = 10;
            // 
            // txtSISI
            // 
            this.txtSISI.Location = new System.Drawing.Point(173, 192);
            this.txtSISI.Name = "txtSISI";
            this.txtSISI.Size = new System.Drawing.Size(100, 24);
            this.txtSISI.TabIndex = 9;
            // 
            // txtDSSI
            // 
            this.txtDSSI.Location = new System.Drawing.Point(173, 162);
            this.txtDSSI.Name = "txtDSSI";
            this.txtDSSI.Size = new System.Drawing.Size(100, 24);
            this.txtDSSI.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(38, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(129, 18);
            this.label19.TabIndex = 6;
            this.label19.Text = "Stride length (mm)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(65, 225);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(102, 18);
            this.label18.TabIndex = 5;
            this.label18.Text = "Cycle duration";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(79, 195);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 18);
            this.label17.TabIndex = 4;
            this.label17.Text = "Step interval";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 166);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 18);
            this.label16.TabIndex = 3;
            this.label16.Text = "Double support duration";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label15.Location = new System.Drawing.Point(13, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(253, 18);
            this.label15.TabIndex = 2;
            this.label15.Text = "Gait symmetry indices (per cent)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(38, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 18);
            this.label14.TabIndex = 1;
            this.label14.Text = "Swing stance ratio";
            // 
            // txtSSSI
            // 
            this.txtSSSI.Location = new System.Drawing.Point(173, 132);
            this.txtSSSI.Name = "txtSSSI";
            this.txtSSSI.Size = new System.Drawing.Size(100, 24);
            this.txtSSSI.TabIndex = 0;
            // 
            // btnVisualFeedback
            // 
            this.btnVisualFeedback.Location = new System.Drawing.Point(9, 149);
            this.btnVisualFeedback.Name = "btnVisualFeedback";
            this.btnVisualFeedback.Size = new System.Drawing.Size(234, 34);
            this.btnVisualFeedback.TabIndex = 8;
            this.btnVisualFeedback.Text = "Start visual feedback";
            this.btnVisualFeedback.UseVisualStyleBackColor = true;
            this.btnVisualFeedback.Click += new System.EventHandler(this.btnVisualFeedback_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(770, 319);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // frmUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1032, 464);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbFootPref);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "StromoLight Clinician Interface";
            this.Load += new System.EventHandler(this.frmUI_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAge)).EndInit();
            this.gbGender.ResumeLayout(false);
            this.gbGender.PerformLayout();
            this.gbFootPref.ResumeLayout(false);
            this.gbFootPref.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartTreadmill;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInitials;
        private System.Windows.Forms.NumericUpDown nudAge;
        private System.Windows.Forms.RadioButton rbF;
        private System.Windows.Forms.RadioButton rbM;
        private System.Windows.Forms.GroupBox gbGender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTestID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbFootPref;
        private System.Windows.Forms.RadioButton rbNoPref;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSavePersonData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTestNumber;
        private System.Windows.Forms.ListBox lbFileNames;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPlaySound;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudTaskTime;
        private System.Windows.Forms.CheckBox cbFrequency;
        private System.Windows.Forms.CheckBox cbDiscrete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkLink;
        private System.Windows.Forms.CheckBox chkDelayStart;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSSSI;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtStride;
        private System.Windows.Forms.TextBox txtCDSI;
        private System.Windows.Forms.TextBox txtSISI;
        private System.Windows.Forms.TextBox txtDSSI;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnVisualFeedback;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

