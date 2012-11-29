namespace StromoLight_Visualiser
{
    partial class Form_DataDisplay
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
            this.lblAvatarX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAvatarX
            // 
            this.lblAvatarX.AutoSize = true;
            this.lblAvatarX.Location = new System.Drawing.Point(59, 49);
            this.lblAvatarX.Name = "lblAvatarX";
            this.lblAvatarX.Size = new System.Drawing.Size(14, 13);
            this.lblAvatarX.TabIndex = 0;
            this.lblAvatarX.Text = "X";
            // 
            // Form_DataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 116);
            this.ControlBox = false;
            this.Controls.Add(this.lblAvatarX);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_DataDisplay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StromoLight Visualiser Data Display";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAvatarX;
    }
}