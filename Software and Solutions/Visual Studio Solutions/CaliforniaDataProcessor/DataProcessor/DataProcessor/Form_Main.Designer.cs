namespace DataProcessor
{
    partial class Form_Main
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
            this.comboBoxSubjectToProcess = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFileToProcess = new System.Windows.Forms.Label();
            this.comboBoxTestToProcess = new System.Windows.Forms.ComboBox();
            this.buttonUpdateTopGraph = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.buttonUpdateBottomGraph = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLeftFootMeanAbsoluteAccuracy = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelRightFootMeanAbsoluteAccuracy = new System.Windows.Forms.Label();
            this.checkBoxUsePostProcessedData = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelMeanAbsoluteValueDifference = new System.Windows.Forms.Label();
            this.labelRightFootStandardDeviation = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelLeftFootStandardDeviation = new System.Windows.Forms.Label();
            this.labelDualStandardDeviation = new System.Windows.Forms.Label();
            this.labelRightFootSkewness = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelLeftFootSkewness = new System.Windows.Forms.Label();
            this.labelRightFootVariance = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelLeftFootVariance = new System.Windows.Forms.Label();
            this.buttonProcessAllData = new System.Windows.Forms.Button();
            this.checkBoxDrawHistogram = new System.Windows.Forms.CheckBox();
            this.buttonOutputSpreadsheet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxSubjectToProcess
            // 
            this.comboBoxSubjectToProcess.FormattingEnabled = true;
            this.comboBoxSubjectToProcess.Location = new System.Drawing.Point(5, 29);
            this.comboBoxSubjectToProcess.Name = "comboBoxSubjectToProcess";
            this.comboBoxSubjectToProcess.Size = new System.Drawing.Size(356, 21);
            this.comboBoxSubjectToProcess.TabIndex = 0;
            this.comboBoxSubjectToProcess.DropDown += new System.EventHandler(this.comboBoxSubjectToProcess_DropDown);
            this.comboBoxSubjectToProcess.TextChanged += new System.EventHandler(this.comboBoxSubjectToProcess_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Subject To Process:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Test To Process:";
            // 
            // labelFileToProcess
            // 
            this.labelFileToProcess.AutoSize = true;
            this.labelFileToProcess.Location = new System.Drawing.Point(94, 69);
            this.labelFileToProcess.Name = "labelFileToProcess";
            this.labelFileToProcess.Size = new System.Drawing.Size(0, 13);
            this.labelFileToProcess.TabIndex = 3;
            // 
            // comboBoxTestToProcess
            // 
            this.comboBoxTestToProcess.FormattingEnabled = true;
            this.comboBoxTestToProcess.Location = new System.Drawing.Point(5, 85);
            this.comboBoxTestToProcess.Name = "comboBoxTestToProcess";
            this.comboBoxTestToProcess.Size = new System.Drawing.Size(356, 21);
            this.comboBoxTestToProcess.TabIndex = 4;
            // 
            // buttonUpdateTopGraph
            // 
            this.buttonUpdateTopGraph.Location = new System.Drawing.Point(12, 112);
            this.buttonUpdateTopGraph.Name = "buttonUpdateTopGraph";
            this.buttonUpdateTopGraph.Size = new System.Drawing.Size(103, 53);
            this.buttonUpdateTopGraph.TabIndex = 5;
            this.buttonUpdateTopGraph.Text = "Update Top Graph";
            this.buttonUpdateTopGraph.UseVisualStyleBackColor = true;
            this.buttonUpdateTopGraph.Click += new System.EventHandler(this.buttonUpdateTopGraph_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.AutoScroll = true;
            this.zedGraphControl1.IsAntiAlias = true;
            this.zedGraphControl1.IsAutoScrollRange = true;
            this.zedGraphControl1.IsEnableSelection = true;
            this.zedGraphControl1.IsShowCursorValues = true;
            this.zedGraphControl1.IsShowHScrollBar = true;
            this.zedGraphControl1.IsShowPointValues = true;
            this.zedGraphControl1.IsShowVScrollBar = true;
            this.zedGraphControl1.IsSynchronizeXAxes = true;
            this.zedGraphControl1.IsSynchronizeYAxes = true;
            this.zedGraphControl1.IsZoomOnMouseCenter = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(483, 11);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(1042, 371);
            this.zedGraphControl1.TabIndex = 6;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.AutoScroll = true;
            this.zedGraphControl2.IsAntiAlias = true;
            this.zedGraphControl2.IsAutoScrollRange = true;
            this.zedGraphControl2.IsEnableSelection = true;
            this.zedGraphControl2.IsShowCursorValues = true;
            this.zedGraphControl2.IsShowHScrollBar = true;
            this.zedGraphControl2.IsShowPointValues = true;
            this.zedGraphControl2.IsShowVScrollBar = true;
            this.zedGraphControl2.IsSynchronizeXAxes = true;
            this.zedGraphControl2.IsSynchronizeYAxes = true;
            this.zedGraphControl2.IsZoomOnMouseCenter = true;
            this.zedGraphControl2.Location = new System.Drawing.Point(483, 401);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0;
            this.zedGraphControl2.ScrollMaxX = 0;
            this.zedGraphControl2.ScrollMaxY = 0;
            this.zedGraphControl2.ScrollMaxY2 = 0;
            this.zedGraphControl2.ScrollMinX = 0;
            this.zedGraphControl2.ScrollMinY = 0;
            this.zedGraphControl2.ScrollMinY2 = 0;
            this.zedGraphControl2.Size = new System.Drawing.Size(1042, 371);
            this.zedGraphControl2.TabIndex = 7;
            // 
            // buttonUpdateBottomGraph
            // 
            this.buttonUpdateBottomGraph.Location = new System.Drawing.Point(147, 112);
            this.buttonUpdateBottomGraph.Name = "buttonUpdateBottomGraph";
            this.buttonUpdateBottomGraph.Size = new System.Drawing.Size(103, 53);
            this.buttonUpdateBottomGraph.TabIndex = 8;
            this.buttonUpdateBottomGraph.Text = "Update Bottom Graph";
            this.buttonUpdateBottomGraph.UseVisualStyleBackColor = true;
            this.buttonUpdateBottomGraph.Click += new System.EventHandler(this.buttonUpdateBottomGraph_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(160, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Left Foot:";
            // 
            // labelLeftFootMeanAbsoluteAccuracy
            // 
            this.labelLeftFootMeanAbsoluteAccuracy.AutoSize = true;
            this.labelLeftFootMeanAbsoluteAccuracy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelLeftFootMeanAbsoluteAccuracy.Location = new System.Drawing.Point(177, 251);
            this.labelLeftFootMeanAbsoluteAccuracy.Name = "labelLeftFootMeanAbsoluteAccuracy";
            this.labelLeftFootMeanAbsoluteAccuracy.Size = new System.Drawing.Size(13, 17);
            this.labelLeftFootMeanAbsoluteAccuracy.TabIndex = 10;
            this.labelLeftFootMeanAbsoluteAccuracy.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.Location = new System.Drawing.Point(12, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mean Absolute Accuracy:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(300, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Right Foot:";
            // 
            // labelRightFootMeanAbsoluteAccuracy
            // 
            this.labelRightFootMeanAbsoluteAccuracy.AutoSize = true;
            this.labelRightFootMeanAbsoluteAccuracy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelRightFootMeanAbsoluteAccuracy.Location = new System.Drawing.Point(320, 251);
            this.labelRightFootMeanAbsoluteAccuracy.Name = "labelRightFootMeanAbsoluteAccuracy";
            this.labelRightFootMeanAbsoluteAccuracy.Size = new System.Drawing.Size(13, 17);
            this.labelRightFootMeanAbsoluteAccuracy.TabIndex = 13;
            this.labelRightFootMeanAbsoluteAccuracy.Text = "-";
            // 
            // checkBoxUsePostProcessedData
            // 
            this.checkBoxUsePostProcessedData.AutoSize = true;
            this.checkBoxUsePostProcessedData.Checked = true;
            this.checkBoxUsePostProcessedData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUsePostProcessedData.Location = new System.Drawing.Point(121, 62);
            this.checkBoxUsePostProcessedData.Name = "checkBoxUsePostProcessedData";
            this.checkBoxUsePostProcessedData.Size = new System.Drawing.Size(162, 17);
            this.checkBoxUsePostProcessedData.TabIndex = 14;
            this.checkBoxUsePostProcessedData.Text = "Analyse post-processed data";
            this.checkBoxUsePostProcessedData.UseVisualStyleBackColor = true;
            this.checkBoxUsePostProcessedData.CheckedChanged += new System.EventHandler(this.checkBoxUsePostProcessedData_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label7.Location = new System.Drawing.Point(12, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(198, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Mean Absolute Value Difference:";
            // 
            // labelMeanAbsoluteValueDifference
            // 
            this.labelMeanAbsoluteValueDifference.AutoSize = true;
            this.labelMeanAbsoluteValueDifference.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelMeanAbsoluteValueDifference.Location = new System.Drawing.Point(242, 282);
            this.labelMeanAbsoluteValueDifference.Name = "labelMeanAbsoluteValueDifference";
            this.labelMeanAbsoluteValueDifference.Size = new System.Drawing.Size(13, 17);
            this.labelMeanAbsoluteValueDifference.TabIndex = 15;
            this.labelMeanAbsoluteValueDifference.Text = "-";
            // 
            // labelRightFootStandardDeviation
            // 
            this.labelRightFootStandardDeviation.AutoSize = true;
            this.labelRightFootStandardDeviation.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelRightFootStandardDeviation.Location = new System.Drawing.Point(320, 317);
            this.labelRightFootStandardDeviation.Name = "labelRightFootStandardDeviation";
            this.labelRightFootStandardDeviation.Size = new System.Drawing.Size(13, 17);
            this.labelRightFootStandardDeviation.TabIndex = 19;
            this.labelRightFootStandardDeviation.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label8.Location = new System.Drawing.Point(12, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Standard Deviation:";
            // 
            // labelLeftFootStandardDeviation
            // 
            this.labelLeftFootStandardDeviation.AutoSize = true;
            this.labelLeftFootStandardDeviation.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelLeftFootStandardDeviation.Location = new System.Drawing.Point(177, 317);
            this.labelLeftFootStandardDeviation.Name = "labelLeftFootStandardDeviation";
            this.labelLeftFootStandardDeviation.Size = new System.Drawing.Size(13, 17);
            this.labelLeftFootStandardDeviation.TabIndex = 17;
            this.labelLeftFootStandardDeviation.Text = "-";
            // 
            // labelDualStandardDeviation
            // 
            this.labelDualStandardDeviation.AutoSize = true;
            this.labelDualStandardDeviation.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelDualStandardDeviation.Location = new System.Drawing.Point(242, 336);
            this.labelDualStandardDeviation.Name = "labelDualStandardDeviation";
            this.labelDualStandardDeviation.Size = new System.Drawing.Size(13, 17);
            this.labelDualStandardDeviation.TabIndex = 20;
            this.labelDualStandardDeviation.Text = "-";
            // 
            // labelRightFootSkewness
            // 
            this.labelRightFootSkewness.AutoSize = true;
            this.labelRightFootSkewness.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelRightFootSkewness.Location = new System.Drawing.Point(320, 365);
            this.labelRightFootSkewness.Name = "labelRightFootSkewness";
            this.labelRightFootSkewness.Size = new System.Drawing.Size(13, 17);
            this.labelRightFootSkewness.TabIndex = 23;
            this.labelRightFootSkewness.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label9.Location = new System.Drawing.Point(12, 365);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Skewness:";
            // 
            // labelLeftFootSkewness
            // 
            this.labelLeftFootSkewness.AutoSize = true;
            this.labelLeftFootSkewness.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelLeftFootSkewness.Location = new System.Drawing.Point(177, 365);
            this.labelLeftFootSkewness.Name = "labelLeftFootSkewness";
            this.labelLeftFootSkewness.Size = new System.Drawing.Size(13, 17);
            this.labelLeftFootSkewness.TabIndex = 21;
            this.labelLeftFootSkewness.Text = "-";
            // 
            // labelRightFootVariance
            // 
            this.labelRightFootVariance.AutoSize = true;
            this.labelRightFootVariance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelRightFootVariance.Location = new System.Drawing.Point(320, 401);
            this.labelRightFootVariance.Name = "labelRightFootVariance";
            this.labelRightFootVariance.Size = new System.Drawing.Size(13, 17);
            this.labelRightFootVariance.TabIndex = 26;
            this.labelRightFootVariance.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label10.Location = new System.Drawing.Point(12, 401);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Variance:";
            // 
            // labelLeftFootVariance
            // 
            this.labelLeftFootVariance.AutoSize = true;
            this.labelLeftFootVariance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelLeftFootVariance.Location = new System.Drawing.Point(177, 401);
            this.labelLeftFootVariance.Name = "labelLeftFootVariance";
            this.labelLeftFootVariance.Size = new System.Drawing.Size(13, 17);
            this.labelLeftFootVariance.TabIndex = 24;
            this.labelLeftFootVariance.Text = "-";
            // 
            // buttonProcessAllData
            // 
            this.buttonProcessAllData.Location = new System.Drawing.Point(323, 128);
            this.buttonProcessAllData.Name = "buttonProcessAllData";
            this.buttonProcessAllData.Size = new System.Drawing.Size(120, 37);
            this.buttonProcessAllData.TabIndex = 27;
            this.buttonProcessAllData.Text = "Process All Data";
            this.buttonProcessAllData.UseVisualStyleBackColor = true;
            this.buttonProcessAllData.Click += new System.EventHandler(this.buttonProcessAllData_Click);
            // 
            // checkBoxDrawHistogram
            // 
            this.checkBoxDrawHistogram.AutoSize = true;
            this.checkBoxDrawHistogram.Location = new System.Drawing.Point(163, 171);
            this.checkBoxDrawHistogram.Name = "checkBoxDrawHistogram";
            this.checkBoxDrawHistogram.Size = new System.Drawing.Size(73, 17);
            this.checkBoxDrawHistogram.TabIndex = 28;
            this.checkBoxDrawHistogram.Text = "Histogram";
            this.checkBoxDrawHistogram.UseVisualStyleBackColor = true;
            // 
            // buttonOutputSpreadsheet
            // 
            this.buttonOutputSpreadsheet.Location = new System.Drawing.Point(81, 474);
            this.buttonOutputSpreadsheet.Name = "buttonOutputSpreadsheet";
            this.buttonOutputSpreadsheet.Size = new System.Drawing.Size(280, 101);
            this.buttonOutputSpreadsheet.TabIndex = 29;
            this.buttonOutputSpreadsheet.Text = "Output Spreadsheet";
            this.buttonOutputSpreadsheet.UseVisualStyleBackColor = true;
            this.buttonOutputSpreadsheet.Click += new System.EventHandler(this.buttonOutputSpreadsheet_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 784);
            this.Controls.Add(this.buttonOutputSpreadsheet);
            this.Controls.Add(this.checkBoxDrawHistogram);
            this.Controls.Add(this.buttonProcessAllData);
            this.Controls.Add(this.labelRightFootVariance);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelLeftFootVariance);
            this.Controls.Add(this.labelRightFootSkewness);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelLeftFootSkewness);
            this.Controls.Add(this.labelDualStandardDeviation);
            this.Controls.Add(this.labelRightFootStandardDeviation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelLeftFootStandardDeviation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelMeanAbsoluteValueDifference);
            this.Controls.Add(this.checkBoxUsePostProcessedData);
            this.Controls.Add(this.labelRightFootMeanAbsoluteAccuracy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelLeftFootMeanAbsoluteAccuracy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonUpdateBottomGraph);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.buttonUpdateTopGraph);
            this.Controls.Add(this.comboBoxTestToProcess);
            this.Controls.Add(this.labelFileToProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSubjectToProcess);
            this.Name = "Form_Main";
            this.Text = "Data Processor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSubjectToProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFileToProcess;
        private System.Windows.Forms.ComboBox comboBoxTestToProcess;
        private System.Windows.Forms.Button buttonUpdateTopGraph;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.Button buttonUpdateBottomGraph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLeftFootMeanAbsoluteAccuracy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelRightFootMeanAbsoluteAccuracy;
        private System.Windows.Forms.CheckBox checkBoxUsePostProcessedData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelMeanAbsoluteValueDifference;
        private System.Windows.Forms.Label labelRightFootStandardDeviation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelLeftFootStandardDeviation;
        private System.Windows.Forms.Label labelDualStandardDeviation;
        private System.Windows.Forms.Label labelRightFootSkewness;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelLeftFootSkewness;
        private System.Windows.Forms.Label labelRightFootVariance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelLeftFootVariance;
        private System.Windows.Forms.Button buttonProcessAllData;
        private System.Windows.Forms.CheckBox checkBoxDrawHistogram;
        private System.Windows.Forms.Button buttonOutputSpreadsheet;
    }
}

