namespace task9
{
    partial class Guro
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
            label1 = new Label();
            ApplyProjection = new Button();
            sceneView1 = new Scene();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            numericUpDown5 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            numericUpDown7 = new NumericUpDown();
            numericUpDown8 = new NumericUpDown();
            numericUpDown9 = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ApplyAffin = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            FigureBox = new ComboBox();
            panel4 = new Panel();
            figureLabel = new Label();
            ApplyFigure = new Button();
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
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // ProjectionBox
            // 
            ProjectionBox.FormattingEnabled = true;
            ProjectionBox.Items.AddRange(new object[] { "Перспективная", "Ортографическая XY", "Ортографическая XZ", "Ортографическая YZ" });
            ProjectionBox.Location = new Point(82, 42);
            ProjectionBox.Name = "ProjectionBox";
            ProjectionBox.Size = new Size(173, 31);
            ProjectionBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(117, 16);
            label1.Name = "label1";
            label1.Size = new Size(87, 23);
            label1.TabIndex = 2;
            label1.Text = "Проекция";
            // 
            // ApplyProjection
            // 
            ApplyProjection.BackColor = SystemColors.ButtonHighlight;
            ApplyProjection.FlatAppearance.BorderColor = Color.Silver;
            ApplyProjection.FlatAppearance.BorderSize = 2;
            ApplyProjection.FlatStyle = FlatStyle.Flat;
            ApplyProjection.Location = new Point(82, 79);
            ApplyProjection.Name = "ApplyProjection";
            ApplyProjection.Size = new Size(173, 33);
            ApplyProjection.TabIndex = 3;
            ApplyProjection.Text = "Применить";
            ApplyProjection.UseVisualStyleBackColor = false;
            ApplyProjection.Click += ApplyProjection_Click;
            // 
            // sceneView1
            // 
            sceneView1.BackColor = SystemColors.ControlLightLight;
            sceneView1.Camera = null;
            sceneView1.Drawable = null;
            sceneView1.Location = new Point(12, 12);
            sceneView1.Name = "sceneView1";
            sceneView1.Size = new Size(889, 657);
            sceneView1.TabIndex = 0;
            sceneView1.Text = "sceneView1";
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(92, 32);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(50, 30);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 1;
            numericUpDown2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown2.Location = new Point(164, 32);
            numericUpDown2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(56, 30);
            numericUpDown2.TabIndex = 5;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 1;
            numericUpDown3.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown3.Location = new Point(240, 32);
            numericUpDown3.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(61, 30);
            numericUpDown3.TabIndex = 6;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDown4
            // 
            numericUpDown4.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown4.Location = new Point(92, 78);
            numericUpDown4.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(50, 30);
            numericUpDown4.TabIndex = 7;
            // 
            // numericUpDown5
            // 
            numericUpDown5.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown5.Location = new Point(164, 78);
            numericUpDown5.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(56, 30);
            numericUpDown5.TabIndex = 8;
            // 
            // numericUpDown6
            // 
            numericUpDown6.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown6.Location = new Point(240, 78);
            numericUpDown6.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDown6.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(61, 30);
            numericUpDown6.TabIndex = 9;
            // 
            // numericUpDown7
            // 
            numericUpDown7.DecimalPlaces = 2;
            numericUpDown7.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown7.Location = new Point(92, 127);
            numericUpDown7.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown7.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(50, 30);
            numericUpDown7.TabIndex = 10;
            // 
            // numericUpDown8
            // 
            numericUpDown8.DecimalPlaces = 2;
            numericUpDown8.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown8.Location = new Point(164, 127);
            numericUpDown8.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown8.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(56, 30);
            numericUpDown8.TabIndex = 11;
            // 
            // numericUpDown9
            // 
            numericUpDown9.DecimalPlaces = 2;
            numericUpDown9.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDown9.Location = new Point(240, 127);
            numericUpDown9.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown9.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDown9.Name = "numericUpDown9";
            numericUpDown9.Size = new Size(61, 30);
            numericUpDown9.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(102, 9);
            label2.Name = "label2";
            label2.Size = new Size(22, 23);
            label2.TabIndex = 13;
            label2.Text = "X";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(183, 9);
            label3.Name = "label3";
            label3.Size = new Size(21, 23);
            label3.TabIndex = 14;
            label3.Text = "Y";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(262, 9);
            label4.Name = "label4";
            label4.Size = new Size(22, 23);
            label4.TabIndex = 15;
            label4.Text = "Z";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 34);
            label5.Name = "label5";
            label5.Size = new Size(77, 23);
            label5.TabIndex = 16;
            label5.Text = "Масштаб";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 84);
            label6.Name = "label6";
            label6.Size = new Size(77, 23);
            label6.TabIndex = 17;
            label6.Text = "Поворот";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 127);
            label7.Name = "label7";
            label7.Size = new Size(55, 23);
            label7.TabIndex = 18;
            label7.Text = "Сдвиг";
            // 
            // ApplyAffin
            // 
            ApplyAffin.BackColor = SystemColors.ButtonHighlight;
            ApplyAffin.FlatAppearance.BorderColor = Color.Silver;
            ApplyAffin.FlatAppearance.BorderSize = 2;
            ApplyAffin.FlatStyle = FlatStyle.Flat;
            ApplyAffin.Location = new Point(82, 170);
            ApplyAffin.Name = "ApplyAffin";
            ApplyAffin.Size = new Size(173, 37);
            ApplyAffin.TabIndex = 19;
            ApplyAffin.Text = "Применить";
            ApplyAffin.UseVisualStyleBackColor = false;
            ApplyAffin.Click += ApplyAffin_Click;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = SystemColors.ButtonHighlight;
            SaveButton.FlatAppearance.BorderColor = Color.Silver;
            SaveButton.FlatAppearance.BorderSize = 2;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Location = new Point(82, 16);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(173, 36);
            SaveButton.TabIndex = 20;
            SaveButton.Text = "Сохранить модель";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.BackColor = SystemColors.ButtonHighlight;
            LoadButton.FlatAppearance.BorderColor = Color.Silver;
            LoadButton.FlatAppearance.BorderSize = 2;
            LoadButton.FlatStyle = FlatStyle.Flat;
            LoadButton.Location = new Point(82, 58);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(173, 36);
            LoadButton.TabIndex = 21;
            LoadButton.Text = "Загрузить модель";
            LoadButton.UseVisualStyleBackColor = false;
            LoadButton.Click += LoadButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(ProjectionBox);
            panel1.Controls.Add(ApplyProjection);
            panel1.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            panel1.Location = new Point(928, 254);
            panel1.Name = "panel1";
            panel1.Size = new Size(324, 138);
            panel1.TabIndex = 22;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(numericUpDown1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(numericUpDown2);
            panel2.Controls.Add(ApplyAffin);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(numericUpDown3);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(numericUpDown4);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(numericUpDown5);
            panel2.Controls.Add(numericUpDown9);
            panel2.Controls.Add(numericUpDown6);
            panel2.Controls.Add(numericUpDown8);
            panel2.Controls.Add(numericUpDown7);
            panel2.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            panel2.Location = new Point(928, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(325, 222);
            panel2.TabIndex = 23;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(SaveButton);
            panel3.Controls.Add(LoadButton);
            panel3.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            panel3.Location = new Point(928, 415);
            panel3.Name = "panel3";
            panel3.Size = new Size(324, 110);
            panel3.TabIndex = 24;
            // 
            // FigureBox
            // 
            FigureBox.FormattingEnabled = true;
            FigureBox.Items.AddRange(new object[] { "Тетраэдр", "Куб", "Октаэдр", "Сфера" });
            FigureBox.Location = new Point(79, 32);
            FigureBox.Name = "FigureBox";
            FigureBox.Size = new Size(173, 31);
            FigureBox.TabIndex = 25;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(figureLabel);
            panel4.Controls.Add(ApplyFigure);
            panel4.Controls.Add(FigureBox);
            panel4.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            panel4.Location = new Point(931, 556);
            panel4.Name = "panel4";
            panel4.Size = new Size(321, 113);
            panel4.TabIndex = 26;
            // 
            // figureLabel
            // 
            figureLabel.AutoSize = true;
            figureLabel.Location = new Point(131, 9);
            figureLabel.Name = "figureLabel";
            figureLabel.Size = new Size(69, 23);
            figureLabel.TabIndex = 27;
            figureLabel.Text = "Модель";
            // 
            // ApplyFigure
            // 
            ApplyFigure.BackColor = SystemColors.ButtonHighlight;
            ApplyFigure.Location = new Point(79, 66);
            ApplyFigure.Name = "ApplyFigure";
            ApplyFigure.Size = new Size(173, 32);
            ApplyFigure.TabIndex = 26;
            ApplyFigure.Text = "Применить";
            ApplyFigure.UseVisualStyleBackColor = false;
            ApplyFigure.Click += ApplyFigure_Click;
            // 
            // Guro
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroundImage;
            ClientSize = new Size(1264, 681);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(sceneView1);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "Guro";
            Text = "Guro";
            Load += Guro_Load;
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
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Scene sceneView1;
        private System.Windows.Forms.ComboBox ProjectionBox;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ApplyAffin;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private ComboBox FigureBox;
        private Panel panel4;
        private Button ApplyFigure;
        private Label figureLabel;
    }
}