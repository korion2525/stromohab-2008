namespace StroMoHab_Client
{
    partial class Create_Edit_Clinician_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_Edit_Clinician_Form));
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.checkBoxRS = new System.Windows.Forms.CheckBox();
            this.checkBoxSS = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxT = new System.Windows.Forms.CheckBox();
            this.checkBoxP = new System.Windows.Forms.CheckBox();
            this.checkBoxS = new System.Windows.Forms.CheckBox();
            this.checkBoxC = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPasswordCheck = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.buttonCancle = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDetails.Controls.Add(this.checkBoxRS);
            this.groupBoxDetails.Controls.Add(this.checkBoxSS);
            this.groupBoxDetails.Controls.Add(this.label7);
            this.groupBoxDetails.Controls.Add(this.label4);
            this.groupBoxDetails.Controls.Add(this.checkBoxT);
            this.groupBoxDetails.Controls.Add(this.checkBoxP);
            this.groupBoxDetails.Controls.Add(this.checkBoxS);
            this.groupBoxDetails.Controls.Add(this.checkBoxC);
            this.groupBoxDetails.Controls.Add(this.label3);
            this.groupBoxDetails.Controls.Add(this.textBoxPasswordCheck);
            this.groupBoxDetails.Controls.Add(this.label1);
            this.groupBoxDetails.Controls.Add(this.label2);
            this.groupBoxDetails.Controls.Add(this.textBoxUserName);
            this.groupBoxDetails.Controls.Add(this.textBoxPassword);
            this.groupBoxDetails.Controls.Add(this.label5);
            this.groupBoxDetails.Controls.Add(this.label6);
            this.groupBoxDetails.Controls.Add(this.textBoxFirstName);
            this.groupBoxDetails.Controls.Add(this.textBoxLastName);
            this.groupBoxDetails.Location = new System.Drawing.Point(13, 13);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(527, 269);
            this.groupBoxDetails.TabIndex = 25;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Clinician Details";
            // 
            // checkBoxRS
            // 
            this.checkBoxRS.Checked = true;
            this.checkBoxRS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRS.Location = new System.Drawing.Point(204, 239);
            this.checkBoxRS.Name = "checkBoxRS";
            this.checkBoxRS.Size = new System.Drawing.Size(175, 24);
            this.checkBoxRS.TabIndex = 116;
            this.checkBoxRS.Text = "Run Sessions";
            this.checkBoxRS.UseVisualStyleBackColor = true;
            // 
            // checkBoxSS
            // 
            this.checkBoxSS.Checked = true;
            this.checkBoxSS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSS.Location = new System.Drawing.Point(23, 239);
            this.checkBoxSS.Name = "checkBoxSS";
            this.checkBoxSS.Size = new System.Drawing.Size(175, 24);
            this.checkBoxSS.TabIndex = 115;
            this.checkBoxSS.Text = "Schedule Sessions";
            this.checkBoxSS.UseVisualStyleBackColor = true;
            this.checkBoxSS.CheckedChanged += new System.EventHandler(this.checkBoxSS_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(245, 20);
            this.label7.TabIndex = 73;
            this.label7.Text = "Select what the Clinician can do...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 20);
            this.label4.TabIndex = 72;
            this.label4.Text = "Select what the Clinician can edit...";
            // 
            // checkBoxT
            // 
            this.checkBoxT.Checked = true;
            this.checkBoxT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxT.Location = new System.Drawing.Point(389, 180);
            this.checkBoxT.Name = "checkBoxT";
            this.checkBoxT.Size = new System.Drawing.Size(100, 24);
            this.checkBoxT.TabIndex = 114;
            this.checkBoxT.Text = "Tasks";
            this.checkBoxT.UseVisualStyleBackColor = true;
            // 
            // checkBoxP
            // 
            this.checkBoxP.Checked = true;
            this.checkBoxP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxP.Location = new System.Drawing.Point(145, 180);
            this.checkBoxP.Name = "checkBoxP";
            this.checkBoxP.Size = new System.Drawing.Size(100, 24);
            this.checkBoxP.TabIndex = 112;
            this.checkBoxP.Text = "Patients";
            this.checkBoxP.UseVisualStyleBackColor = true;
            // 
            // checkBoxS
            // 
            this.checkBoxS.Checked = true;
            this.checkBoxS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxS.Location = new System.Drawing.Point(267, 180);
            this.checkBoxS.Name = "checkBoxS";
            this.checkBoxS.Size = new System.Drawing.Size(100, 24);
            this.checkBoxS.TabIndex = 113;
            this.checkBoxS.Text = "Sessions";
            this.checkBoxS.UseVisualStyleBackColor = true;
            this.checkBoxS.CheckedChanged += new System.EventHandler(this.checkBoxS_CheckedChanged);
            // 
            // checkBoxC
            // 
            this.checkBoxC.Enabled = false;
            this.checkBoxC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxC.Location = new System.Drawing.Point(23, 180);
            this.checkBoxC.Name = "checkBoxC";
            this.checkBoxC.Size = new System.Drawing.Size(100, 24);
            this.checkBoxC.TabIndex = 111;
            this.checkBoxC.Text = "Clinicians";
            this.checkBoxC.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(241, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 67;
            this.label3.Text = "Retype Password :";
            // 
            // textBoxPasswordCheck
            // 
            this.textBoxPasswordCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPasswordCheck.Location = new System.Drawing.Point(384, 65);
            this.textBoxPasswordCheck.Name = "textBoxPasswordCheck";
            this.textBoxPasswordCheck.PasswordChar = '*';
            this.textBoxPasswordCheck.Size = new System.Drawing.Size(116, 26);
            this.textBoxPasswordCheck.TabIndex = 103;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 64;
            this.label1.Text = "Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "User Name :";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserName.Location = new System.Drawing.Point(109, 25);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(116, 26);
            this.textBoxUserName.TabIndex = 101;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(109, 65);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(116, 26);
            this.textBoxPassword.TabIndex = 102;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(243, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 60;
            this.label5.Text = "Last Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 20);
            this.label6.TabIndex = 61;
            this.label6.Text = "First Name :";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFirstName.Location = new System.Drawing.Point(108, 106);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(116, 26);
            this.textBoxFirstName.TabIndex = 104;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLastName.Location = new System.Drawing.Point(342, 106);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(116, 26);
            this.textBoxLastName.TabIndex = 105;
            // 
            // buttonCancle
            // 
            this.buttonCancle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancle.CausesValidation = false;
            this.buttonCancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonCancle.Location = new System.Drawing.Point(301, 288);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new System.Drawing.Size(142, 53);
            this.buttonCancle.TabIndex = 24;
            this.buttonCancle.Text = "Cancel";
            this.buttonCancle.UseVisualStyleBackColor = true;
            this.buttonCancle.Click += new System.EventHandler(this.buttonCancle_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonOK.Location = new System.Drawing.Point(109, 288);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(142, 53);
            this.buttonOK.TabIndex = 23;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // Create_Edit_Clinician_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 359);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(568, 397);
            this.MinimumSize = new System.Drawing.Size(568, 397);
            this.Name = "Create_Edit_Clinician_Form";
            this.Text = "New Clinician";
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Button buttonCancle;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxT;
        private System.Windows.Forms.CheckBox checkBoxP;
        private System.Windows.Forms.CheckBox checkBoxS;
        private System.Windows.Forms.CheckBox checkBoxC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPasswordCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox checkBoxRS;
        private System.Windows.Forms.CheckBox checkBoxSS;
        private System.Windows.Forms.Label label7;
    }
}