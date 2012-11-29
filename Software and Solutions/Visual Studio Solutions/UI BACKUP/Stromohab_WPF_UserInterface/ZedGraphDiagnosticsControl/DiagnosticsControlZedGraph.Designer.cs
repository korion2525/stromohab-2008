namespace ZedGraphDiagnosticsControl
{
    partial class DiagnosticsControlZedGraph
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
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStepLength = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.labelAngle = new System.Windows.Forms.Label();
            this.labelSymmetry = new System.Windows.Forms.Label();
            this.labelRightFootY = new System.Windows.Forms.Label();
            this.labelRightFootZ = new System.Windows.Forms.Label();
            this.labelRightFootX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLeftFootZ = new System.Windows.Forms.Label();
            this.labelLeftFootY = new System.Windows.Forms.Label();
            this.labelLeftFootX = new System.Windows.Forms.Label();
            this.labelLeftLeg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxPauseSession = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 139);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(708, 356);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(387, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "Step Frequency";
            // 
            // labelStepLength
            // 
            this.labelStepLength.AutoSize = true;
            this.labelStepLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStepLength.Location = new System.Drawing.Point(387, 86);
            this.labelStepLength.Name = "labelStepLength";
            this.labelStepLength.Size = new System.Drawing.Size(128, 25);
            this.labelStepLength.TabIndex = 33;
            this.labelStepLength.Text = "Step Length";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(256, 88);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 29);
            this.radioButton3.TabIndex = 32;
            this.radioButton3.Text = "Hip";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(256, 58);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(80, 29);
            this.radioButton2.TabIndex = 31;
            this.radioButton2.Text = "Knee";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(256, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 29);
            this.radioButton1.TabIndex = 30;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Ankle";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Enabled = false;
            this.labelAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngle.Location = new System.Drawing.Point(387, 56);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(67, 25);
            this.labelAngle.TabIndex = 29;
            this.labelAngle.Text = "Angle";
            // 
            // labelSymmetry
            // 
            this.labelSymmetry.AutoSize = true;
            this.labelSymmetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSymmetry.Location = new System.Drawing.Point(387, 26);
            this.labelSymmetry.Name = "labelSymmetry";
            this.labelSymmetry.Size = new System.Drawing.Size(107, 25);
            this.labelSymmetry.TabIndex = 28;
            this.labelSymmetry.Text = "Symmetry";
            // 
            // labelRightFootY
            // 
            this.labelRightFootY.AutoSize = true;
            this.labelRightFootY.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRightFootY.Location = new System.Drawing.Point(142, 52);
            this.labelRightFootY.Name = "labelRightFootY";
            this.labelRightFootY.Size = new System.Drawing.Size(33, 25);
            this.labelRightFootY.TabIndex = 27;
            this.labelRightFootY.Text = "Y:";
            // 
            // labelRightFootZ
            // 
            this.labelRightFootZ.AutoSize = true;
            this.labelRightFootZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRightFootZ.Location = new System.Drawing.Point(142, 72);
            this.labelRightFootZ.Name = "labelRightFootZ";
            this.labelRightFootZ.Size = new System.Drawing.Size(31, 25);
            this.labelRightFootZ.TabIndex = 26;
            this.labelRightFootZ.Text = "Z:";
            // 
            // labelRightFootX
            // 
            this.labelRightFootX.AutoSize = true;
            this.labelRightFootX.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRightFootX.Location = new System.Drawing.Point(141, 32);
            this.labelRightFootX.Name = "labelRightFootX";
            this.labelRightFootX.Size = new System.Drawing.Size(32, 25);
            this.labelRightFootX.TabIndex = 25;
            this.labelRightFootX.Text = "X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(118, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 24);
            this.label5.TabIndex = 24;
            this.label5.Text = "Right Leg";
            // 
            // labelLeftFootZ
            // 
            this.labelLeftFootZ.AutoSize = true;
            this.labelLeftFootZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLeftFootZ.Location = new System.Drawing.Point(27, 74);
            this.labelLeftFootZ.Name = "labelLeftFootZ";
            this.labelLeftFootZ.Size = new System.Drawing.Size(31, 25);
            this.labelLeftFootZ.TabIndex = 22;
            this.labelLeftFootZ.Text = "Z:";
            // 
            // labelLeftFootY
            // 
            this.labelLeftFootY.AutoSize = true;
            this.labelLeftFootY.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLeftFootY.Location = new System.Drawing.Point(25, 52);
            this.labelLeftFootY.Name = "labelLeftFootY";
            this.labelLeftFootY.Size = new System.Drawing.Size(33, 25);
            this.labelLeftFootY.TabIndex = 23;
            this.labelLeftFootY.Text = "Y:";
            // 
            // labelLeftFootX
            // 
            this.labelLeftFootX.AutoSize = true;
            this.labelLeftFootX.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLeftFootX.Location = new System.Drawing.Point(25, 32);
            this.labelLeftFootX.Name = "labelLeftFootX";
            this.labelLeftFootX.Size = new System.Drawing.Size(32, 25);
            this.labelLeftFootX.TabIndex = 21;
            this.labelLeftFootX.Text = "X:";
            // 
            // labelLeftLeg
            // 
            this.labelLeftLeg.AutoSize = true;
            this.labelLeftLeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLeftLeg.Location = new System.Drawing.Point(3, 0);
            this.labelLeftLeg.Name = "labelLeftLeg";
            this.labelLeftLeg.Size = new System.Drawing.Size(84, 24);
            this.labelLeftLeg.TabIndex = 20;
            this.labelLeftLeg.Text = "Left Leg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(264, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 24);
            this.label3.TabIndex = 35;
            this.label3.Text = "Joint";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(400, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 24);
            this.label4.TabIndex = 36;
            this.label4.Text = "Measures";
            // 
            // checkBoxPauseSession
            // 
            this.checkBoxPauseSession.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxPauseSession.AutoSize = true;
            this.checkBoxPauseSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPauseSession.Location = new System.Drawing.Point(580, 3);
            this.checkBoxPauseSession.Name = "checkBoxPauseSession";
            this.checkBoxPauseSession.Size = new System.Drawing.Size(125, 30);
            this.checkBoxPauseSession.TabIndex = 37;
            this.checkBoxPauseSession.Text = "Pause Session";
            this.checkBoxPauseSession.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxPauseSession.UseVisualStyleBackColor = true;
            this.checkBoxPauseSession.CheckedChanged += new System.EventHandler(this.checkBoxPauseSession_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(580, 39);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 30);
            this.checkBox1.TabIndex = 38;
            this.checkBox1.Text = "End Session  ";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DiagnosticsControlZedGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBoxPauseSession);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelStepLength);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.labelAngle);
            this.Controls.Add(this.labelSymmetry);
            this.Controls.Add(this.labelRightFootY);
            this.Controls.Add(this.labelRightFootZ);
            this.Controls.Add(this.labelRightFootX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelLeftFootZ);
            this.Controls.Add(this.labelLeftFootY);
            this.Controls.Add(this.labelLeftFootX);
            this.Controls.Add(this.labelLeftLeg);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "DiagnosticsControlZedGraph";
            this.Size = new System.Drawing.Size(708, 492);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStepLength;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label labelAngle;
        private System.Windows.Forms.Label labelSymmetry;
        private System.Windows.Forms.Label labelRightFootY;
        private System.Windows.Forms.Label labelRightFootZ;
        private System.Windows.Forms.Label labelRightFootX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLeftFootZ;
        private System.Windows.Forms.Label labelLeftFootY;
        private System.Windows.Forms.Label labelLeftFootX;
        private System.Windows.Forms.Label labelLeftLeg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxPauseSession;
        private System.Windows.Forms.CheckBox checkBox1;


    }
}
