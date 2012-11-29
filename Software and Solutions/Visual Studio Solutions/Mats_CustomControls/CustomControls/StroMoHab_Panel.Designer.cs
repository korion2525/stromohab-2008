namespace CustomControls
{
    partial class StroMoHab_Panel
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
            this.panel_StroMoHab = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel_StroMoHab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_StroMoHab
            // 
            this.panel_StroMoHab.AutoSize = true;
            this.panel_StroMoHab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel_StroMoHab.Controls.Add(this.pictureBox);
            this.panel_StroMoHab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_StroMoHab.Location = new System.Drawing.Point(0, 0);
            this.panel_StroMoHab.Name = "panel_StroMoHab";
            this.panel_StroMoHab.Size = new System.Drawing.Size(181, 150);
            this.panel_StroMoHab.TabIndex = 3;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.Image = global::CustomControls.Properties.Resources.StromohabLogosmall;
            this.pictureBox.Location = new System.Drawing.Point(24, 46);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(154, 71);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // StroMoHab_Panel
            // 
            this.AutoSize = true;
            this.Controls.Add(this.panel_StroMoHab);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StroMoHab_Panel";
            this.Size = new System.Drawing.Size(181, 150);
            this.panel_StroMoHab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_StroMoHab;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}
