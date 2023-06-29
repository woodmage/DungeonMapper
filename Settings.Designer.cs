namespace DungeonMapper
{
    partial class Settings
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
            btnback = new Button();
            btnfore = new Button();
            btngrid = new Button();
            btnbutt = new Button();
            cbgridsave = new CheckBox();
            cbgridview = new CheckBox();
            but0 = new Button();
            but1 = new Button();
            picture = new PictureBox();
            btnaccept = new Button();
            btncancel = new Button();
            colorsel = new ColorDialog();
            label1 = new Label();
            updnbuttonset = new NumericUpDown();
            label2 = new Label();
            mapsizex = new NumericUpDown();
            mapsizey = new NumericUpDown();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)updnbuttonset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mapsizex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mapsizey).BeginInit();
            SuspendLayout();
            // 
            // btnback
            // 
            btnback.Location = new Point(12, 12);
            btnback.Name = "btnback";
            btnback.Size = new Size(98, 58);
            btnback.TabIndex = 0;
            btnback.Text = "background color";
            btnback.UseVisualStyleBackColor = true;
            btnback.Click += BackgroundColor;
            // 
            // btnfore
            // 
            btnfore.Location = new Point(12, 76);
            btnfore.Name = "btnfore";
            btnfore.Size = new Size(98, 58);
            btnfore.TabIndex = 0;
            btnfore.Text = "foreground color";
            btnfore.UseVisualStyleBackColor = true;
            btnfore.Click += ForegroundColor;
            // 
            // btngrid
            // 
            btngrid.Location = new Point(12, 140);
            btngrid.Name = "btngrid";
            btngrid.Size = new Size(98, 58);
            btngrid.TabIndex = 0;
            btngrid.Text = "gridline color";
            btngrid.UseVisualStyleBackColor = true;
            btngrid.Click += GridColor;
            // 
            // btnbutt
            // 
            btnbutt.Location = new Point(12, 204);
            btnbutt.Name = "btnbutt";
            btnbutt.Size = new Size(98, 58);
            btnbutt.TabIndex = 0;
            btnbutt.Text = "button color";
            btnbutt.UseVisualStyleBackColor = true;
            btnbutt.Click += ButtonColor;
            // 
            // cbgridsave
            // 
            cbgridsave.AutoSize = true;
            cbgridsave.Location = new Point(12, 303);
            cbgridsave.Name = "cbgridsave";
            cbgridsave.Size = new Size(107, 24);
            cbgridsave.TabIndex = 1;
            cbgridsave.Text = "Grid in Save";
            cbgridsave.UseVisualStyleBackColor = true;
            cbgridsave.CheckedChanged += GridSave;
            // 
            // cbgridview
            // 
            cbgridview.AutoSize = true;
            cbgridview.Location = new Point(12, 273);
            cbgridview.Name = "cbgridview";
            cbgridview.Size = new Size(108, 24);
            cbgridview.TabIndex = 1;
            cbgridview.Text = "Grid in View";
            cbgridview.UseVisualStyleBackColor = true;
            cbgridview.CheckedChanged += GridView;
            // 
            // but0
            // 
            but0.Location = new Point(126, 76);
            but0.Name = "but0";
            but0.Size = new Size(35, 35);
            but0.TabIndex = 2;
            but0.UseVisualStyleBackColor = true;
            // 
            // but1
            // 
            but1.Location = new Point(167, 76);
            but1.Name = "but1";
            but1.Size = new Size(35, 35);
            but1.TabIndex = 2;
            but1.UseVisualStyleBackColor = true;
            // 
            // picture
            // 
            picture.Location = new Point(126, 114);
            picture.Name = "picture";
            picture.Size = new Size(224, 213);
            picture.TabIndex = 3;
            picture.TabStop = false;
            // 
            // btnaccept
            // 
            btnaccept.Location = new Point(12, 333);
            btnaccept.Name = "btnaccept";
            btnaccept.Size = new Size(75, 30);
            btnaccept.TabIndex = 4;
            btnaccept.Text = "Accept";
            btnaccept.UseVisualStyleBackColor = true;
            btnaccept.Click += Accept;
            // 
            // btncancel
            // 
            btncancel.Location = new Point(275, 333);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(75, 30);
            btncancel.TabIndex = 4;
            btncancel.Text = "Cancel";
            btncancel.UseVisualStyleBackColor = true;
            btncancel.Click += Cancel;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(208, 83);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 5;
            label1.Text = "Buttons:";
            // 
            // updnbuttonset
            // 
            updnbuttonset.Location = new Point(276, 81);
            updnbuttonset.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            updnbuttonset.Name = "updnbuttonset";
            updnbuttonset.Size = new Size(74, 27);
            updnbuttonset.TabIndex = 6;
            updnbuttonset.ValueChanged += ButtonSet;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 19);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 7;
            label2.Text = "Map Size:";
            // 
            // mapsizex
            // 
            mapsizex.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            mapsizex.Location = new Point(126, 43);
            mapsizex.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            mapsizex.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            mapsizex.Name = "mapsizex";
            mapsizex.Size = new Size(98, 27);
            mapsizex.TabIndex = 8;
            mapsizex.Value = new decimal(new int[] { 100, 0, 0, 0 });
            mapsizex.ValueChanged += MapSizeChanged;
            // 
            // mapsizey
            // 
            mapsizey.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            mapsizey.Location = new Point(252, 43);
            mapsizey.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            mapsizey.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            mapsizey.Name = "mapsizey";
            mapsizey.Size = new Size(98, 27);
            mapsizey.TabIndex = 8;
            mapsizey.Value = new decimal(new int[] { 100, 0, 0, 0 });
            mapsizey.ValueChanged += MapSizeChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(230, 45);
            label3.Name = "label3";
            label3.Size = new Size(16, 20);
            label3.TabIndex = 7;
            label3.Text = "x";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(361, 374);
            Controls.Add(mapsizey);
            Controls.Add(mapsizex);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(updnbuttonset);
            Controls.Add(label1);
            Controls.Add(btncancel);
            Controls.Add(btnaccept);
            Controls.Add(picture);
            Controls.Add(but1);
            Controls.Add(but0);
            Controls.Add(cbgridview);
            Controls.Add(cbgridsave);
            Controls.Add(btnbutt);
            Controls.Add(btngrid);
            Controls.Add(btnfore);
            Controls.Add(btnback);
            Name = "Settings";
            Text = "Settings";
            Load += OnLoad;
            ((System.ComponentModel.ISupportInitialize)picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)updnbuttonset).EndInit();
            ((System.ComponentModel.ISupportInitialize)mapsizex).EndInit();
            ((System.ComponentModel.ISupportInitialize)mapsizey).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnback;
        private Button btnfore;
        private Button btngrid;
        private Button btnbutt;
        private CheckBox cbgridsave;
        private CheckBox cbgridview;
        private Button but0;
        private Button but1;
        private PictureBox picture;
        private Button btnaccept;
        private Button btncancel;
        private ColorDialog colorsel;
        private Label label1;
        private NumericUpDown updnbuttonset;
        private Label label2;
        private NumericUpDown mapsizex;
        private NumericUpDown mapsizey;
        private Label label3;
    }
}