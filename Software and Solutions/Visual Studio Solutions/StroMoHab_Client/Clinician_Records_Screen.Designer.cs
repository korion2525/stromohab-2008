namespace StroMoHab_Client
{
    partial class Clinician_Records_Screen
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
            this.stroMoHab_Panel = new CustomControls.StroMoHab_Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listViewClinicians = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.labelHelpInfo = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxActions.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // stroMoHab_Panel
            // 
            this.stroMoHab_Panel.AutoSize = true;
            this.stroMoHab_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stroMoHab_Panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stroMoHab_Panel.Location = new System.Drawing.Point(0, 0);
            this.stroMoHab_Panel.Name = "stroMoHab_Panel";
            this.stroMoHab_Panel.Size = new System.Drawing.Size(1024, 730);
            this.stroMoHab_Panel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(388, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 35);
            this.label3.TabIndex = 29;
            this.label3.Text = "Clinicians";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.Controls.Add(this.flowLayoutPanel);
            this.groupBoxActions.Location = new System.Drawing.Point(18, 48);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(989, 86);
            this.groupBoxActions.TabIndex = 38;
            this.groupBoxActions.TabStop = false;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.Controls.Add(this.buttonLogout);
            this.flowLayoutPanel.Controls.Add(this.buttonEdit);
            this.flowLayoutPanel.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel.Location = new System.Drawing.Point(2, 10);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(983, 70);
            this.flowLayoutPanel.TabIndex = 0;
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
            // buttonEdit
            // 
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonEdit.Location = new System.Drawing.Point(129, 5);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 60);
            this.buttonEdit.TabIndex = 38;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonDelete.Location = new System.Drawing.Point(256, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 60);
            this.buttonDelete.TabIndex = 36;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listViewClinicians
            // 
            this.listViewClinicians.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewClinicians.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewClinicians.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewClinicians.FullRowSelect = true;
            this.listViewClinicians.GridLines = true;
            this.listViewClinicians.HideSelection = false;
            this.listViewClinicians.Location = new System.Drawing.Point(21, 150);
            this.listViewClinicians.MultiSelect = false;
            this.listViewClinicians.Name = "listViewClinicians";
            this.listViewClinicians.ShowItemToolTips = true;
            this.listViewClinicians.Size = new System.Drawing.Size(983, 470);
            this.listViewClinicians.TabIndex = 39;
            this.listViewClinicians.UseCompatibleStateImageBehavior = false;
            this.listViewClinicians.View = System.Windows.Forms.View.Details;
            this.listViewClinicians.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewClinicians_MouseDoubleClick);
            this.listViewClinicians.SelectedIndexChanged += new System.EventHandler(this.listViewClinicians_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "User Name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Edit Clinicians";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Edit Patients";
            this.columnHeader5.Width = 103;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Edit Sessions";
            this.columnHeader6.Width = 109;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Edit Tasks";
            this.columnHeader7.Width = 87;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Schedule Sessions";
            this.columnHeader8.Width = 149;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Run Sessions";
            this.columnHeader9.Width = 112;
            // 
            // labelHelpInfo
            // 
            this.labelHelpInfo.AutoSize = true;
            this.labelHelpInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpInfo.Location = new System.Drawing.Point(19, 35);
            this.labelHelpInfo.Name = "labelHelpInfo";
            this.labelHelpInfo.Size = new System.Drawing.Size(188, 13);
            this.labelHelpInfo.TabIndex = 40;
            this.labelHelpInfo.Text = "Manage clinicians and their permisions";
            // 
            // Clinician_Records_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelHelpInfo);
            this.Controls.Add(this.listViewClinicians);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stroMoHab_Panel);
            this.Name = "Clinician_Records_Screen";
            this.Size = new System.Drawing.Size(1024, 730);
            this.groupBoxActions.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ListViewGroup listViewGroup_New = new System.Windows.Forms.ListViewGroup("", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup listViewGroup_Special = new System.Windows.Forms.ListViewGroup("Special Accounts", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup listViewGroup_Clinicians = new System.Windows.Forms.ListViewGroup("Clinicians", System.Windows.Forms.HorizontalAlignment.Left);
                
        private CustomControls.StroMoHab_Panel stroMoHab_Panel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListView listViewClinicians;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label labelHelpInfo;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
