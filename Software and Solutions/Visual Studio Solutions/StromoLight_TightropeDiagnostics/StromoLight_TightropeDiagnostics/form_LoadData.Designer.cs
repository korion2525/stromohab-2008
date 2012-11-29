namespace StromoLight_Diagnostics
{
    partial class Form_LoadData
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
            this.fbdLoadPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnLoadPath = new System.Windows.Forms.Button();
            this.lblLoadPath = new System.Windows.Forms.Label();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbPersonID = new System.Windows.Forms.ListBox();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnUnSwap = new System.Windows.Forms.Button();
            this.btnRemoveSubject = new System.Windows.Forms.Button();
            this.lbNotLoaded = new System.Windows.Forms.ListBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.nudFilterPoints = new System.Windows.Forms.NumericUpDown();
            this.lblWritingFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIgnoreSwaps = new System.Windows.Forms.TextBox();
            this.txtPersonDetails = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // fbdLoadPath
            // 
            this.fbdLoadPath.SelectedPath = "\\\\APPC05\\Users\\Public\\Documents\\Stromohab\\";
            // 
            // btnLoadPath
            // 
            this.btnLoadPath.Location = new System.Drawing.Point(12, 12);
            this.btnLoadPath.Name = "btnLoadPath";
            this.btnLoadPath.Size = new System.Drawing.Size(75, 23);
            this.btnLoadPath.TabIndex = 0;
            this.btnLoadPath.Text = "Load path";
            this.btnLoadPath.UseVisualStyleBackColor = true;
            this.btnLoadPath.Click += new System.EventHandler(this.btnLoadPath_Click);
            // 
            // lblLoadPath
            // 
            this.lblLoadPath.AutoSize = true;
            this.lblLoadPath.Location = new System.Drawing.Point(93, 17);
            this.lblLoadPath.Name = "lblLoadPath";
            this.lblLoadPath.Size = new System.Drawing.Size(243, 13);
            this.lblLoadPath.TabIndex = 1;
            this.lblLoadPath.Text = "\\\\APPC05\\Users\\Public\\Documents\\Stromohab\\";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(12, 69);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(121, 24);
            this.btnLoadData.TabIndex = 2;
            this.btnLoadData.Text = "Load participant data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbPersonID
            // 
            this.lbPersonID.FormattingEnabled = true;
            this.lbPersonID.Location = new System.Drawing.Point(12, 99);
            this.lbPersonID.Name = "lbPersonID";
            this.lbPersonID.Size = new System.Drawing.Size(121, 95);
            this.lbPersonID.TabIndex = 4;
            this.lbPersonID.SelectedIndexChanged += new System.EventHandler(this.lbPersonID_SelectedIndexChanged);
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(173, 99);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(120, 95);
            this.lbFiles.TabIndex = 5;
            // 
            // btnUnSwap
            // 
            this.btnUnSwap.Location = new System.Drawing.Point(158, 302);
            this.btnUnSwap.Name = "btnUnSwap";
            this.btnUnSwap.Size = new System.Drawing.Size(113, 23);
            this.btnUnSwap.TabIndex = 6;
            this.btnUnSwap.Text = "Unswap markers";
            this.btnUnSwap.UseVisualStyleBackColor = true;
            this.btnUnSwap.Click += new System.EventHandler(this.btnUnSwap_Click);
            // 
            // btnRemoveSubject
            // 
            this.btnRemoveSubject.Location = new System.Drawing.Point(394, 19);
            this.btnRemoveSubject.Name = "btnRemoveSubject";
            this.btnRemoveSubject.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveSubject.TabIndex = 7;
            this.btnRemoveSubject.Text = "Remove participant";
            this.btnRemoveSubject.UseVisualStyleBackColor = true;
            // 
            // lbNotLoaded
            // 
            this.lbNotLoaded.FormattingEnabled = true;
            this.lbNotLoaded.Location = new System.Drawing.Point(337, 98);
            this.lbNotLoaded.Name = "lbNotLoaded";
            this.lbNotLoaded.Size = new System.Drawing.Size(214, 95);
            this.lbNotLoaded.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(337, 303);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // nudFilterPoints
            // 
            this.nudFilterPoints.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudFilterPoints.Location = new System.Drawing.Point(431, 304);
            this.nudFilterPoints.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.nudFilterPoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFilterPoints.Name = "nudFilterPoints";
            this.nudFilterPoints.Size = new System.Drawing.Size(38, 20);
            this.nudFilterPoints.TabIndex = 10;
            this.nudFilterPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudFilterPoints.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblWritingFile
            // 
            this.lblWritingFile.AutoSize = true;
            this.lblWritingFile.Location = new System.Drawing.Point(231, 225);
            this.lblWritingFile.Name = "lblWritingFile";
            this.lblWritingFile.Size = new System.Drawing.Size(62, 13);
            this.lblWritingFile.TabIndex = 11;
            this.lblWritingFile.Text = "Writing file: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ignore swaps at:";
            // 
            // txtIgnoreSwaps
            // 
            this.txtIgnoreSwaps.Location = new System.Drawing.Point(12, 225);
            this.txtIgnoreSwaps.Multiline = true;
            this.txtIgnoreSwaps.Name = "txtIgnoreSwaps";
            this.txtIgnoreSwaps.Size = new System.Drawing.Size(121, 72);
            this.txtIgnoreSwaps.TabIndex = 14;
            this.txtIgnoreSwaps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPersonDetails
            // 
            this.txtPersonDetails.Location = new System.Drawing.Point(173, 200);
            this.txtPersonDetails.Name = "txtPersonDetails";
            this.txtPersonDetails.Size = new System.Drawing.Size(120, 20);
            this.txtPersonDetails.TabIndex = 15;
            // 
            // Form_LoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 338);
            this.Controls.Add(this.txtPersonDetails);
            this.Controls.Add(this.txtIgnoreSwaps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWritingFile);
            this.Controls.Add(this.nudFilterPoints);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.lbNotLoaded);
            this.Controls.Add(this.btnRemoveSubject);
            this.Controls.Add(this.btnUnSwap);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.lbPersonID);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.lblLoadPath);
            this.Controls.Add(this.btnLoadPath);
            this.Name = "Form_LoadData";
            this.Text = "form_LoadData";
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdLoadPath;
        private System.Windows.Forms.Button btnLoadPath;
        private System.Windows.Forms.Label lblLoadPath;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbPersonID;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button btnUnSwap;
        private System.Windows.Forms.Button btnRemoveSubject;
        private System.Windows.Forms.ListBox lbNotLoaded;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.NumericUpDown nudFilterPoints;
        private System.Windows.Forms.Label lblWritingFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIgnoreSwaps;
        private System.Windows.Forms.TextBox txtPersonDetails;
    }
}