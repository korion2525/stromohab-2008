using System.Windows.Forms;
using System.ComponentModel;

namespace ZedGraphDiagnosticsControl
{
    partial class GraphDataDisplayControlForm : Form
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
            this.checkBoxAR = new System.Windows.Forms.CheckBox();
            this.checkBoxLR = new System.Windows.Forms.CheckBox();
            this.checkBoxMR = new System.Windows.Forms.CheckBox();
            this.checkBoxLL = new System.Windows.Forms.CheckBox();
            this.checkBoxML = new System.Windows.Forms.CheckBox();
            this.checkBoxAL = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxAR
            // 
            this.checkBoxAR.AutoSize = true;
            this.checkBoxAR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxAR.Location = new System.Drawing.Point(198, 82);
            this.checkBoxAR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAR.Name = "checkBoxAR";
            this.checkBoxAR.Size = new System.Drawing.Size(40, 24);
            this.checkBoxAR.TabIndex = 18;
            this.checkBoxAR.Text = "R";
            this.checkBoxAR.UseVisualStyleBackColor = true;
            // 
            // checkBoxLR
            // 
            this.checkBoxLR.AutoSize = true;
            this.checkBoxLR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxLR.Location = new System.Drawing.Point(204, 48);
            this.checkBoxLR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxLR.Name = "checkBoxLR";
            this.checkBoxLR.Size = new System.Drawing.Size(40, 24);
            this.checkBoxLR.TabIndex = 17;
            this.checkBoxLR.Text = "R";
            this.checkBoxLR.UseVisualStyleBackColor = true;
            // 
            // checkBoxMR
            // 
            this.checkBoxMR.AutoSize = true;
            this.checkBoxMR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxMR.Location = new System.Drawing.Point(204, 14);
            this.checkBoxMR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMR.Name = "checkBoxMR";
            this.checkBoxMR.Size = new System.Drawing.Size(40, 24);
            this.checkBoxMR.TabIndex = 16;
            this.checkBoxMR.Text = "R";
            this.checkBoxMR.UseVisualStyleBackColor = true;
            // 
            // checkBoxLL
            // 
            this.checkBoxLL.AutoSize = true;
            this.checkBoxLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxLL.Location = new System.Drawing.Point(138, 48);
            this.checkBoxLL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxLL.Name = "checkBoxLL";
            this.checkBoxLL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxLL.Size = new System.Drawing.Size(37, 24);
            this.checkBoxLL.TabIndex = 15;
            this.checkBoxLL.Text = "L";
            this.checkBoxLL.UseVisualStyleBackColor = true;
            // 
            // checkBoxML
            // 
            this.checkBoxML.AutoSize = true;
            this.checkBoxML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxML.Location = new System.Drawing.Point(138, 14);
            this.checkBoxML.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxML.Name = "checkBoxML";
            this.checkBoxML.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxML.Size = new System.Drawing.Size(37, 24);
            this.checkBoxML.TabIndex = 14;
            this.checkBoxML.Text = "L";
            this.checkBoxML.UseVisualStyleBackColor = true;
            // 
            // checkBoxAL
            // 
            this.checkBoxAL.AutoSize = true;
            this.checkBoxAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxAL.Location = new System.Drawing.Point(138, 82);
            this.checkBoxAL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAL.Name = "checkBoxAL";
            this.checkBoxAL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAL.Size = new System.Drawing.Size(37, 24);
            this.checkBoxAL.TabIndex = 13;
            this.checkBoxAL.Text = "L";
            this.checkBoxAL.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Mediolateral";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Longitudinal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Anteroposterior";
            // 
            // GraphDataDisplayControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 122);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxAR);
            this.Controls.Add(this.checkBoxLR);
            this.Controls.Add(this.checkBoxMR);
            this.Controls.Add(this.checkBoxLL);
            this.Controls.Add(this.checkBoxML);
            this.Controls.Add(this.checkBoxAL);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GraphDataDisplayControlForm";
            this.Text = "Graph Display";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAR;
        private System.Windows.Forms.CheckBox checkBoxLR;
        private System.Windows.Forms.CheckBox checkBoxMR;
        private System.Windows.Forms.CheckBox checkBoxLL;
        private System.Windows.Forms.CheckBox checkBoxML;
        private System.Windows.Forms.CheckBox checkBoxAL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}