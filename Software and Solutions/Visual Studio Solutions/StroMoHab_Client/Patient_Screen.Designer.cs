namespace StroMoHab_Client
{
    partial class Patient_Screen
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
            this.components = new System.ComponentModel.Container();
            this.listViewSessions = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBoxInfoActions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonRunSession = new System.Windows.Forms.Button();
            this.buttonReviewSession = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.panelMainView = new System.Windows.Forms.Panel();
            this.create_Edit_Session_Control = new StroMoHab_Client.Create_Edit_Session_Control();
            this.session_Details_Control = new StroMoHab_Client.Session_Details_Control();
            this.patient_Detials_Control = new StroMoHab_Client.Patient_Detials_Control();
            this.labelNoSessions = new System.Windows.Forms.Label();
            this.labelHelpInfo = new System.Windows.Forms.Label();
            this.stroMoHab_Panel = new CustomControls.StroMoHab_Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxInfoActions.SuspendLayout();
            this.flowLayoutPanelControls.SuspendLayout();
            this.panelMainView.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewSessions
            // 
            this.listViewSessions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewSessions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.listViewSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSessions.FullRowSelect = true;
            this.listViewSessions.GridLines = true;
            this.listViewSessions.HideSelection = false;
            this.listViewSessions.Location = new System.Drawing.Point(21, 150);
            this.listViewSessions.MultiSelect = false;
            this.listViewSessions.Name = "listViewSessions";
            this.listViewSessions.ShowItemToolTips = true;
            this.listViewSessions.Size = new System.Drawing.Size(500, 470);
            this.listViewSessions.TabIndex = 28;
            this.listViewSessions.UseCompatibleStateImageBehavior = false;
            this.listViewSessions.View = System.Windows.Forms.View.Details;
            this.listViewSessions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSessions_MouseDoubleClick);
            this.listViewSessions.SelectedIndexChanged += new System.EventHandler(this.listViewSessions_SelectedIndexChanged);
            this.listViewSessions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSessions_ColumnClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Date";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 56;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Duration";
            this.columnHeader2.Width = 73;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Task";
            this.columnHeader4.Width = 245;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(262, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(500, 35);
            this.labelTitle.TabIndex = 31;
            this.labelTitle.Text = "Patient";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxInfoActions
            // 
            this.groupBoxInfoActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfoActions.Controls.Add(this.flowLayoutPanelControls);
            this.groupBoxInfoActions.Location = new System.Drawing.Point(18, 48);
            this.groupBoxInfoActions.Name = "groupBoxInfoActions";
            this.groupBoxInfoActions.Size = new System.Drawing.Size(989, 86);
            this.groupBoxInfoActions.TabIndex = 34;
            this.groupBoxInfoActions.TabStop = false;
            // 
            // flowLayoutPanelControls
            // 
            this.flowLayoutPanelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelControls.Controls.Add(this.buttonBack);
            this.flowLayoutPanelControls.Controls.Add(this.buttonOpen);
            this.flowLayoutPanelControls.Controls.Add(this.buttonEdit);
            this.flowLayoutPanelControls.Controls.Add(this.buttonDelete);
            this.flowLayoutPanelControls.Controls.Add(this.buttonReport);
            this.flowLayoutPanelControls.Controls.Add(this.buttonRunSession);
            this.flowLayoutPanelControls.Controls.Add(this.buttonReviewSession);
            this.flowLayoutPanelControls.Controls.Add(this.buttonDuplicate);
            this.flowLayoutPanelControls.Location = new System.Drawing.Point(2, 10);
            this.flowLayoutPanelControls.Name = "flowLayoutPanelControls";
            this.flowLayoutPanelControls.Size = new System.Drawing.Size(981, 72);
            this.flowLayoutPanelControls.TabIndex = 36;
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.Location = new System.Drawing.Point(2, 5);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 60);
            this.buttonBack.TabIndex = 35;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(129, 5);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(120, 60);
            this.buttonOpen.TabIndex = 36;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.Location = new System.Drawing.Point(256, 5);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 60);
            this.buttonEdit.TabIndex = 39;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(383, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 60);
            this.buttonDelete.TabIndex = 38;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.Location = new System.Drawing.Point(520, 5);
            this.buttonReport.Margin = new System.Windows.Forms.Padding(12, 5, 5, 5);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(120, 60);
            this.buttonReport.TabIndex = 42;
            this.buttonReport.Text = "Report";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonRunSession
            // 
            this.buttonRunSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunSession.Location = new System.Drawing.Point(647, 5);
            this.buttonRunSession.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonRunSession.Name = "buttonRunSession";
            this.buttonRunSession.Size = new System.Drawing.Size(120, 60);
            this.buttonRunSession.TabIndex = 40;
            this.buttonRunSession.Text = "Run";
            this.buttonRunSession.UseVisualStyleBackColor = true;
            this.buttonRunSession.Click += new System.EventHandler(this.buttonRunSession_Click);
            // 
            // buttonReviewSession
            // 
            this.buttonReviewSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReviewSession.Location = new System.Drawing.Point(774, 5);
            this.buttonReviewSession.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonReviewSession.Name = "buttonReviewSession";
            this.buttonReviewSession.Size = new System.Drawing.Size(120, 60);
            this.buttonReviewSession.TabIndex = 41;
            this.buttonReviewSession.Text = "Review";
            this.buttonReviewSession.UseVisualStyleBackColor = true;
            this.buttonReviewSession.Click += new System.EventHandler(this.buttonReviewSession_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDuplicate.Location = new System.Drawing.Point(2, 75);
            this.buttonDuplicate.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(120, 60);
            this.buttonDuplicate.TabIndex = 43;
            this.buttonDuplicate.Text = "Duplicate";
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
            // 
            // panelMainView
            // 
            this.panelMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMainView.Controls.Add(this.create_Edit_Session_Control);
            this.panelMainView.Controls.Add(this.session_Details_Control);
            this.panelMainView.Controls.Add(this.patient_Detials_Control);
            this.panelMainView.Location = new System.Drawing.Point(533, 142);
            this.panelMainView.Name = "panelMainView";
            this.panelMainView.Size = new System.Drawing.Size(478, 545);
            this.panelMainView.TabIndex = 35;
            // 
            // create_Edit_Session_Control
            // 
            this.create_Edit_Session_Control.BackColor = System.Drawing.Color.White;
            this.create_Edit_Session_Control.CurrentClinician = null;
            this.create_Edit_Session_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.create_Edit_Session_Control.Location = new System.Drawing.Point(0, 0);
            this.create_Edit_Session_Control.Name = "create_Edit_Session_Control";
            this.create_Edit_Session_Control.ShowDetailedTaskPreview = false;
            this.create_Edit_Session_Control.Size = new System.Drawing.Size(478, 545);
            this.create_Edit_Session_Control.TabIndex = 8;
            this.create_Edit_Session_Control.Visible = false;
            // 
            // session_Details_Control
            // 
            this.session_Details_Control.BackColor = System.Drawing.Color.White;
            this.session_Details_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.session_Details_Control.Location = new System.Drawing.Point(0, 0);
            this.session_Details_Control.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.session_Details_Control.Name = "session_Details_Control";
            this.session_Details_Control.Session = null;
            this.session_Details_Control.Size = new System.Drawing.Size(478, 545);
            this.session_Details_Control.TabIndex = 7;
            this.session_Details_Control.Visible = false;
            // 
            // patient_Detials_Control
            // 
            this.patient_Detials_Control.BackColor = System.Drawing.Color.White;
            this.patient_Detials_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patient_Detials_Control.Location = new System.Drawing.Point(0, 0);
            this.patient_Detials_Control.Name = "patient_Detials_Control";
            this.patient_Detials_Control.Patient = null;
            this.patient_Detials_Control.ShowDetails = true;
            this.patient_Detials_Control.ShowTextBox = true;
            this.patient_Detials_Control.Size = new System.Drawing.Size(478, 545);
            this.patient_Detials_Control.TabIndex = 1;
            // 
            // labelNoSessions
            // 
            this.labelNoSessions.BackColor = System.Drawing.Color.Transparent;
            this.labelNoSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoSessions.Location = new System.Drawing.Point(198, 322);
            this.labelNoSessions.Name = "labelNoSessions";
            this.labelNoSessions.Size = new System.Drawing.Size(129, 62);
            this.labelNoSessions.TabIndex = 36;
            this.labelNoSessions.Text = "No Sessions On Record";
            this.labelNoSessions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNoSessions.Visible = false;
            // 
            // labelHelpInfo
            // 
            this.labelHelpInfo.AutoSize = true;
            this.labelHelpInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpInfo.Location = new System.Drawing.Point(19, 35);
            this.labelHelpInfo.Name = "labelHelpInfo";
            this.labelHelpInfo.Size = new System.Drawing.Size(331, 13);
            this.labelHelpInfo.TabIndex = 37;
            this.labelHelpInfo.Text = "Schedule and run new sessions or review previously completed ones";
            // 
            // stroMoHab_Panel
            // 
            this.stroMoHab_Panel.AutoSize = true;
            this.stroMoHab_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stroMoHab_Panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.stroMoHab_Panel.Location = new System.Drawing.Point(0, 0);
            this.stroMoHab_Panel.Name = "stroMoHab_Panel";
            this.stroMoHab_Panel.Size = new System.Drawing.Size(1024, 730);
            this.stroMoHab_Panel.TabIndex = 0;
            // 
            // Patient_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelHelpInfo);
            this.Controls.Add(this.labelNoSessions);
            this.Controls.Add(this.panelMainView);
            this.Controls.Add(this.groupBoxInfoActions);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.listViewSessions);
            this.Controls.Add(this.stroMoHab_Panel);
            this.Name = "Patient_Screen";
            this.Size = new System.Drawing.Size(1024, 730);
            this.groupBoxInfoActions.ResumeLayout(false);
            this.flowLayoutPanelControls.ResumeLayout(false);
            this.panelMainView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        System.Windows.Forms.ListViewGroup listViewGroup_New = new System.Windows.Forms.ListViewGroup("", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup listViewGroup_Scheduled = new System.Windows.Forms.ListViewGroup("Scheduled", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup listViewGroup_Completed = new System.Windows.Forms.ListViewGroup("Completed", System.Windows.Forms.HorizontalAlignment.Left);
        private CustomControls.StroMoHab_Panel stroMoHab_Panel;
        private System.Windows.Forms.ListView listViewSessions;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox groupBoxInfoActions;
        private System.Windows.Forms.Panel panelMainView;
        private Patient_Detials_Control patient_Detials_Control;
        private Session_Details_Control session_Details_Control;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControls;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonBack;
        private Create_Edit_Session_Control create_Edit_Session_Control;
        private System.Windows.Forms.Label labelNoSessions;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonRunSession;
        private System.Windows.Forms.Button buttonReviewSession;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Label labelHelpInfo;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
