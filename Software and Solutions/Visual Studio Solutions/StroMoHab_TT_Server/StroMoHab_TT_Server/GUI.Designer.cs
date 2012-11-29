namespace StroMoHab_TT_Server.Forms
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
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.labelTTVersionText = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTS = new System.Windows.Forms.Label();
            this.labelStatusTSText = new System.Windows.Forms.Label();
            this.labelStatusVTText = new System.Windows.Forms.Label();
            this.labelStatusVT = new System.Windows.Forms.Label();
            this.labelServerIPText = new System.Windows.Forms.Label();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.labelVersionText = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStatusVMText = new System.Windows.Forms.Label();
            this.labelStatusVM = new System.Windows.Forms.Label();
            this.labelStatusTCPText = new System.Windows.Forms.Label();
            this.labelStatusCCText = new System.Windows.Forms.Label();
            this.labelStatusMCText = new System.Windows.Forms.Label();
            this.labelStatusTCP = new System.Windows.Forms.Label();
            this.labelStatusCC = new System.Windows.Forms.Label();
            this.labelStatusMC = new System.Windows.Forms.Label();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.buttonCalibrateTreadmill = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRestartMC = new System.Windows.Forms.Button();
            this.comboBoxCOMPort = new System.Windows.Forms.ComboBox();
            this.buttonRestartTM = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCalibrationTrackablesJointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTasksPatientsSessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCalibrationTrackablesJointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTasksPatientsSessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemControlTreadmill = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxStatus.SuspendLayout();
            this.groupBoxTasks.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Running...";
            this.notifyIcon.BalloonTipTitle = "StroMoHab Server";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "StroMoHab Server";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(203, 54);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openToolStripMenuItem.Text = "Open StroMoHab Server";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StroMoHab_TT_Server.Properties.Resources.StromohabLogosmall;
            this.pictureBox1.Location = new System.Drawing.Point(1, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(443, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxStatus.Controls.Add(this.labelTTVersionText);
            this.groupBoxStatus.Controls.Add(this.label7);
            this.groupBoxStatus.Controls.Add(this.labelTS);
            this.groupBoxStatus.Controls.Add(this.labelStatusTSText);
            this.groupBoxStatus.Controls.Add(this.labelStatusVTText);
            this.groupBoxStatus.Controls.Add(this.labelStatusVT);
            this.groupBoxStatus.Controls.Add(this.labelServerIPText);
            this.groupBoxStatus.Controls.Add(this.labelServerIP);
            this.groupBoxStatus.Controls.Add(this.labelVersionText);
            this.groupBoxStatus.Controls.Add(this.labelVersion);
            this.groupBoxStatus.Controls.Add(this.label5);
            this.groupBoxStatus.Controls.Add(this.label4);
            this.groupBoxStatus.Controls.Add(this.label3);
            this.groupBoxStatus.Controls.Add(this.label2);
            this.groupBoxStatus.Controls.Add(this.labelStatusVMText);
            this.groupBoxStatus.Controls.Add(this.labelStatusVM);
            this.groupBoxStatus.Controls.Add(this.labelStatusTCPText);
            this.groupBoxStatus.Controls.Add(this.labelStatusCCText);
            this.groupBoxStatus.Controls.Add(this.labelStatusMCText);
            this.groupBoxStatus.Controls.Add(this.labelStatusTCP);
            this.groupBoxStatus.Controls.Add(this.labelStatusCC);
            this.groupBoxStatus.Controls.Add(this.labelStatusMC);
            this.groupBoxStatus.Location = new System.Drawing.Point(12, 177);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(420, 156);
            this.groupBoxStatus.TabIndex = 8;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // labelTTVersionText
            // 
            this.labelTTVersionText.AutoSize = true;
            this.labelTTVersionText.Location = new System.Drawing.Point(145, 110);
            this.labelTTVersionText.Name = "labelTTVersionText";
            this.labelTTVersionText.Size = new System.Drawing.Size(13, 13);
            this.labelTTVersionText.TabIndex = 23;
            this.labelTTVersionText.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Tracking Tools Version :";
            // 
            // labelTS
            // 
            this.labelTS.AutoSize = true;
            this.labelTS.Location = new System.Drawing.Point(271, 111);
            this.labelTS.Name = "labelTS";
            this.labelTS.Size = new System.Drawing.Size(89, 13);
            this.labelTS.TabIndex = 21;
            this.labelTS.Text = "Treadmill Speed :";
            // 
            // labelStatusTSText
            // 
            this.labelStatusTSText.AutoSize = true;
            this.labelStatusTSText.Location = new System.Drawing.Point(361, 112);
            this.labelStatusTSText.Name = "labelStatusTSText";
            this.labelStatusTSText.Size = new System.Drawing.Size(13, 13);
            this.labelStatusTSText.TabIndex = 20;
            this.labelStatusTSText.Text = "0";
            // 
            // labelStatusVTText
            // 
            this.labelStatusVTText.AutoSize = true;
            this.labelStatusVTText.Location = new System.Drawing.Point(361, 41);
            this.labelStatusVTText.Name = "labelStatusVTText";
            this.labelStatusVTText.Size = new System.Drawing.Size(13, 13);
            this.labelStatusVTText.TabIndex = 19;
            this.labelStatusVTText.Text = "0";
            // 
            // labelStatusVT
            // 
            this.labelStatusVT.AutoSize = true;
            this.labelStatusVT.Location = new System.Drawing.Point(261, 41);
            this.labelStatusVT.Name = "labelStatusVT";
            this.labelStatusVT.Size = new System.Drawing.Size(99, 13);
            this.labelStatusVT.TabIndex = 18;
            this.labelStatusVT.Text = "Visible Trackables :";
            // 
            // labelServerIPText
            // 
            this.labelServerIPText.AutoSize = true;
            this.labelServerIPText.Location = new System.Drawing.Point(144, 64);
            this.labelServerIPText.Name = "labelServerIPText";
            this.labelServerIPText.Size = new System.Drawing.Size(52, 13);
            this.labelServerIPText.TabIndex = 17;
            this.labelServerIPText.Text = "127.0.0.1";
            // 
            // labelServerIP
            // 
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Location = new System.Drawing.Point(85, 64);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(60, 13);
            this.labelServerIP.TabIndex = 16;
            this.labelServerIP.Text = "Server IP : ";
            // 
            // labelVersionText
            // 
            this.labelVersionText.AutoSize = true;
            this.labelVersionText.Location = new System.Drawing.Point(145, 87);
            this.labelVersionText.Name = "labelVersionText";
            this.labelVersionText.Size = new System.Drawing.Size(13, 13);
            this.labelVersionText.TabIndex = 15;
            this.labelVersionText.Text = "0";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(60, 87);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(82, 13);
            this.labelVersion.TabIndex = 14;
            this.labelVersion.Text = "Server Version :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Marker 1 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Marker 0 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "0";
            // 
            // labelStatusVMText
            // 
            this.labelStatusVMText.AutoSize = true;
            this.labelStatusVMText.Location = new System.Drawing.Point(361, 18);
            this.labelStatusVMText.Name = "labelStatusVMText";
            this.labelStatusVMText.Size = new System.Drawing.Size(13, 13);
            this.labelStatusVMText.TabIndex = 9;
            this.labelStatusVMText.Text = "0";
            // 
            // labelStatusVM
            // 
            this.labelStatusVM.AutoSize = true;
            this.labelStatusVM.Location = new System.Drawing.Point(276, 18);
            this.labelStatusVM.Name = "labelStatusVM";
            this.labelStatusVM.Size = new System.Drawing.Size(84, 13);
            this.labelStatusVM.TabIndex = 8;
            this.labelStatusVM.Text = "Visible Markers :";
            // 
            // labelStatusTCPText
            // 
            this.labelStatusTCPText.AutoSize = true;
            this.labelStatusTCPText.Location = new System.Drawing.Point(145, 133);
            this.labelStatusTCPText.Name = "labelStatusTCPText";
            this.labelStatusTCPText.Size = new System.Drawing.Size(61, 13);
            this.labelStatusTCPText.TabIndex = 5;
            this.labelStatusTCPText.Text = "Not Started";
            // 
            // labelStatusCCText
            // 
            this.labelStatusCCText.AutoSize = true;
            this.labelStatusCCText.Location = new System.Drawing.Point(146, 18);
            this.labelStatusCCText.Name = "labelStatusCCText";
            this.labelStatusCCText.Size = new System.Drawing.Size(13, 13);
            this.labelStatusCCText.TabIndex = 4;
            this.labelStatusCCText.Text = "0";
            // 
            // labelStatusMCText
            // 
            this.labelStatusMCText.AutoSize = true;
            this.labelStatusMCText.Location = new System.Drawing.Point(144, 41);
            this.labelStatusMCText.Name = "labelStatusMCText";
            this.labelStatusMCText.Size = new System.Drawing.Size(0, 13);
            this.labelStatusMCText.TabIndex = 3;
            // 
            // labelStatusTCP
            // 
            this.labelStatusTCP.AutoSize = true;
            this.labelStatusTCP.Location = new System.Drawing.Point(65, 133);
            this.labelStatusTCP.Name = "labelStatusTCP";
            this.labelStatusTCP.Size = new System.Drawing.Size(80, 13);
            this.labelStatusTCP.TabIndex = 2;
            this.labelStatusTCP.Text = "Server Status : ";
            // 
            // labelStatusCC
            // 
            this.labelStatusCC.AutoSize = true;
            this.labelStatusCC.Location = new System.Drawing.Point(34, 18);
            this.labelStatusCC.Name = "labelStatusCC";
            this.labelStatusCC.Size = new System.Drawing.Size(112, 13);
            this.labelStatusCC.TabIndex = 1;
            this.labelStatusCC.Text = "Connected Cameras : ";
            // 
            // labelStatusMC
            // 
            this.labelStatusMC.AutoSize = true;
            this.labelStatusMC.Location = new System.Drawing.Point(57, 41);
            this.labelStatusMC.Name = "labelStatusMC";
            this.labelStatusMC.Size = new System.Drawing.Size(85, 13);
            this.labelStatusMC.TabIndex = 0;
            this.labelStatusMC.Text = "Motion Capture :";
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.Controls.Add(this.buttonCalibrateTreadmill);
            this.groupBoxTasks.Controls.Add(this.checkBox1);
            this.groupBoxTasks.Controls.Add(this.label1);
            this.groupBoxTasks.Controls.Add(this.buttonRestartMC);
            this.groupBoxTasks.Controls.Add(this.comboBoxCOMPort);
            this.groupBoxTasks.Controls.Add(this.buttonRestartTM);
            this.groupBoxTasks.Location = new System.Drawing.Point(12, 341);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(420, 87);
            this.groupBoxTasks.TabIndex = 9;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "Tasks";
            // 
            // buttonCalibrateTreadmill
            // 
            this.buttonCalibrateTreadmill.Location = new System.Drawing.Point(293, 16);
            this.buttonCalibrateTreadmill.Name = "buttonCalibrateTreadmill";
            this.buttonCalibrateTreadmill.Size = new System.Drawing.Size(95, 58);
            this.buttonCalibrateTreadmill.TabIndex = 5;
            this.buttonCalibrateTreadmill.Text = "Calibrate Treadmill Speed";
            this.buttonCalibrateTreadmill.UseVisualStyleBackColor = true;
            this.buttonCalibrateTreadmill.Click += new System.EventHandler(this.buttonCalibrateTreadmill_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(147, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Detect Treadmill Speed";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Treadmill Port";
            this.label1.Visible = false;
            // 
            // buttonRestartMC
            // 
            this.buttonRestartMC.Location = new System.Drawing.Point(27, 16);
            this.buttonRestartMC.Name = "buttonRestartMC";
            this.buttonRestartMC.Size = new System.Drawing.Size(95, 58);
            this.buttonRestartMC.TabIndex = 0;
            this.buttonRestartMC.Text = "Restart the Motion Capture Engine";
            this.buttonRestartMC.UseVisualStyleBackColor = true;
            this.buttonRestartMC.Click += new System.EventHandler(this.buttonRestartMC_Click);
            // 
            // comboBoxCOMPort
            // 
            this.comboBoxCOMPort.FormattingEnabled = true;
            this.comboBoxCOMPort.Location = new System.Drawing.Point(269, 44);
            this.comboBoxCOMPort.Name = "comboBoxCOMPort";
            this.comboBoxCOMPort.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCOMPort.TabIndex = 2;
            this.comboBoxCOMPort.Visible = false;
            this.comboBoxCOMPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxCOMPort_SelectedIndexChanged);
            // 
            // buttonRestartTM
            // 
            this.buttonRestartTM.Location = new System.Drawing.Point(154, 16);
            this.buttonRestartTM.Name = "buttonRestartTM";
            this.buttonRestartTM.Size = new System.Drawing.Size(95, 58);
            this.buttonRestartTM.TabIndex = 1;
            this.buttonRestartTM.Text = "Re-Detect Treadmill";
            this.buttonRestartTM.UseVisualStyleBackColor = true;
            this.buttonRestartTM.Visible = false;
            this.buttonRestartTM.Click += new System.EventHandler(this.buttonRestartTM_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(444, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDataToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemControlTreadmill,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importDataToolStripMenuItem
            // 
            this.importDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importCalibrationTrackablesJointsToolStripMenuItem,
            this.importTasksPatientsSessionsToolStripMenuItem});
            this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
            this.importDataToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.importDataToolStripMenuItem.Text = "Import";
            // 
            // importCalibrationTrackablesJointsToolStripMenuItem
            // 
            this.importCalibrationTrackablesJointsToolStripMenuItem.Name = "importCalibrationTrackablesJointsToolStripMenuItem";
            this.importCalibrationTrackablesJointsToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.importCalibrationTrackablesJointsToolStripMenuItem.Text = "Import Settings (Calibration/Trackables/Joints)";
            this.importCalibrationTrackablesJointsToolStripMenuItem.Click += new System.EventHandler(this.importCalibrationTrackablesJointsToolStripMenuItem_Click);
            // 
            // importTasksPatientsSessionsToolStripMenuItem
            // 
            this.importTasksPatientsSessionsToolStripMenuItem.Name = "importTasksPatientsSessionsToolStripMenuItem";
            this.importTasksPatientsSessionsToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.importTasksPatientsSessionsToolStripMenuItem.Text = "Import Data (Tasks/Patients/Sessions/Clinicians)";
            this.importTasksPatientsSessionsToolStripMenuItem.Click += new System.EventHandler(this.importTasksPatientsSessionsToolStripMenuItem_Click);
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCalibrationTrackablesJointsToolStripMenuItem,
            this.exportTasksPatientsSessionsToolStripMenuItem});
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.exportDataToolStripMenuItem.Text = "Export";
            // 
            // exportCalibrationTrackablesJointsToolStripMenuItem
            // 
            this.exportCalibrationTrackablesJointsToolStripMenuItem.Name = "exportCalibrationTrackablesJointsToolStripMenuItem";
            this.exportCalibrationTrackablesJointsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.exportCalibrationTrackablesJointsToolStripMenuItem.Text = "Export Settings (Calibration/Trackables/Joints)";
            this.exportCalibrationTrackablesJointsToolStripMenuItem.Click += new System.EventHandler(this.exportCalibrationTrackablesJointsToolStripMenuItem_Click);
            // 
            // exportTasksPatientsSessionsToolStripMenuItem
            // 
            this.exportTasksPatientsSessionsToolStripMenuItem.Name = "exportTasksPatientsSessionsToolStripMenuItem";
            this.exportTasksPatientsSessionsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.exportTasksPatientsSessionsToolStripMenuItem.Text = "Export Data (Tasks/Patients/Sessions/Clinicians)";
            this.exportTasksPatientsSessionsToolStripMenuItem.Click += new System.EventHandler(this.exportTasksPatientsSessionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
            // 
            // toolStripMenuItemControlTreadmill
            // 
            this.toolStripMenuItemControlTreadmill.CheckOnClick = true;
            this.toolStripMenuItemControlTreadmill.Name = "toolStripMenuItemControlTreadmill";
            this.toolStripMenuItemControlTreadmill.Size = new System.Drawing.Size(270, 22);
            this.toolStripMenuItemControlTreadmill.Text = "Enable Software Controlled Treadmill";
            this.toolStripMenuItemControlTreadmill.CheckStateChanged += new System.EventHandler(this.toolStripMenuItemControlTreadmill_CheckStateChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(267, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(270, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(444, 442);
            this.Controls.Add(this.groupBoxTasks);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBoxStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 480);
            this.MinimumSize = new System.Drawing.Size(460, 480);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StroMoHab Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.Resize += new System.EventHandler(this.GUI_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.groupBoxTasks.ResumeLayout(false);
            this.groupBoxTasks.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label labelStatusTCPText;
        private System.Windows.Forms.Label labelStatusCCText;
        private System.Windows.Forms.Label labelStatusMCText;
        private System.Windows.Forms.Label labelStatusTCP;
        private System.Windows.Forms.Label labelStatusCC;
        private System.Windows.Forms.Label labelStatusMC;
        private System.Windows.Forms.GroupBox groupBoxTasks;
        private System.Windows.Forms.Button buttonRestartMC;
        private System.Windows.Forms.Button buttonRestartTM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox comboBoxCOMPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStatusVMText;
        private System.Windows.Forms.Label labelStatusVM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelVersionText;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelServerIPText;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.Label labelStatusVTText;
        private System.Windows.Forms.Label labelStatusVT;
        private System.Windows.Forms.Label labelTS;
        private System.Windows.Forms.Label labelStatusTSText;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonCalibrateTreadmill;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importCalibrationTrackablesJointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTasksPatientsSessionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCalibrationTrackablesJointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTasksPatientsSessionsToolStripMenuItem;
        private System.Windows.Forms.Label labelTTVersionText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemControlTreadmill;
    }
}

