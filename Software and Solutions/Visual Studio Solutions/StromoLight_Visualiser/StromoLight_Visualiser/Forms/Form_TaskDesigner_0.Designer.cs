namespace StromoLight_Visualiser.Forms
{
    partial class Form_TaskDesigner_0
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TaskDesigner_0));
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.lblObjectToAdd = new System.Windows.Forms.Label();
            this.comboBxObjectToAdd = new System.Windows.Forms.ComboBox();
            this.groupBxCube = new System.Windows.Forms.GroupBox();
            this.numericUpDownImageNumber = new System.Windows.Forms.NumericUpDown();
            this.pictureBxTextureImage = new System.Windows.Forms.PictureBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.numericUpDownDistanceFromGround = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numericUpDownCubeSize = new System.Windows.Forms.NumericUpDown();
            this.txtBxLeftRightPosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarLeftRightPosition = new System.Windows.Forms.TrackBar();
            this.lblCubeSize = new System.Windows.Forms.Label();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.customNumericUpDownCurrentPosition = new StromoLight_Visualiser.CustomNumericUpDown();
            this.groupBxCube.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBxTextureImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCubeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLeftRightPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownCurrentPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrentPosition
            // 
            this.lblCurrentPosition.AutoSize = true;
            this.lblCurrentPosition.Location = new System.Drawing.Point(12, 39);
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(84, 13);
            this.lblCurrentPosition.TabIndex = 2;
            this.lblCurrentPosition.Text = "Current Position:";
            // 
            // lblObjectToAdd
            // 
            this.lblObjectToAdd.AutoSize = true;
            this.lblObjectToAdd.Location = new System.Drawing.Point(12, 76);
            this.lblObjectToAdd.Name = "lblObjectToAdd";
            this.lblObjectToAdd.Size = new System.Drawing.Size(79, 13);
            this.lblObjectToAdd.TabIndex = 3;
            this.lblObjectToAdd.Text = "Object To Add:";
            // 
            // comboBxObjectToAdd
            // 
            this.comboBxObjectToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBxObjectToAdd.FormattingEnabled = true;
            this.comboBxObjectToAdd.Items.AddRange(new object[] {
            "Cube",
            " "});
            this.comboBxObjectToAdd.Location = new System.Drawing.Point(151, 73);
            this.comboBxObjectToAdd.Name = "comboBxObjectToAdd";
            this.comboBxObjectToAdd.Size = new System.Drawing.Size(121, 21);
            this.comboBxObjectToAdd.TabIndex = 4;
            this.comboBxObjectToAdd.SelectedIndexChanged += new System.EventHandler(this.comboBxObjectToAdd_SelectedIndexChanged);
            // 
            // groupBxCube
            // 
            this.groupBxCube.BackColor = System.Drawing.SystemColors.Control;
            this.groupBxCube.Controls.Add(this.numericUpDownImageNumber);
            this.groupBxCube.Controls.Add(this.pictureBxTextureImage);
            this.groupBxCube.Controls.Add(this.lblImage);
            this.groupBxCube.Controls.Add(this.numericUpDownDistanceFromGround);
            this.groupBxCube.Controls.Add(this.lblHeight);
            this.groupBxCube.Controls.Add(this.numericUpDownCubeSize);
            this.groupBxCube.Controls.Add(this.txtBxLeftRightPosition);
            this.groupBxCube.Controls.Add(this.label1);
            this.groupBxCube.Controls.Add(this.trackBarLeftRightPosition);
            this.groupBxCube.Controls.Add(this.lblCubeSize);
            this.groupBxCube.Location = new System.Drawing.Point(12, 123);
            this.groupBxCube.Name = "groupBxCube";
            this.groupBxCube.Size = new System.Drawing.Size(281, 258);
            this.groupBxCube.TabIndex = 5;
            this.groupBxCube.TabStop = false;
            this.groupBxCube.Text = "Cube";
            this.groupBxCube.Visible = false;
            this.groupBxCube.VisibleChanged += new System.EventHandler(this.groupBxCube_VisibleChanged);
            // 
            // numericUpDownImageNumber
            // 
            this.numericUpDownImageNumber.Location = new System.Drawing.Point(54, 26);
            this.numericUpDownImageNumber.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownImageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageNumber.Name = "numericUpDownImageNumber";
            this.numericUpDownImageNumber.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownImageNumber.TabIndex = 9;
            this.numericUpDownImageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageNumber.ValueChanged += new System.EventHandler(this.numericUpDownImageNumber_ValueChanged);
            // 
            // pictureBxTextureImage
            // 
            this.pictureBxTextureImage.Location = new System.Drawing.Point(125, 19);
            this.pictureBxTextureImage.Name = "pictureBxTextureImage";
            this.pictureBxTextureImage.Size = new System.Drawing.Size(64, 64);
            this.pictureBxTextureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBxTextureImage.TabIndex = 8;
            this.pictureBxTextureImage.TabStop = false;
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(6, 28);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(42, 13);
            this.lblImage.TabIndex = 7;
            this.lblImage.Text = "Image: ";
            // 
            // numericUpDownDistanceFromGround
            // 
            this.numericUpDownDistanceFromGround.AutoSize = true;
            this.numericUpDownDistanceFromGround.DecimalPlaces = 1;
            this.numericUpDownDistanceFromGround.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDistanceFromGround.Location = new System.Drawing.Point(125, 120);
            this.numericUpDownDistanceFromGround.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDistanceFromGround.Name = "numericUpDownDistanceFromGround";
            this.numericUpDownDistanceFromGround.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownDistanceFromGround.TabIndex = 6;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(6, 122);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(113, 13);
            this.lblHeight.TabIndex = 5;
            this.lblHeight.Text = "Distance from Ground:";
            // 
            // numericUpDownCubeSize
            // 
            this.numericUpDownCubeSize.AutoSize = true;
            this.numericUpDownCubeSize.DecimalPlaces = 1;
            this.numericUpDownCubeSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCubeSize.Location = new System.Drawing.Point(125, 94);
            this.numericUpDownCubeSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCubeSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownCubeSize.Name = "numericUpDownCubeSize";
            this.numericUpDownCubeSize.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownCubeSize.TabIndex = 4;
            this.numericUpDownCubeSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            // 
            // txtBxLeftRightPosition
            // 
            this.txtBxLeftRightPosition.Location = new System.Drawing.Point(125, 146);
            this.txtBxLeftRightPosition.Name = "txtBxLeftRightPosition";
            this.txtBxLeftRightPosition.Size = new System.Drawing.Size(53, 20);
            this.txtBxLeftRightPosition.TabIndex = 3;
            this.txtBxLeftRightPosition.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Left/Right Position:";
            // 
            // trackBarLeftRightPosition
            // 
            this.trackBarLeftRightPosition.Location = new System.Drawing.Point(24, 172);
            this.trackBarLeftRightPosition.Maximum = 300;
            this.trackBarLeftRightPosition.Name = "trackBarLeftRightPosition";
            this.trackBarLeftRightPosition.Size = new System.Drawing.Size(251, 45);
            this.trackBarLeftRightPosition.TabIndex = 1;
            this.trackBarLeftRightPosition.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarLeftRightPosition.Value = 150;
            this.trackBarLeftRightPosition.ValueChanged += new System.EventHandler(this.trackBarLeftRightPosition_ValueChanged);
            // 
            // lblCubeSize
            // 
            this.lblCubeSize.AutoSize = true;
            this.lblCubeSize.Location = new System.Drawing.Point(89, 96);
            this.lblCubeSize.Name = "lblCubeSize";
            this.lblCubeSize.Size = new System.Drawing.Size(30, 13);
            this.lblCubeSize.TabIndex = 0;
            this.lblCubeSize.Text = "Size:";
            // 
            // btnAddObject
            // 
            this.btnAddObject.Location = new System.Drawing.Point(212, 398);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(75, 23);
            this.btnAddObject.TabIndex = 5;
            this.btnAddObject.Text = "Add Object";
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Face_simon_cowell-gal-villains.bmp");
            this.imageList1.Images.SetKeyName(1, "Face_cameron-diaz-picture-001.bmp");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Change Me";
            // 
            // customNumericUpDownCurrentPosition
            // 
            this.customNumericUpDownCurrentPosition.DecimalPlaces = 1;
            this.customNumericUpDownCurrentPosition.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.customNumericUpDownCurrentPosition.Location = new System.Drawing.Point(151, 39);
            this.customNumericUpDownCurrentPosition.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.customNumericUpDownCurrentPosition.Name = "customNumericUpDownCurrentPosition";
            this.customNumericUpDownCurrentPosition.Size = new System.Drawing.Size(50, 20);
            this.customNumericUpDownCurrentPosition.TabIndex = 6;
            this.customNumericUpDownCurrentPosition.ValueChanged += new System.EventHandler(this.customNumericUpDownCurrentPosition_ValueChanged);
            // 
            // Form_TaskDesigner_0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(303, 433);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.customNumericUpDownCurrentPosition);
            this.Controls.Add(this.btnAddObject);
            this.Controls.Add(this.groupBxCube);
            this.Controls.Add(this.comboBxObjectToAdd);
            this.Controls.Add(this.lblObjectToAdd);
            this.Controls.Add(this.lblCurrentPosition);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Form_TaskDesigner_0";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Task Designer";
            this.Shown += new System.EventHandler(this.Form_TaskDesigner_0_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_TaskDesigner_0_KeyDown);
            this.groupBxCube.ResumeLayout(false);
            this.groupBxCube.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBxTextureImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCubeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLeftRightPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownCurrentPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentPosition;
        private System.Windows.Forms.Label lblObjectToAdd;
        private System.Windows.Forms.ComboBox comboBxObjectToAdd;
        private System.Windows.Forms.GroupBox groupBxCube;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarLeftRightPosition;
        private System.Windows.Forms.Label lblCubeSize;
        private System.Windows.Forms.TextBox txtBxLeftRightPosition;
        private System.Windows.Forms.NumericUpDown numericUpDownCubeSize;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.NumericUpDown numericUpDownDistanceFromGround;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBxTextureImage;
        private System.Windows.Forms.NumericUpDown numericUpDownImageNumber;
        private CustomNumericUpDown customNumericUpDownCurrentPosition;
        private System.Windows.Forms.TextBox textBox1;
    }
}