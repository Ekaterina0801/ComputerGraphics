namespace task9
{
    partial class FloatingHorizon
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
            picBox = new PictureBox();
            button1 = new Button();
            trackBarX = new TrackBar();
            trackBarZ = new TrackBar();
            trackBarY = new TrackBar();
            groupBox1 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmbBoxFunctions = new ComboBox();
            txtBoxXBegin = new TextBox();
            label4 = new Label();
            txtBoxXEnd = new TextBox();
            txtBoxXStep = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label10 = new Label();
            label11 = new Label();
            txtBoxZStep = new TextBox();
            txtBoxZEnd = new TextBox();
            label12 = new Label();
            txtBoxZBegin = new TextBox();
            groupBoxOptions = new GroupBox();
            groupBox2 = new GroupBox();
            btnMainColor = new Button();
            btnBackColor = new Button();
            label13 = new Label();
            label7 = new Label();
            colorDlg = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarY).BeginInit();
            groupBox1.SuspendLayout();
            groupBoxOptions.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // picBox
            // 
            picBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picBox.Location = new Point(-1, 0);
            picBox.Margin = new Padding(4, 5, 4, 5);
            picBox.Name = "picBox";
            picBox.Size = new Size(1327, 986);
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            picBox.MouseDown += picBox_MouseDown;
            picBox.MouseMove += picBox_MouseMove;
            picBox.MouseUp += picBox_MouseUp;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(13, 86);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(275, 54);
            button1.TabIndex = 1;
            button1.Text = "Draw";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // trackBarX
            // 
            trackBarX.Location = new Point(8, 195);
            trackBarX.Margin = new Padding(4, 5, 4, 5);
            trackBarX.Maximum = 360;
            trackBarX.Name = "trackBarX";
            trackBarX.Size = new Size(280, 56);
            trackBarX.SmallChange = 10;
            trackBarX.TabIndex = 2;
            trackBarX.Value = 30;
            trackBarX.ValueChanged += trackBarX_ValueChanged;
            // 
            // trackBarZ
            // 
            trackBarZ.Location = new Point(8, 352);
            trackBarZ.Margin = new Padding(4, 5, 4, 5);
            trackBarZ.Maximum = 360;
            trackBarZ.Name = "trackBarZ";
            trackBarZ.Size = new Size(280, 56);
            trackBarZ.TabIndex = 3;
            trackBarZ.ValueChanged += trackBarZ_ValueChanged;
            // 
            // trackBarY
            // 
            trackBarY.Location = new Point(8, 274);
            trackBarY.Margin = new Padding(4, 5, 4, 5);
            trackBarY.Maximum = 360;
            trackBarY.Name = "trackBarY";
            trackBarY.Size = new Size(280, 56);
            trackBarY.SmallChange = 10;
            trackBarY.TabIndex = 4;
            trackBarY.Value = 15;
            trackBarY.ValueChanged += trackBarY_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbBoxFunctions);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(trackBarY);
            groupBox1.Controls.Add(trackBarX);
            groupBox1.Controls.Add(trackBarZ);
            groupBox1.Location = new Point(1340, 20);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(316, 438);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(13, 328);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 9;
            label3.Text = "Z Rotate:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(13, 249);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 8;
            label2.Text = "Y Rotate:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(13, 166);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 7;
            label1.Text = "X Rotate:";
            // 
            // cmbBoxFunctions
            // 
            cmbBoxFunctions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxFunctions.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbBoxFunctions.FormattingEnabled = true;
            cmbBoxFunctions.Location = new Point(13, 29);
            cmbBoxFunctions.Margin = new Padding(4, 5, 4, 5);
            cmbBoxFunctions.Name = "cmbBoxFunctions";
            cmbBoxFunctions.Size = new Size(273, 33);
            cmbBoxFunctions.TabIndex = 6;
            cmbBoxFunctions.SelectedIndexChanged += cmbBoxFunctions_SelectedIndexChanged;
            // 
            // txtBoxXBegin
            // 
            txtBoxXBegin.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxXBegin.Location = new Point(13, 74);
            txtBoxXBegin.Margin = new Padding(4, 5, 4, 5);
            txtBoxXBegin.Name = "txtBoxXBegin";
            txtBoxXBegin.Size = new Size(79, 34);
            txtBoxXBegin.TabIndex = 6;
            txtBoxXBegin.Text = "-5";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(85, 25);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(34, 25);
            label4.TabIndex = 7;
            label4.Text = "X:";
            // 
            // txtBoxXEnd
            // 
            txtBoxXEnd.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxXEnd.Location = new Point(128, 74);
            txtBoxXEnd.Margin = new Padding(4, 5, 4, 5);
            txtBoxXEnd.Name = "txtBoxXEnd";
            txtBoxXEnd.Size = new Size(79, 34);
            txtBoxXEnd.TabIndex = 8;
            txtBoxXEnd.Text = "5";
            // 
            // txtBoxXStep
            // 
            txtBoxXStep.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxXStep.Location = new Point(235, 74);
            txtBoxXStep.Margin = new Padding(4, 5, 4, 5);
            txtBoxXStep.Name = "txtBoxXStep";
            txtBoxXStep.Size = new Size(79, 34);
            txtBoxXStep.TabIndex = 9;
            txtBoxXStep.Text = "0.1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(101, 83);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(19, 25);
            label5.TabIndex = 10;
            label5.Text = "-";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(239, 25);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(59, 25);
            label6.TabIndex = 11;
            label6.Text = "Step:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(239, 140);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(59, 25);
            label10.TabIndex = 23;
            label10.Text = "Step:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(101, 198);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(19, 25);
            label11.TabIndex = 22;
            label11.Text = "-";
            // 
            // txtBoxZStep
            // 
            txtBoxZStep.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxZStep.Location = new Point(235, 189);
            txtBoxZStep.Margin = new Padding(4, 5, 4, 5);
            txtBoxZStep.Name = "txtBoxZStep";
            txtBoxZStep.Size = new Size(79, 34);
            txtBoxZStep.TabIndex = 21;
            txtBoxZStep.Text = "0.1";
            // 
            // txtBoxZEnd
            // 
            txtBoxZEnd.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxZEnd.Location = new Point(128, 189);
            txtBoxZEnd.Margin = new Padding(4, 5, 4, 5);
            txtBoxZEnd.Name = "txtBoxZEnd";
            txtBoxZEnd.Size = new Size(79, 34);
            txtBoxZEnd.TabIndex = 20;
            txtBoxZEnd.Text = "5";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(85, 140);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(32, 25);
            label12.TabIndex = 19;
            label12.Text = "Z:";
            // 
            // txtBoxZBegin
            // 
            txtBoxZBegin.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxZBegin.Location = new Point(13, 189);
            txtBoxZBegin.Margin = new Padding(4, 5, 4, 5);
            txtBoxZBegin.Name = "txtBoxZBegin";
            txtBoxZBegin.Size = new Size(79, 34);
            txtBoxZBegin.TabIndex = 18;
            txtBoxZBegin.Text = "-5";
            // 
            // groupBoxOptions
            // 
            groupBoxOptions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxOptions.Controls.Add(txtBoxXBegin);
            groupBoxOptions.Controls.Add(label10);
            groupBoxOptions.Controls.Add(label4);
            groupBoxOptions.Controls.Add(label11);
            groupBoxOptions.Controls.Add(txtBoxXEnd);
            groupBoxOptions.Controls.Add(txtBoxZStep);
            groupBoxOptions.Controls.Add(txtBoxXStep);
            groupBoxOptions.Controls.Add(txtBoxZEnd);
            groupBoxOptions.Controls.Add(label5);
            groupBoxOptions.Controls.Add(label12);
            groupBoxOptions.Controls.Add(label6);
            groupBoxOptions.Controls.Add(txtBoxZBegin);
            groupBoxOptions.Location = new Point(1340, 462);
            groupBoxOptions.Margin = new Padding(4, 5, 4, 5);
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Padding = new Padding(4, 5, 4, 5);
            groupBoxOptions.Size = new Size(316, 352);
            groupBoxOptions.TabIndex = 24;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Options";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(btnMainColor);
            groupBox2.Controls.Add(btnBackColor);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(1340, 823);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(321, 175);
            groupBox2.TabIndex = 25;
            groupBox2.TabStop = false;
            groupBox2.Text = "Colors";
            // 
            // btnMainColor
            // 
            btnMainColor.Location = new Point(187, 111);
            btnMainColor.Margin = new Padding(4, 5, 4, 5);
            btnMainColor.Name = "btnMainColor";
            btnMainColor.Size = new Size(101, 52);
            btnMainColor.TabIndex = 4;
            btnMainColor.UseVisualStyleBackColor = true;
            btnMainColor.Click += btnMainColor_Click;
            // 
            // btnBackColor
            // 
            btnBackColor.Location = new Point(187, 35);
            btnBackColor.Margin = new Padding(4, 5, 4, 5);
            btnBackColor.Name = "btnBackColor";
            btnBackColor.Size = new Size(101, 52);
            btnBackColor.TabIndex = 3;
            btnBackColor.UseVisualStyleBackColor = true;
            btnBackColor.Click += btnBackColor_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(19, 118);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(136, 29);
            label13.TabIndex = 2;
            label13.Text = "Основной:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(19, 51);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(68, 29);
            label7.TabIndex = 1;
            label7.Text = "Фон:";
            // 
            // FloatingHorizon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1659, 1005);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxOptions);
            Controls.Add(groupBox1);
            Controls.Add(picBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "FloatingHorizon";
            Text = "Floating Horizon";
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarY).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBoxOptions.ResumeLayout(false);
            groupBoxOptions.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBox;
        private Button button1;
        private TrackBar trackBarX;
        private TrackBar trackBarZ;
        private TrackBar trackBarY;
        private GroupBox groupBox1;
        private ComboBox cmbBoxFunctions;
        private Label label1;
        private Label label3;
        private Label label2;
        private TextBox txtBoxXBegin;
        private Label label4;
        private TextBox txtBoxXEnd;
        private TextBox txtBoxXStep;
        private Label label5;
        private Label label6;
        private Label label10;
        private Label label11;
        private TextBox txtBoxZStep;
        private TextBox txtBoxZEnd;
        private Label label12;
        private TextBox txtBoxZBegin;
        private GroupBox groupBoxOptions;
        private GroupBox groupBox2;
        private Label label13;
        private Label label7;
        private ColorDialog colorDlg;
        private Button btnMainColor;
        private Button btnBackColor;
    }
}

