namespace StroMoHab_Client
{
    partial class Patient_Records_Screen
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.listViewPatients = new System.Windows.Forms.ListView();
            this.columnHeaderID = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTitle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLastName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderFirstName = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSearch = new System.Windows.Forms.Button();
            this.backgroundWorkerSearcher = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerLoader = new System.ComponentModel.BackgroundWorker();
            this.listViewPatientsDummy = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.panelPatientDetails = new System.Windows.Forms.Panel();
            this.patient_Detials_Control = new StroMoHab_Client.Patient_Detials_Control();
            this.stroMoHab_Panel = new CustomControls.StroMoHab_Panel();
            this.labelHelpInfo = new System.Windows.Forms.Label();
            this.groupBoxActions.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.panelPatientDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(34, 145);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(410, 26);
            this.textBoxSearch.TabIndex = 31;
            this.textBoxSearch.Text = "Search by Name, NHS Number, DOB, Postcode etc..";
            this.toolTip.SetToolTip(this.textBoxSearch, "Enter patient details to find the patient you want. E.g Name, NHS Number, DOB in " +
                    "the form dd/mm/yyyy");
            this.textBoxSearch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSearch_MouseClick);
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // listViewPatients
            // 
            this.listViewPatients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewPatients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderTitle,
            this.columnHeaderLastName,
            this.columnHeaderFirstName});
            this.listViewPatients.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPatients.FullRowSelect = true;
            this.listViewPatients.GridLines = true;
            this.listViewPatients.HideSelection = false;
            this.listViewPatients.Location = new System.Drawing.Point(21, 184);
            this.listViewPatients.Margin = new System.Windows.Forms.Padding(14);
            this.listViewPatients.Name = "listViewPatients";
            this.listViewPatients.ShowItemToolTips = true;
            this.listViewPatients.Size = new System.Drawing.Size(500, 436);
            this.listViewPatients.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewPatients.TabIndex = 30;
            this.listViewPatients.UseCompatibleStateImageBehavior = false;
            this.listViewPatients.View = System.Windows.Forms.View.Details;
            this.listViewPatients.SelectedIndexChanged += new System.EventHandler(this.listViewPatients_SelectedIndexChanged);
            this.listViewPatients.DoubleClick += new System.EventHandler(this.listViewPatients_DoubleClick);
            this.listViewPatients.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPatients_ColumnClick);
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "NHS Number";
            this.columnHeaderID.Width = 141;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 56;
            // 
            // columnHeaderLastName
            // 
            this.columnHeaderLastName.Text = "Last Name";
            this.columnHeaderLastName.Width = 140;
            // 
            // columnHeaderFirstName
            // 
            this.columnHeaderFirstName.Text = "First Name";
            this.columnHeaderFirstName.Width = 147;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(388, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 35);
            this.label3.TabIndex = 27;
            this.label3.Text = "Patients";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogout.Location = new System.Drawing.Point(2, 5);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(120, 60);
            this.buttonLogout.TabIndex = 26;
            this.buttonLogout.Text = "Back";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.Location = new System.Drawing.Point(453, 140);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(57, 36);
            this.buttonSearch.TabIndex = 32;
            this.buttonSearch.Text = "Go";
            this.toolTip.SetToolTip(this.buttonSearch, "Enter patient details to find the patient you want. E.g Name, NHS Number, DOB in " +
                    "the form dd/mm/yyyy");
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // backgroundWorkerSearcher
            // 
            this.backgroundWorkerSearcher.WorkerReportsProgress = true;
            this.backgroundWorkerSearcher.WorkerSupportsCancellation = true;
            // 
            // listViewPatientsDummy
            // 
            this.listViewPatientsDummy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewPatientsDummy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewPatientsDummy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPatientsDummy.FullRowSelect = true;
            this.listViewPatientsDummy.GridLines = true;
            this.listViewPatientsDummy.Location = new System.Drawing.Point(21, 184);
            this.listViewPatientsDummy.Margin = new System.Windows.Forms.Padding(14);
            this.listViewPatientsDummy.MultiSelect = false;
            this.listViewPatientsDummy.Name = "listViewPatientsDummy";
            this.listViewPatientsDummy.Size = new System.Drawing.Size(500, 430);
            this.listViewPatientsDummy.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewPatientsDummy.TabIndex = 33;
            this.listViewPatientsDummy.UseCompatibleStateImageBehavior = false;
            this.listViewPatientsDummy.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NHS Number";
            this.columnHeader1.Width = 141;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title";
            this.columnHeader2.Width = 56;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Name";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "First Name";
            this.columnHeader4.Width = 147;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonDelete.Location = new System.Drawing.Point(383, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 60);
            this.buttonDelete.TabIndex = 36;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonOpen.Location = new System.Drawing.Point(129, 5);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(120, 60);
            this.buttonOpen.TabIndex = 34;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.Controls.Add(this.flowLayoutPanel);
            this.groupBoxActions.Location = new System.Drawing.Point(18, 48);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(989, 86);
            this.groupBoxActions.TabIndex = 37;
            this.groupBoxActions.TabStop = false;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.Controls.Add(this.buttonLogout);
            this.flowLayoutPanel.Controls.Add(this.buttonOpen);
            this.flowLayoutPanel.Controls.Add(this.buttonEdit);
            this.flowLayoutPanel.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel.Location = new System.Drawing.Point(2, 10);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(983, 70);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonEdit.Location = new System.Drawing.Point(256, 5);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 60);
            this.buttonEdit.TabIndex = 38;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // panelPatientDetails
            // 
            this.panelPatientDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPatientDetails.Controls.Add(this.patient_Detials_Control);
            this.panelPatientDetails.Location = new System.Drawing.Point(533, 142);
            this.panelPatientDetails.Name = "panelPatientDetails";
            this.panelPatientDetails.Size = new System.Drawing.Size(478, 545);
            this.panelPatientDetails.TabIndex = 0;
            // 
            // patient_Detials_Control
            // 
            this.patient_Detials_Control.BackColor = System.Drawing.Color.White;
            this.patient_Detials_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patient_Detials_Control.Location = new System.Drawing.Point(0, 0);
            this.patient_Detials_Control.Name = "patient_Detials_Control";
            this.patient_Detials_Control.Patient = null;
            this.patient_Detials_Control.ShowDetails = false;
            this.patient_Detials_Control.ShowTextBox = false;
            this.patient_Detials_Control.Size = new System.Drawing.Size(478, 545);
            this.patient_Detials_Control.TabIndex = 0;
            // 
            // stroMoHab_Panel
            // 
            this.stroMoHab_Panel.AutoSize = true;
            this.stroMoHab_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stroMoHab_Panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stroMoHab_Panel.Location = new System.Drawing.Point(0, 0);
            this.stroMoHab_Panel.Name = "stroMoHab_Panel";
            this.stroMoHab_Panel.Size = new System.Drawing.Size(1024, 730);
            this.stroMoHab_Panel.TabIndex = 23;
            // 
            // labelHelpInfo
            // 
            this.labelHelpInfo.AutoSize = true;
            this.labelHelpInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpInfo.Location = new System.Drawing.Point(19, 35);
            this.labelHelpInfo.Name = "labelHelpInfo";
            this.labelHelpInfo.Size = new System.Drawing.Size(195, 13);
            this.labelHelpInfo.TabIndex = 38;
            this.labelHelpInfo.Text = "Find, manage, create and open patients";
            // 
            // Patient_Records_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelHelpInfo);
            this.Controls.Add(this.panelPatientDetails);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.listViewPatients);
            this.Controls.Add(this.listViewPatientsDummy);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stroMoHab_Panel);
            this.Name = "Patient_Records_Screen";
            this.Size = new System.Drawing.Size(1024, 730);
            this.groupBoxActions.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.panelPatientDetails.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ListViewGroup listViewGroup_New = new System.Windows.Forms.ListViewGroup("", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup listViewGroup_Patients = new System.Windows.Forms.ListViewGroup("Patients", System.Windows.Forms.HorizontalAlignment.Left);
        private CustomControls.StroMoHab_Panel stroMoHab_Panel;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ListView listViewPatients;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderTitle;
        private System.Windows.Forms.ColumnHeader columnHeaderLastName;
        private System.Windows.Forms.ColumnHeader columnHeaderFirstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.ToolTip toolTip;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSearcher;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoader;
        private System.Windows.Forms.ListView listViewPatientsDummy;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Panel panelPatientDetails;
        private Patient_Detials_Control patient_Detials_Control;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label labelHelpInfo;

    }
}
