namespace task9
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            ProjectionBox = new ComboBox();
            ApplyProjection = new Button();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            numericUpDown5 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            numericUpDown7 = new NumericUpDown();
            numericUpDown8 = new NumericUpDown();
            numericUpDown9 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ApplyAffin = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            listBox1 = new ListBox();
            button1 = new Button();
            sceneView1 = new Scene();
            label7 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label8 = new Label();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // ProjectionBox
            // 
            ProjectionBox.FormattingEnabled = true;
            ProjectionBox.Items.AddRange(new object[] { "Перспективная", "Ортографическая XY", "Ортографическая XZ", "Ортографическая YZ" });
            ProjectionBox.Location = new Point(7, 47);
            ProjectionBox.Name = "ProjectionBox";
            ProjectionBox.Size = new Size(176, 28);
            ProjectionBox.TabIndex = 1;
            // 
            // ApplyProjection
            // 
            ApplyProjection.BackColor = SystemColors.ButtonHighlight;
            ApplyProjection.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            ApplyProjection.FlatAppearance.BorderSize = 2;
            ApplyProjection.FlatStyle = FlatStyle.Flat;
            ApplyProjection.Location = new Point(7, 99);
            ApplyProjection.Name = "ApplyProjection";
            ApplyProjection.Size = new Size(176, 39);
            ApplyProjection.TabIndex = 2;
            ApplyProjection.Text = "Применить";
            ApplyProjection.UseVisualStyleBackColor = false;
            ApplyProjection.Click += ApplyProjection_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(106, 39);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(59, 26);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 1;
            numericUpDown2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown2.Location = new Point(180, 39);
            numericUpDown2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(61, 26);
            numericUpDown2.TabIndex = 4;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 1;
            numericUpDown3.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown3.Location = new Point(259, 39);
            numericUpDown3.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(68, 26);
            numericUpDown3.TabIndex = 5;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown4
            // 
            numericUpDown4.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown4.Location = new Point(106, 81);
            numericUpDown4.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(59, 26);
            numericUpDown4.TabIndex = 6;
            // 
            // numericUpDown5
            // 
            numericUpDown5.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown5.Location = new Point(180, 81);
            numericUpDown5.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(61, 26);
            numericUpDown5.TabIndex = 7;
            // 
            // numericUpDown6
            // 
            numericUpDown6.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown6.Location = new Point(259, 81);
            numericUpDown6.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown6.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(68, 26);
            numericUpDown6.TabIndex = 8;
            // 
            // numericUpDown7
            // 
            numericUpDown7.DecimalPlaces = 2;
            numericUpDown7.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown7.Location = new Point(106, 118);
            numericUpDown7.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown7.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(59, 26);
            numericUpDown7.TabIndex = 9;
            // 
            // numericUpDown8
            // 
            numericUpDown8.DecimalPlaces = 2;
            numericUpDown8.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown8.Location = new Point(180, 118);
            numericUpDown8.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown8.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(61, 26);
            numericUpDown8.TabIndex = 10;
            // 
            // numericUpDown9
            // 
            numericUpDown9.DecimalPlaces = 2;
            numericUpDown9.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown9.Location = new Point(259, 118);
            numericUpDown9.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown9.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown9.Name = "numericUpDown9";
            numericUpDown9.Size = new Size(68, 26);
            numericUpDown9.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(126, 8);
            label1.Name = "label1";
            label1.Size = new Size(20, 20);
            label1.TabIndex = 12;
            label1.Text = "X";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(202, 8);
            label2.Name = "label2";
            label2.Size = new Size(20, 20);
            label2.TabIndex = 13;
            label2.Text = "Y";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(278, 8);
            label3.Name = "label3";
            label3.Size = new Size(19, 20);
            label3.TabIndex = 14;
            label3.Text = "Z";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(7, 45);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 15;
            label4.Text = "Масштаб";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(7, 83);
            label5.Name = "label5";
            label5.Size = new Size(75, 20);
            label5.TabIndex = 16;
            label5.Text = "Поворот";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(7, 120);
            label6.Name = "label6";
            label6.Size = new Size(56, 20);
            label6.TabIndex = 17;
            label6.Text = "Сдвиг";
            // 
            // ApplyAffin
            // 
            ApplyAffin.BackColor = SystemColors.ButtonHighlight;
            ApplyAffin.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            ApplyAffin.FlatAppearance.BorderSize = 2;
            ApplyAffin.FlatStyle = FlatStyle.Flat;
            ApplyAffin.Location = new Point(75, 170);
            ApplyAffin.Name = "ApplyAffin";
            ApplyAffin.Size = new Size(185, 37);
            ApplyAffin.TabIndex = 18;
            ApplyAffin.Text = "Применить";
            ApplyAffin.UseVisualStyleBackColor = false;
            ApplyAffin.Click += ApplyAffin_Click;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.ButtonHighlight;
            SaveButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            SaveButton.FlatAppearance.BorderSize = 2;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Location = new Point(75, 225);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(185, 44);
            SaveButton.TabIndex = 19;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.BackColor = SystemColors.ButtonHighlight;
            LoadButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            LoadButton.FlatAppearance.BorderSize = 2;
            LoadButton.FlatStyle = FlatStyle.Flat;
            LoadButton.Location = new Point(75, 293);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(185, 52);
            LoadButton.TabIndex = 20;
            LoadButton.Text = "Загрузить";
            LoadButton.UseVisualStyleBackColor = false;
            LoadButton.Click += LoadButton_Click;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.White;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Items.AddRange(new object[] { "x+y", "x*x+y*y", "sin(x)+cos(y)", "sin(x)*cos(y)", "cap", "cap2" });
            listBox1.Location = new Point(55, 26);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 124);
            listBox1.TabIndex = 21;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(36, 156);
            button1.Name = "button1";
            button1.Size = new Size(164, 44);
            button1.TabIndex = 22;
            button1.Text = "Применить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // sceneView1
            // 
            sceneView1.BackColor = SystemColors.ControlLightLight;
            sceneView1.Camera = null;
            sceneView1.Drawable = null;
            sceneView1.ForeColor = SystemColors.ActiveCaption;
            sceneView1.Location = new Point(534, 41);
            sceneView1.Name = "sceneView1";
            sceneView1.Size = new Size(707, 602);
            sceneView1.TabIndex = 0;
            sceneView1.Text = "sceneView1";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Location = new Point(52, 19);
            label7.Name = "label7";
            label7.Size = new Size(83, 20);
            label7.TabIndex = 23;
            label7.Text = "Проекция";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(LoadButton);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(SaveButton);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(ApplyAffin);
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(numericUpDown9);
            panel1.Controls.Add(numericUpDown4);
            panel1.Controls.Add(numericUpDown6);
            panel1.Controls.Add(numericUpDown8);
            panel1.Controls.Add(numericUpDown3);
            panel1.Controls.Add(numericUpDown7);
            panel1.Controls.Add(numericUpDown2);
            panel1.Controls.Add(numericUpDown5);
            panel1.Location = new Point(40, 270);
            panel1.Name = "panel1";
            panel1.Size = new Size(462, 373);
            panel1.TabIndex = 24;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(listBox1);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(button1);
            panel2.Location = new Point(263, 41);
            panel2.Name = "panel2";
            panel2.Size = new Size(239, 223);
            panel2.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Location = new Point(82, 0);
            label8.Name = "label8";
            label8.Size = new Size(75, 20);
            label8.TabIndex = 26;
            label8.Text = "Функция";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(ProjectionBox);
            panel3.Controls.Add(ApplyProjection);
            panel3.Location = new Point(40, 43);
            panel3.Name = "panel3";
            panel3.Size = new Size(204, 221);
            panel3.TabIndex = 27;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroundImage;
            ClientSize = new Size(1264, 681);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(sceneView1);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Плавающий горизонт";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Scene sceneView1;
        private System.Windows.Forms.ComboBox ProjectionBox;
        private System.Windows.Forms.Button ApplyProjection;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ApplyAffin;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private Label label7;
        private Panel panel1;
        private Panel panel2;
        private Label label8;
        private Panel panel3;
    }
}

