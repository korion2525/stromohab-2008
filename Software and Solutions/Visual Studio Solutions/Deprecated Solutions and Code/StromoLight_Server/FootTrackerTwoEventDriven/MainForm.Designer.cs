namespace Stromohab_MCE
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenCalibrationFile = new System.Windows.Forms.Button();
            this.textBoxCalibrationFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialogCalibrationFile = new System.Windows.Forms.OpenFileDialog();
            this.timerPollCameraInterface = new System.Windows.Forms.Timer(this.components);
            this.buttonCameraControl = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.buttonDisconnectAll = new System.Windows.Forms.Button();
            this.buttonUpdateCameraList = new System.Windows.Forms.Button();
            this.comboBoxTreadmillPort = new System.Windows.Forms.ComboBox();
            this.btnTreadmillControl = new System.Windows.Forms.Button();
            this.btnConnectToTreadmill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera Calibration File:";
            // 
            // buttonOpenCalibrationFile
            // 
            this.buttonOpenCalibrationFile.Location = new System.Drawing.Point(493, 27);
            this.buttonOpenCalibrationFile.Name = "buttonOpenCalibrationFile";
            this.buttonOpenCalibrationFile.Size = new System.Drawing.Size(56, 25);
            this.buttonOpenCalibrationFile.TabIndex = 1;
            this.buttonOpenCalibrationFile.Text = "Browse..";
            this.buttonOpenCalibrationFile.UseVisualStyleBackColor = true;
            this.buttonOpenCalibrationFile.Click += new System.EventHandler(this.buttonOpenCalibrationFile_Click);
            // 
            // textBoxCalibrationFilePath
            // 
            this.textBoxCalibrationFilePath.Location = new System.Drawing.Point(132, 27);
            this.textBoxCalibrationFilePath.Name = "textBoxCalibrationFilePath";
            this.textBoxCalibrationFilePath.Size = new System.Drawing.Size(339, 20);
            this.textBoxCalibrationFilePath.TabIndex = 2;
            this.textBoxCalibrationFilePath.Text = "C:\\\\cali.cal";
            // 
            // openFileDialogCalibrationFile
            // 
            this.openFileDialogCalibrationFile.FileName = "C:\\Mat\\MotionCapture\\Calibrations\\Calibration_051108.cal";
            this.openFileDialogCalibrationFile.Filter = "Calibration Files|*.cal";
            this.openFileDialogCalibrationFile.InitialDirectory = "C:\\Mat\\MotionCapture\\Calibrations";
            this.openFileDialogCalibrationFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogCalibrationFile_FileOk);
            // 
            // timerPollCameraInterface
            // 
            this.timerPollCameraInterface.Interval = 10;
            this.timerPollCameraInterface.Tick += new System.EventHandler(this.timerPollCameraInterface_Tick);
            // 
            // buttonCameraControl
            // 
            this.buttonCameraControl.Location = new System.Drawing.Point(474, 302);
            this.buttonCameraControl.Name = "buttonCameraControl";
            this.buttonCameraControl.Size = new System.Drawing.Size(75, 34);
            this.buttonCameraControl.TabIndex = 3;
            this.buttonCameraControl.Text = "Start Cameras";
            this.buttonCameraControl.UseVisualStyleBackColor = true;
            this.buttonCameraControl.Click += new System.EventHandler(this.buttonCameraControl_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Treadmill Port:";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 352);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(568, 22);
            this.statusBar1.TabIndex = 5;
            this.statusBar1.Text = "Cameras Stopped";
            // 
            // buttonDisconnectAll
            // 
            this.buttonDisconnectAll.Location = new System.Drawing.Point(24, 302);
            this.buttonDisconnectAll.Name = "buttonDisconnectAll";
            this.buttonDisconnectAll.Size = new System.Drawing.Size(90, 30);
            this.buttonDisconnectAll.TabIndex = 6;
            this.buttonDisconnectAll.Text = "Disconnect all";
            this.buttonDisconnectAll.UseVisualStyleBackColor = true;
            this.buttonDisconnectAll.Click += new System.EventHandler(this.buttonDisconnectAll_Click);
            // 
            // buttonUpdateCameraList
            // 
            this.buttonUpdateCameraList.Location = new System.Drawing.Point(132, 302);
            this.buttonUpdateCameraList.Name = "buttonUpdateCameraList";
            this.buttonUpdateCameraList.Size = new System.Drawing.Size(75, 34);
            this.buttonUpdateCameraList.TabIndex = 7;
            this.buttonUpdateCameraList.Text = "Update Camera List";
            this.buttonUpdateCameraList.UseVisualStyleBackColor = true;
            this.buttonUpdateCameraList.Click += new System.EventHandler(this.buttonUpdateCameraList_Click);
            // 
            // comboBoxTreadmillPort
            // 
            this.comboBoxTreadmillPort.FormattingEnabled = true;
            this.comboBoxTreadmillPort.Location = new System.Drawing.Point(132, 67);
            this.comboBoxTreadmillPort.Name = "comboBoxTreadmillPort";
            this.comboBoxTreadmillPort.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTreadmillPort.TabIndex = 8;
            // 
            // btnTreadmillControl
            // 
            this.btnTreadmillControl.Location = new System.Drawing.Point(326, 108);
            this.btnTreadmillControl.Name = "btnTreadmillControl";
            this.btnTreadmillControl.Size = new System.Drawing.Size(96, 45);
            this.btnTreadmillControl.TabIndex = 11;
            this.btnTreadmillControl.Text = "Start Treadmill";
            this.btnTreadmillControl.UseVisualStyleBackColor = true;
            this.btnTreadmillControl.Click += new System.EventHandler(this.btnTreadmillControl_Click);
            // 
            // btnConnectToTreadmill
            // 
            this.btnConnectToTreadmill.Location = new System.Drawing.Point(326, 159);
            this.btnConnectToTreadmill.Name = "btnConnectToTreadmill";
            this.btnConnectToTreadmill.Size = new System.Drawing.Size(96, 44);
            this.btnConnectToTreadmill.TabIndex = 12;
            this.btnConnectToTreadmill.Text = "Connect to Treadmill";
            this.btnConnectToTreadmill.UseVisualStyleBackColor = true;
            this.btnConnectToTreadmill.Click += new System.EventHandler(this.btnConnectToTreadmill_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(568, 374);
            this.Controls.Add(this.btnConnectToTreadmill);
            this.Controls.Add(this.btnTreadmillControl);
            this.Controls.Add(this.comboBoxTreadmillPort);
            this.Controls.Add(this.buttonUpdateCameraList);
            this.Controls.Add(this.buttonDisconnectAll);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCameraControl);
            this.Controls.Add(this.textBoxCalibrationFilePath);
            this.Controls.Add(this.buttonOpenCalibrationFile);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StromoLight Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenCalibrationFile;
        private System.Windows.Forms.TextBox textBoxCalibrationFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialogCalibrationFile;
        private System.Windows.Forms.Button buttonCameraControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerPollCameraInterface;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Button buttonDisconnectAll;
        private System.Windows.Forms.Button buttonUpdateCameraList;
        private System.Windows.Forms.ComboBox comboBoxTreadmillPort;
        private System.Windows.Forms.Button btnTreadmillControl;
        private System.Windows.Forms.Button btnConnectToTreadmill;
    }
}

