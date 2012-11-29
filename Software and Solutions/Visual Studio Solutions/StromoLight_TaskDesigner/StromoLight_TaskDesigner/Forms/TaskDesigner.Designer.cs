namespace StroMoHab_Task_Designer.Forms
{
    partial class Form_TaskDesigner
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
            this.btnAddObject = new System.Windows.Forms.Button();
            this.groupBxCube = new System.Windows.Forms.GroupBox();
            this.numericUpDownObjectDepth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownObjectHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownImageNumber = new System.Windows.Forms.NumericUpDown();
            this.pictureBxTextureImage = new System.Windows.Forms.PictureBox();
            this.labelImage = new System.Windows.Forms.Label();
            this.numericUpDownDistanceFromGround = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numericUpDownObjectWidth = new System.Windows.Forms.NumericUpDown();
            this.txtBxLeftRightPosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarLeftRightPosition = new System.Windows.Forms.TrackBar();
            this.lblCubeSize = new System.Windows.Forms.Label();
            this.checkBoxUseTexture = new System.Windows.Forms.CheckBox();
            this.comboBxObjectToAdd = new System.Windows.Forms.ComboBox();
            this.lblObjectToAdd = new System.Windows.Forms.Label();
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.customNumericUpDownPlaceObjectAt = new CustomControls.CustomNumericUpDown();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.customNumericUpDownSelectObject = new CustomControls.CustomNumericUpDown();
            this.labelObjectNumber = new System.Windows.Forms.Label();
            this.checkBoxSelectObject = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDownCurrentPosition = new System.Windows.Forms.NumericUpDown();
            this.buttonRemoveObject = new System.Windows.Forms.Button();
            this.labelCurrentPosition = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.ckBoxTaskDesignMode = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createANewTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stroMoHab_Panel1 = new CustomControls.StroMoHab_Panel();
            this.groupBxCube.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBxTextureImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLeftRightPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownPlaceObjectAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownSelectObject)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCurrentPosition)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddObject
            // 
            this.btnAddObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnAddObject.Location = new System.Drawing.Point(85, 287);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(140, 32);
            this.btnAddObject.TabIndex = 9;
            this.btnAddObject.Text = "Add Object";
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // groupBxCube
            // 
            this.groupBxCube.BackColor = System.Drawing.Color.White;
            this.groupBxCube.Controls.Add(this.numericUpDownObjectDepth);
            this.groupBxCube.Controls.Add(this.label3);
            this.groupBxCube.Controls.Add(this.numericUpDownObjectHeight);
            this.groupBxCube.Controls.Add(this.label2);
            this.groupBxCube.Controls.Add(this.numericUpDownImageNumber);
            this.groupBxCube.Controls.Add(this.btnAddObject);
            this.groupBxCube.Controls.Add(this.pictureBxTextureImage);
            this.groupBxCube.Controls.Add(this.labelImage);
            this.groupBxCube.Controls.Add(this.numericUpDownDistanceFromGround);
            this.groupBxCube.Controls.Add(this.lblHeight);
            this.groupBxCube.Controls.Add(this.numericUpDownObjectWidth);
            this.groupBxCube.Controls.Add(this.txtBxLeftRightPosition);
            this.groupBxCube.Controls.Add(this.label1);
            this.groupBxCube.Controls.Add(this.trackBarLeftRightPosition);
            this.groupBxCube.Controls.Add(this.lblCubeSize);
            this.groupBxCube.Controls.Add(this.checkBoxUseTexture);
            this.groupBxCube.Location = new System.Drawing.Point(15, 297);
            this.groupBxCube.Name = "groupBxCube";
            this.groupBxCube.Size = new System.Drawing.Size(470, 328);
            this.groupBxCube.TabIndex = 10;
            this.groupBxCube.TabStop = false;
            this.groupBxCube.Text = "Cube";
            this.groupBxCube.Visible = false;
            // 
            // numericUpDownObjectDepth
            // 
            this.numericUpDownObjectDepth.AutoSize = true;
            this.numericUpDownObjectDepth.DecimalPlaces = 1;
            this.numericUpDownObjectDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownObjectDepth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownObjectDepth.Location = new System.Drawing.Point(213, 158);
            this.numericUpDownObjectDepth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownObjectDepth.Name = "numericUpDownObjectDepth";
            this.numericUpDownObjectDepth.Size = new System.Drawing.Size(75, 22);
            this.numericUpDownObjectDepth.TabIndex = 14;
            this.numericUpDownObjectDepth.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.numericUpDownObjectDepth.ValueChanged += new System.EventHandler(this.numericUpDownObjectDepth_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Object Depth:";
            // 
            // numericUpDownObjectHeight
            // 
            this.numericUpDownObjectHeight.AutoSize = true;
            this.numericUpDownObjectHeight.DecimalPlaces = 1;
            this.numericUpDownObjectHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownObjectHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownObjectHeight.Location = new System.Drawing.Point(213, 125);
            this.numericUpDownObjectHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownObjectHeight.Name = "numericUpDownObjectHeight";
            this.numericUpDownObjectHeight.Size = new System.Drawing.Size(75, 22);
            this.numericUpDownObjectHeight.TabIndex = 12;
            this.numericUpDownObjectHeight.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.numericUpDownObjectHeight.ValueChanged += new System.EventHandler(this.numericUpDownObjectHeight_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Object Height:";
            // 
            // numericUpDownImageNumber
            // 
            this.numericUpDownImageNumber.Enabled = false;
            this.numericUpDownImageNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownImageNumber.Location = new System.Drawing.Point(213, 61);
            this.numericUpDownImageNumber.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownImageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageNumber.Name = "numericUpDownImageNumber";
            this.numericUpDownImageNumber.Size = new System.Drawing.Size(62, 22);
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
            this.pictureBxTextureImage.Enabled = false;
            this.pictureBxTextureImage.Location = new System.Drawing.Point(314, 22);
            this.pictureBxTextureImage.Name = "pictureBxTextureImage";
            this.pictureBxTextureImage.Size = new System.Drawing.Size(149, 148);
            this.pictureBxTextureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBxTextureImage.TabIndex = 8;
            this.pictureBxTextureImage.TabStop = false;
            this.pictureBxTextureImage.Visible = false;
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Enabled = false;
            this.labelImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImage.Location = new System.Drawing.Point(8, 61);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(113, 18);
            this.labelImage.TabIndex = 7;
            this.labelImage.Text = "Image Number: ";
            // 
            // numericUpDownDistanceFromGround
            // 
            this.numericUpDownDistanceFromGround.AutoSize = true;
            this.numericUpDownDistanceFromGround.DecimalPlaces = 1;
            this.numericUpDownDistanceFromGround.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownDistanceFromGround.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDistanceFromGround.Location = new System.Drawing.Point(213, 192);
            this.numericUpDownDistanceFromGround.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownDistanceFromGround.Name = "numericUpDownDistanceFromGround";
            this.numericUpDownDistanceFromGround.Size = new System.Drawing.Size(75, 22);
            this.numericUpDownDistanceFromGround.TabIndex = 6;
            this.numericUpDownDistanceFromGround.ValueChanged += new System.EventHandler(this.numericUpDownDistanceFromGround_ValueChanged);
            this.numericUpDownDistanceFromGround.Leave += new System.EventHandler(this.numericUpDownDistanceFromGround_Leave);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeight.Location = new System.Drawing.Point(8, 192);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(159, 18);
            this.lblHeight.TabIndex = 5;
            this.lblHeight.Text = "Distance from Ground:";
            // 
            // numericUpDownObjectWidth
            // 
            this.numericUpDownObjectWidth.AutoSize = true;
            this.numericUpDownObjectWidth.DecimalPlaces = 1;
            this.numericUpDownObjectWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownObjectWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownObjectWidth.Location = new System.Drawing.Point(213, 91);
            this.numericUpDownObjectWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownObjectWidth.Name = "numericUpDownObjectWidth";
            this.numericUpDownObjectWidth.Size = new System.Drawing.Size(75, 22);
            this.numericUpDownObjectWidth.TabIndex = 4;
            this.numericUpDownObjectWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.numericUpDownObjectWidth.ValueChanged += new System.EventHandler(this.numericUpDownObjectWidth_ValueChanged);
            this.numericUpDownObjectWidth.Leave += new System.EventHandler(this.numericUpDownSize_Leave);
            // 
            // txtBxLeftRightPosition
            // 
            this.txtBxLeftRightPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxLeftRightPosition.Location = new System.Drawing.Point(213, 225);
            this.txtBxLeftRightPosition.Name = "txtBxLeftRightPosition";
            this.txtBxLeftRightPosition.Size = new System.Drawing.Size(61, 22);
            this.txtBxLeftRightPosition.TabIndex = 3;
            this.txtBxLeftRightPosition.Text = "0";
            this.txtBxLeftRightPosition.Leave += new System.EventHandler(this.txtBxLeftRightPosition_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Left/Right Position:";
            // 
            // trackBarLeftRightPosition
            // 
            this.trackBarLeftRightPosition.Location = new System.Drawing.Point(12, 255);
            this.trackBarLeftRightPosition.Maximum = 300;
            this.trackBarLeftRightPosition.Name = "trackBarLeftRightPosition";
            this.trackBarLeftRightPosition.Size = new System.Drawing.Size(293, 45);
            this.trackBarLeftRightPosition.TabIndex = 1;
            this.trackBarLeftRightPosition.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarLeftRightPosition.Value = 150;
            this.trackBarLeftRightPosition.ValueChanged += new System.EventHandler(this.trackBarLeftRightPosition_ValueChanged);
            this.trackBarLeftRightPosition.Scroll += new System.EventHandler(this.trackBarLeftRightPosition_Scroll);
            // 
            // lblCubeSize
            // 
            this.lblCubeSize.AutoSize = true;
            this.lblCubeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCubeSize.Location = new System.Drawing.Point(8, 91);
            this.lblCubeSize.Name = "lblCubeSize";
            this.lblCubeSize.Size = new System.Drawing.Size(97, 18);
            this.lblCubeSize.TabIndex = 0;
            this.lblCubeSize.Text = "Object Width:";
            // 
            // checkBoxUseTexture
            // 
            this.checkBoxUseTexture.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxUseTexture.Checked = true;
            this.checkBoxUseTexture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.checkBoxUseTexture.Location = new System.Drawing.Point(10, 19);
            this.checkBoxUseTexture.Name = "checkBoxUseTexture";
            this.checkBoxUseTexture.Size = new System.Drawing.Size(141, 35);
            this.checkBoxUseTexture.TabIndex = 10;
            this.checkBoxUseTexture.Text = "Use Texture";
            this.checkBoxUseTexture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxUseTexture.UseVisualStyleBackColor = true;
            this.checkBoxUseTexture.CheckedChanged += new System.EventHandler(this.checkBoxUseTextures_CheckedChanged);
            // 
            // comboBxObjectToAdd
            // 
            this.comboBxObjectToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBxObjectToAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBxObjectToAdd.FormattingEnabled = true;
            this.comboBxObjectToAdd.Items.AddRange(new object[] {
            "<Select>",
            "Cube",
            "Corridor"});
            this.comboBxObjectToAdd.Location = new System.Drawing.Point(197, 80);
            this.comboBxObjectToAdd.Name = "comboBxObjectToAdd";
            this.comboBxObjectToAdd.Size = new System.Drawing.Size(140, 24);
            this.comboBxObjectToAdd.TabIndex = 8;
            this.comboBxObjectToAdd.SelectedIndexChanged += new System.EventHandler(this.comboBxObjectToAdd_SelectedIndexChanged);
            // 
            // lblObjectToAdd
            // 
            this.lblObjectToAdd.AutoSize = true;
            this.lblObjectToAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjectToAdd.Location = new System.Drawing.Point(6, 82);
            this.lblObjectToAdd.Name = "lblObjectToAdd";
            this.lblObjectToAdd.Size = new System.Drawing.Size(135, 18);
            this.lblObjectToAdd.TabIndex = 7;
            this.lblObjectToAdd.Text = "Object To Add/Edit:";
            // 
            // lblCurrentPosition
            // 
            this.lblCurrentPosition.AutoSize = true;
            this.lblCurrentPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPosition.Location = new System.Drawing.Point(6, 46);
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(113, 18);
            this.lblCurrentPosition.TabIndex = 6;
            this.lblCurrentPosition.Text = "Place Object At:";
            // 
            // customNumericUpDownPlaceObjectAt
            // 
            this.customNumericUpDownPlaceObjectAt.DecimalPlaces = 1;
            this.customNumericUpDownPlaceObjectAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customNumericUpDownPlaceObjectAt.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.customNumericUpDownPlaceObjectAt.Location = new System.Drawing.Point(197, 46);
            this.customNumericUpDownPlaceObjectAt.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.customNumericUpDownPlaceObjectAt.Name = "customNumericUpDownPlaceObjectAt";
            this.customNumericUpDownPlaceObjectAt.Size = new System.Drawing.Size(80, 22);
            this.customNumericUpDownPlaceObjectAt.TabIndex = 11;
            this.customNumericUpDownPlaceObjectAt.Leave += new System.EventHandler(this.customNumericUpDownCurrentPosition_Leave);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // customNumericUpDownSelectObject
            // 
            this.customNumericUpDownSelectObject.Enabled = false;
            this.customNumericUpDownSelectObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customNumericUpDownSelectObject.Location = new System.Drawing.Point(197, 152);
            this.customNumericUpDownSelectObject.Name = "customNumericUpDownSelectObject";
            this.customNumericUpDownSelectObject.Size = new System.Drawing.Size(62, 22);
            this.customNumericUpDownSelectObject.TabIndex = 12;
            this.customNumericUpDownSelectObject.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.customNumericUpDownSelectObject.ValueChanged += new System.EventHandler(this.customNumericUpDownSelectObject_ValueChanged);
            // 
            // labelObjectNumber
            // 
            this.labelObjectNumber.AutoSize = true;
            this.labelObjectNumber.Enabled = false;
            this.labelObjectNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObjectNumber.Location = new System.Drawing.Point(6, 151);
            this.labelObjectNumber.Name = "labelObjectNumber";
            this.labelObjectNumber.Size = new System.Drawing.Size(112, 18);
            this.labelObjectNumber.TabIndex = 13;
            this.labelObjectNumber.Text = "Object Number:";
            // 
            // checkBoxSelectObject
            // 
            this.checkBoxSelectObject.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxSelectObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.checkBoxSelectObject.Location = new System.Drawing.Point(6, 105);
            this.checkBoxSelectObject.Name = "checkBoxSelectObject";
            this.checkBoxSelectObject.Size = new System.Drawing.Size(141, 35);
            this.checkBoxSelectObject.TabIndex = 14;
            this.checkBoxSelectObject.Text = "Select Object";
            this.checkBoxSelectObject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSelectObject.UseVisualStyleBackColor = true;
            this.checkBoxSelectObject.CheckedChanged += new System.EventHandler(this.checkBoxSelectObject_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.numericUpDownCurrentPosition);
            this.panel1.Controls.Add(this.buttonRemoveObject);
            this.panel1.Controls.Add(this.labelCurrentPosition);
            this.panel1.Controls.Add(this.lblCurrentPosition);
            this.panel1.Controls.Add(this.checkBoxSelectObject);
            this.panel1.Controls.Add(this.lblObjectToAdd);
            this.panel1.Controls.Add(this.labelObjectNumber);
            this.panel1.Controls.Add(this.comboBxObjectToAdd);
            this.panel1.Controls.Add(this.customNumericUpDownSelectObject);
            this.panel1.Controls.Add(this.customNumericUpDownPlaceObjectAt);
            this.panel1.Location = new System.Drawing.Point(15, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 197);
            this.panel1.TabIndex = 15;
            // 
            // numericUpDownCurrentPosition
            // 
            this.numericUpDownCurrentPosition.DecimalPlaces = 1;
            this.numericUpDownCurrentPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.numericUpDownCurrentPosition.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownCurrentPosition.Location = new System.Drawing.Point(197, 10);
            this.numericUpDownCurrentPosition.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownCurrentPosition.Name = "numericUpDownCurrentPosition";
            this.numericUpDownCurrentPosition.Size = new System.Drawing.Size(80, 22);
            this.numericUpDownCurrentPosition.TabIndex = 17;
            this.numericUpDownCurrentPosition.ValueChanged += new System.EventHandler(this.numericUpDownCurrentPosition_ValueChanged);
            // 
            // buttonRemoveObject
            // 
            this.buttonRemoveObject.Enabled = false;
            this.buttonRemoveObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.buttonRemoveObject.Location = new System.Drawing.Point(300, 147);
            this.buttonRemoveObject.Name = "buttonRemoveObject";
            this.buttonRemoveObject.Size = new System.Drawing.Size(156, 32);
            this.buttonRemoveObject.TabIndex = 15;
            this.buttonRemoveObject.Text = "Remove Object";
            this.buttonRemoveObject.UseVisualStyleBackColor = true;
            this.buttonRemoveObject.Click += new System.EventHandler(this.buttonRemoveObject_Click);
            // 
            // labelCurrentPosition
            // 
            this.labelCurrentPosition.AutoSize = true;
            this.labelCurrentPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentPosition.Location = new System.Drawing.Point(6, 10);
            this.labelCurrentPosition.Name = "labelCurrentPosition";
            this.labelCurrentPosition.Size = new System.Drawing.Size(119, 18);
            this.labelCurrentPosition.TabIndex = 15;
            this.labelCurrentPosition.Text = "Current Position:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // ckBoxTaskDesignMode
            // 
            this.ckBoxTaskDesignMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckBoxTaskDesignMode.Checked = true;
            this.ckBoxTaskDesignMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxTaskDesignMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ckBoxTaskDesignMode.Location = new System.Drawing.Point(15, 27);
            this.ckBoxTaskDesignMode.Name = "ckBoxTaskDesignMode";
            this.ckBoxTaskDesignMode.Size = new System.Drawing.Size(147, 69);
            this.ckBoxTaskDesignMode.TabIndex = 17;
            this.ckBoxTaskDesignMode.Text = "Task Design Mode";
            this.ckBoxTaskDesignMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckBoxTaskDesignMode.UseVisualStyleBackColor = true;
            this.ckBoxTaskDesignMode.CheckedChanged += new System.EventHandler(this.ckBoxTaskDesignMode_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(275, 27);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(57, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.createANewTaskToolStripMenuItem});
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.taskToolStripMenuItem.Text = "Tasks";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openToolStripMenuItem.Text = "Open an Existing Task";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openExistingTaskToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveToolStripMenuItem.Text = "Save the Current Task";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveTaskToolStripMenuItem_Click);
            // 
            // createANewTaskToolStripMenuItem
            // 
            this.createANewTaskToolStripMenuItem.Name = "createANewTaskToolStripMenuItem";
            this.createANewTaskToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.createANewTaskToolStripMenuItem.Text = "Create a New Task";
            this.createANewTaskToolStripMenuItem.Click += new System.EventHandler(this.createANewTaskToolStripMenuItem_Click);
            // 
            // stroMoHab_Panel1
            // 
            this.stroMoHab_Panel1.AutoSize = true;
            this.stroMoHab_Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stroMoHab_Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stroMoHab_Panel1.Location = new System.Drawing.Point(0, 0);
            this.stroMoHab_Panel1.Name = "stroMoHab_Panel1";
            this.stroMoHab_Panel1.Size = new System.Drawing.Size(1280, 720);
            this.stroMoHab_Panel1.TabIndex = 20;
            // 
            // Form_TaskDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ckBoxTaskDesignMode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBxCube);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.stroMoHab_Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form_TaskDesigner";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Load += new System.EventHandler(this.Form_TaskDesigner_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_TaskDesigner_KeyDown);
            this.groupBxCube.ResumeLayout(false);
            this.groupBxCube.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBxTextureImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLeftRightPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownPlaceObjectAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customNumericUpDownSelectObject)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCurrentPosition)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.GroupBox groupBxCube;
        private System.Windows.Forms.NumericUpDown numericUpDownImageNumber;
        private System.Windows.Forms.PictureBox pictureBxTextureImage;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.NumericUpDown numericUpDownDistanceFromGround;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownObjectWidth;
        private System.Windows.Forms.TextBox txtBxLeftRightPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarLeftRightPosition;
        private System.Windows.Forms.Label lblCubeSize;
        private System.Windows.Forms.ComboBox comboBxObjectToAdd;
        private System.Windows.Forms.Label lblObjectToAdd;
        private System.Windows.Forms.Label lblCurrentPosition;
        private CustomControls.CustomNumericUpDown customNumericUpDownPlaceObjectAt;
        private System.Windows.Forms.CheckBox checkBoxUseTexture;
        private System.Windows.Forms.ImageList imageList;
        private CustomControls.CustomNumericUpDown customNumericUpDownSelectObject;
        private System.Windows.Forms.Label labelObjectNumber;
        private System.Windows.Forms.CheckBox checkBoxSelectObject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label labelCurrentPosition;
        private System.Windows.Forms.NumericUpDown numericUpDownObjectDepth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownObjectHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRemoveObject;
        private System.Windows.Forms.NumericUpDown numericUpDownCurrentPosition;
        private System.Windows.Forms.CheckBox ckBoxTaskDesignMode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createANewTaskToolStripMenuItem;
        private CustomControls.StroMoHab_Panel stroMoHab_Panel1;

    }
}

