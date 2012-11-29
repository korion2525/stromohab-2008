namespace StroMoHab_Client
{
    partial class Patient_Detials_Control
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
            this.groupBoxNotes = new System.Windows.Forms.GroupBox();
            this.richTextBoxNotes = new System.Windows.Forms.RichTextBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.labelLastSession = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelSessions = new System.Windows.Forms.Label();
            this.labelContact = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.labelTitleName = new System.Windows.Forms.Label();
            this.groupBoxNotes.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNotes
            // 
            this.groupBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNotes.Controls.Add(this.richTextBoxNotes);
            this.groupBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNotes.Location = new System.Drawing.Point(10, 384);
            this.groupBoxNotes.Name = "groupBoxNotes";
            this.groupBoxNotes.Size = new System.Drawing.Size(442, 148);
            this.groupBoxNotes.TabIndex = 13;
            this.groupBoxNotes.TabStop = false;
            this.groupBoxNotes.Text = "Patient Notes";
            // 
            // richTextBoxNotes
            // 
            this.richTextBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxNotes.Location = new System.Drawing.Point(6, 21);
            this.richTextBoxNotes.Name = "richTextBoxNotes";
            this.richTextBoxNotes.Size = new System.Drawing.Size(431, 121);
            this.richTextBoxNotes.TabIndex = 1;
            this.richTextBoxNotes.Text = "";
            this.richTextBoxNotes.Validating += new System.ComponentModel.CancelEventHandler(this.richTextBoxNotes_Validating);
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDetails.Controls.Add(this.labelLastSession);
            this.groupBoxDetails.Controls.Add(this.labelAge);
            this.groupBoxDetails.Controls.Add(this.labelSessions);
            this.groupBoxDetails.Controls.Add(this.labelContact);
            this.groupBoxDetails.Controls.Add(this.labelAddress);
            this.groupBoxDetails.Controls.Add(this.labelID);
            this.groupBoxDetails.Controls.Add(this.labelTitleName);
            this.groupBoxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDetails.Location = new System.Drawing.Point(10, 0);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(442, 384);
            this.groupBoxDetails.TabIndex = 14;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Patient Details";
            // 
            // labelLastSession
            // 
            this.labelLastSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastSession.Location = new System.Drawing.Point(6, 214);
            this.labelLastSession.Name = "labelLastSession";
            this.labelLastSession.Size = new System.Drawing.Size(430, 20);
            this.labelLastSession.TabIndex = 9;
            this.labelLastSession.Text = "label";
            // 
            // labelAge
            // 
            this.labelAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAge.Location = new System.Drawing.Point(6, 89);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(430, 20);
            this.labelAge.TabIndex = 8;
            this.labelAge.Text = "label";
            // 
            // labelSessions
            // 
            this.labelSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSessions.Location = new System.Drawing.Point(6, 184);
            this.labelSessions.Name = "labelSessions";
            this.labelSessions.Size = new System.Drawing.Size(430, 20);
            this.labelSessions.TabIndex = 7;
            this.labelSessions.Text = "label";
            // 
            // labelContact
            // 
            this.labelContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContact.Location = new System.Drawing.Point(6, 151);
            this.labelContact.Name = "labelContact";
            this.labelContact.Size = new System.Drawing.Size(430, 20);
            this.labelContact.TabIndex = 6;
            this.labelContact.Text = "label";
            // 
            // labelAddress
            // 
            this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddress.Location = new System.Drawing.Point(6, 121);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(430, 20);
            this.labelAddress.TabIndex = 5;
            this.labelAddress.Text = "label";
            // 
            // labelID
            // 
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(6, 58);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(430, 20);
            this.labelID.TabIndex = 4;
            this.labelID.Text = "label";
            // 
            // labelTitleName
            // 
            this.labelTitleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleName.Location = new System.Drawing.Point(6, 26);
            this.labelTitleName.Name = "labelTitleName";
            this.labelTitleName.Size = new System.Drawing.Size(430, 20);
            this.labelTitleName.TabIndex = 3;
            this.labelTitleName.Text = "label";
            // 
            // Patient_Detials_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.groupBoxNotes);
            this.Name = "Patient_Detials_Control";
            this.Size = new System.Drawing.Size(460, 535);
            this.groupBoxNotes.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNotes;
        private System.Windows.Forms.RichTextBox richTextBoxNotes;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label labelTitleName;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelSessions;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelLastSession;


    }
}
