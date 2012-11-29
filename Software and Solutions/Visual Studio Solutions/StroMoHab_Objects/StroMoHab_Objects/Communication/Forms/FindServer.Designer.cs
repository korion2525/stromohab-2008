namespace StroMoHab_Objects.Communication.Forms
{
    partial class FindServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindServer));
            this.buttonRetry = new System.Windows.Forms.Button();
            this.buttonEnterIP = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonIPOK = new System.Windows.Forms.Button();
            this.buttonIPCancel = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRetry
            // 
            this.buttonRetry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRetry.Location = new System.Drawing.Point(69, 62);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(194, 23);
            this.buttonRetry.TabIndex = 0;
            this.buttonRetry.Text = "Try to Automatically Find the Server Again";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // buttonEnterIP
            // 
            this.buttonEnterIP.Location = new System.Drawing.Point(69, 97);
            this.buttonEnterIP.Name = "buttonEnterIP";
            this.buttonEnterIP.Size = new System.Drawing.Size(194, 23);
            this.buttonEnterIP.TabIndex = 1;
            this.buttonEnterIP.Text = "Try to Find the Server Using it\'s IP";
            this.buttonEnterIP.UseVisualStyleBackColor = true;
            this.buttonEnterIP.Click += new System.EventHandler(this.buttonEnterIP_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(105, 131);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(122, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Failed to automatically find and connect to the server. Please check that the Str" +
                "oMoHab Server is running. Would you like to:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(105, 86);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(122, 20);
            this.textBoxIP.TabIndex = 3;
            // 
            // buttonIPOK
            // 
            this.buttonIPOK.Location = new System.Drawing.Point(79, 131);
            this.buttonIPOK.Name = "buttonIPOK";
            this.buttonIPOK.Size = new System.Drawing.Size(75, 23);
            this.buttonIPOK.TabIndex = 4;
            this.buttonIPOK.Text = "OK";
            this.buttonIPOK.UseVisualStyleBackColor = true;
            this.buttonIPOK.Click += new System.EventHandler(this.buttonIPOK_Click);
            // 
            // buttonIPCancel
            // 
            this.buttonIPCancel.Location = new System.Drawing.Point(178, 130);
            this.buttonIPCancel.Name = "buttonIPCancel";
            this.buttonIPCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonIPCancel.TabIndex = 5;
            this.buttonIPCancel.Text = "Cancel";
            this.buttonIPCancel.UseVisualStyleBackColor = true;
            this.buttonIPCancel.Click += new System.EventHandler(this.buttonIPCancel_Click);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(122, 112);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(89, 13);
            this.labelError.TabIndex = 6;
            this.labelError.Text = "Invalid IP Adress.";
            // 
            // FindServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 172);
            this.ControlBox = false;
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonIPCancel);
            this.Controls.Add(this.buttonIPOK);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEnterIP);
            this.Controls.Add(this.buttonRetry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "FindServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StroMoHab Connection Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Button buttonEnterIP;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonIPOK;
        private System.Windows.Forms.Button buttonIPCancel;
        private System.Windows.Forms.Label labelError;
    }
}