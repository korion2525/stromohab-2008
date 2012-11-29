namespace StromoLight_Visualiser.Forms
{
    partial class Form_Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Start));
            this.surfaceControl1 = new SdlDotNet.Windows.SurfaceControl();
            ((System.ComponentModel.ISupportInitialize)(this.surfaceControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // surfaceControl1
            // 
            this.surfaceControl1.AccessibleDescription = "SdlDotNet SurfaceControl";
            this.surfaceControl1.AccessibleName = "SurfaceControl";
            this.surfaceControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.surfaceControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.surfaceControl1.Image = ((System.Drawing.Image)(resources.GetObject("surfaceControl1.Image")));
            this.surfaceControl1.InitialImage = ((System.Drawing.Image)(resources.GetObject("surfaceControl1.InitialImage")));
            this.surfaceControl1.Location = new System.Drawing.Point(12, 12);
            this.surfaceControl1.Name = "surfaceControl1";
            this.surfaceControl1.Size = new System.Drawing.Size(680, 384);
            this.surfaceControl1.TabIndex = 0;
            this.surfaceControl1.TabStop = false;
            // 
            // Form_Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 524);
            this.Controls.Add(this.surfaceControl1);
            this.Name = "Form_Start";
            this.Text = "Form_Start";
            this.Load += new System.EventHandler(this.Form_Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.surfaceControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SdlDotNet.Windows.SurfaceControl surfaceControl1;
    }
}