namespace StroMoHab_Setup_Packages
{
    partial class GUI
    {

        //Message presented to the user
        string installMessage = "";


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ttButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.MSMQButton = new System.Windows.Forms.Button();
            this.groupBoxTT = new System.Windows.Forms.GroupBox();
            this.groupBoxMSMQ = new System.Windows.Forms.GroupBox();
            this.labelMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxTT.SuspendLayout();
            this.groupBoxMSMQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::StroMoHab_Setup_Packages.Properties.Resources.StromohabLogosmall;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(382, 109);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ttButton
            // 
            this.ttButton.Location = new System.Drawing.Point(24, 19);
            this.ttButton.Name = "ttButton";
            this.ttButton.Size = new System.Drawing.Size(303, 23);
            this.ttButton.TabIndex = 2;
            this.ttButton.Text = "Install";
            this.ttButton.UseVisualStyleBackColor = true;
            this.ttButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(119, 303);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MSMQButton
            // 
            this.MSMQButton.Location = new System.Drawing.Point(25, 19);
            this.MSMQButton.Name = "MSMQButton";
            this.MSMQButton.Size = new System.Drawing.Size(303, 23);
            this.MSMQButton.TabIndex = 6;
            this.MSMQButton.Text = "Install";
            this.MSMQButton.UseVisualStyleBackColor = true;
            this.MSMQButton.Click += new System.EventHandler(this.MSMQButton_Click);
            // 
            // groupBoxTT
            // 
            this.groupBoxTT.Controls.Add(this.ttButton);
            this.groupBoxTT.Location = new System.Drawing.Point(16, 181);
            this.groupBoxTT.Name = "groupBoxTT";
            this.groupBoxTT.Size = new System.Drawing.Size(352, 52);
            this.groupBoxTT.TabIndex = 9;
            this.groupBoxTT.TabStop = false;
            this.groupBoxTT.Text = "Tracking Tools";
            // 
            // groupBoxMSMQ
            // 
            this.groupBoxMSMQ.Controls.Add(this.MSMQButton);
            this.groupBoxMSMQ.Location = new System.Drawing.Point(15, 239);
            this.groupBoxMSMQ.Name = "groupBoxMSMQ";
            this.groupBoxMSMQ.Size = new System.Drawing.Size(352, 52);
            this.groupBoxMSMQ.TabIndex = 10;
            this.groupBoxMSMQ.TabStop = false;
            this.groupBoxMSMQ.Text = "Microsoft Message Queue Server";
            // 
            // labelMessage
            // 
            this.labelMessage.Location = new System.Drawing.Point(13, 116);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(355, 53);
            this.labelMessage.TabIndex = 11;
            this.labelMessage.Text = "label1";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 353);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.groupBoxMSMQ);
            this.Controls.Add(this.groupBoxTT);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(396, 391);
            this.MinimumSize = new System.Drawing.Size(396, 391);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StroMoHab Package Installer";
            this.Load += new System.EventHandler(this.GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxTT.ResumeLayout(false);
            this.groupBoxMSMQ.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ttButton;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button MSMQButton;
        private System.Windows.Forms.GroupBox groupBoxTT;
        private System.Windows.Forms.GroupBox groupBoxMSMQ;
        private System.Windows.Forms.Label labelMessage;
    }
}

