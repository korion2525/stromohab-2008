namespace StroMoHab_Remote_DataManager
{
    partial class TaskFileBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskFileBrowser));
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.buttonGetList = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textBoxNewTaskName = new System.Windows.Forms.TextBox();
            this.buttonNewTask = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelTaskPreview = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(166, 10);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.ScrollAlwaysVisible = true;
            this.fileListBox.Size = new System.Drawing.Size(170, 147);
            this.fileListBox.TabIndex = 0;
            this.fileListBox.SelectedIndexChanged += new System.EventHandler(this.fileListBox_SelectedIndexChanged);
            // 
            // buttonGetList
            // 
            this.buttonGetList.Location = new System.Drawing.Point(9, 10);
            this.buttonGetList.Name = "buttonGetList";
            this.buttonGetList.Size = new System.Drawing.Size(138, 23);
            this.buttonGetList.TabIndex = 1;
            this.buttonGetList.Text = "Refresh Avaliable Tasks";
            this.buttonGetList.UseVisualStyleBackColor = true;
            this.buttonGetList.Click += new System.EventHandler(this.buttonGetList_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(9, 39);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(138, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Load Selected Task";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // textBoxNewTaskName
            // 
            this.textBoxNewTaskName.Location = new System.Drawing.Point(166, 164);
            this.textBoxNewTaskName.Name = "textBoxNewTaskName";
            this.textBoxNewTaskName.Size = new System.Drawing.Size(170, 20);
            this.textBoxNewTaskName.TabIndex = 3;
            // 
            // buttonNewTask
            // 
            this.buttonNewTask.Location = new System.Drawing.Point(9, 161);
            this.buttonNewTask.Name = "buttonNewTask";
            this.buttonNewTask.Size = new System.Drawing.Size(138, 23);
            this.buttonNewTask.TabIndex = 4;
            this.buttonNewTask.Text = "Save Task As : ";
            this.buttonNewTask.UseVisualStyleBackColor = true;
            this.buttonNewTask.Click += new System.EventHandler(this.buttonNewTask_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 210);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(324, 40);
            this.textBoxDescription.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Description : ";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(9, 69);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(138, 23);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete Selected Task";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(355, 10);
            this.pictureBox.Name = "pictureBox1";
            this.pictureBox.Size = new System.Drawing.Size(180, 240);
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            // 
            // labelTaskPreview
            // 
            this.labelTaskPreview.AutoSize = true;
            this.labelTaskPreview.Location = new System.Drawing.Point(412, 123);
            this.labelTaskPreview.Name = "labelTaskPreview";
            this.labelTaskPreview.Size = new System.Drawing.Size(72, 13);
            this.labelTaskPreview.TabIndex = 9;
            this.labelTaskPreview.Text = "Task Preview";
            // 
            // TaskFileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 265);
            this.Controls.Add(this.labelTaskPreview);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.buttonNewTask);
            this.Controls.Add(this.textBoxNewTaskName);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonGetList);
            this.Controls.Add(this.fileListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskFileBrowser";
            this.Text = "StroMoHab Task File Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button buttonGetList;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBoxNewTaskName;
        private System.Windows.Forms.Button buttonNewTask;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelTaskPreview;

    }
}