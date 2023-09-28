namespace lab4
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
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            polygonRB = new RadioButton();
            clearBtn = new Button();
            segmentRB = new RadioButton();
            pointRB = new RadioButton();
            groupBox2 = new GroupBox();
            biasBtn = new Button();
            biasYNumUD = new NumericUpDown();
            biasXNumUD = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            groupBox3 = new GroupBox();
            scaleAroundPointCB = new CheckBox();
            label2 = new Label();
            scaleBtn = new Button();
            scaleYNumUD = new NumericUpDown();
            scaleXNumUD = new NumericUpDown();
            label8 = new Label();
            groupBox6 = new GroupBox();
            label7 = new Label();
            label6 = new Label();
            label3 = new Label();
            intersectionPointLabel = new Label();
            posRelativeToPolygonLabel = new Label();
            posRelativeToSegmentLabel = new Label();
            groupBox5 = new GroupBox();
            rotationAroundPointCB = new CheckBox();
            angle90Btn = new Button();
            rotationBtn = new Button();
            angleNumUD = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)biasYNumUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)biasXNumUD).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scaleYNumUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scaleXNumUD).BeginInit();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)angleNumUD).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(395, 335);
            pictureBox1.Margin = new Padding(6, 7, 6, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1464, 822);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(polygonRB);
            groupBox1.Controls.Add(clearBtn);
            groupBox1.Controls.Add(segmentRB);
            groupBox1.Controls.Add(pointRB);
            groupBox1.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(9, 0);
            groupBox1.Margin = new Padding(6, 7, 6, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 7, 6, 7);
            groupBox1.Size = new Size(312, 310);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Примитив";
            // 
            // polygonRB
            // 
            polygonRB.AutoSize = true;
            polygonRB.Location = new Point(13, 162);
            polygonRB.Margin = new Padding(6, 7, 6, 7);
            polygonRB.Name = "polygonRB";
            polygonRB.Size = new Size(151, 39);
            polygonRB.TabIndex = 3;
            polygonRB.TabStop = true;
            polygonRB.Text = "Полигон";
            polygonRB.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(63, 212);
            clearBtn.Margin = new Padding(6, 7, 6, 7);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(162, 64);
            clearBtn.TabIndex = 3;
            clearBtn.Text = "Очистить";
            clearBtn.UseVisualStyleBackColor = true;
            clearBtn.Click += ButtonClear;
            // 
            // segmentRB
            // 
            segmentRB.AutoSize = true;
            segmentRB.Location = new Point(13, 103);
            segmentRB.Margin = new Padding(6, 7, 6, 7);
            segmentRB.Name = "segmentRB";
            segmentRB.Size = new Size(139, 39);
            segmentRB.TabIndex = 3;
            segmentRB.TabStop = true;
            segmentRB.Text = "Отрезок";
            segmentRB.UseVisualStyleBackColor = true;
            // 
            // pointRB
            // 
            pointRB.AutoSize = true;
            pointRB.Location = new Point(13, 47);
            pointRB.Margin = new Padding(6, 7, 6, 7);
            pointRB.Name = "pointRB";
            pointRB.Size = new Size(115, 39);
            pointRB.TabIndex = 3;
            pointRB.TabStop = true;
            pointRB.Text = "Точка";
            pointRB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(biasBtn);
            groupBox2.Controls.Add(biasYNumUD);
            groupBox2.Controls.Add(biasXNumUD);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(9, 347);
            groupBox2.Margin = new Padding(6, 7, 6, 7);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(6, 7, 6, 7);
            groupBox2.Size = new Size(312, 275);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Смещение";
            // 
            // biasBtn
            // 
            biasBtn.Location = new Point(50, 164);
            biasBtn.Margin = new Padding(6, 7, 6, 7);
            biasBtn.Name = "biasBtn";
            biasBtn.Size = new Size(171, 60);
            biasBtn.TabIndex = 5;
            biasBtn.Text = "Применить";
            biasBtn.UseVisualStyleBackColor = true;
            biasBtn.Click += BiasBtn_Click;
            // 
            // biasYNumUD
            // 
            biasYNumUD.Location = new Point(63, 111);
            biasYNumUD.Margin = new Padding(6, 7, 6, 7);
            biasYNumUD.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            biasYNumUD.Name = "biasYNumUD";
            biasYNumUD.Size = new Size(100, 41);
            biasYNumUD.TabIndex = 2;
            // 
            // biasXNumUD
            // 
            biasXNumUD.Location = new Point(63, 52);
            biasXNumUD.Margin = new Padding(6, 7, 6, 7);
            biasXNumUD.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            biasXNumUD.Name = "biasXNumUD";
            biasXNumUD.Size = new Size(100, 41);
            biasXNumUD.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 116);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(28, 35);
            label5.TabIndex = 0;
            label5.Text = "y";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 52);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(29, 35);
            label4.TabIndex = 0;
            label4.Text = "x";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(scaleAroundPointCB);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(scaleBtn);
            groupBox3.Controls.Add(scaleYNumUD);
            groupBox3.Controls.Add(scaleXNumUD);
            groupBox3.Controls.Add(label8);
            groupBox3.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(9, 686);
            groupBox3.Margin = new Padding(6, 7, 6, 7);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(6, 7, 6, 7);
            groupBox3.Size = new Size(312, 324);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Масштабирование(%)";
            // 
            // scaleAroundPointCB
            // 
            scaleAroundPointCB.AutoSize = true;
            scaleAroundPointCB.Location = new Point(54, 138);
            scaleAroundPointCB.Margin = new Padding(6, 7, 6, 7);
            scaleAroundPointCB.Name = "scaleAroundPointCB";
            scaleAroundPointCB.Size = new Size(200, 39);
            scaleAroundPointCB.TabIndex = 6;
            scaleAroundPointCB.Text = "Вокруг точки";
            scaleAroundPointCB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(167, 62);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(28, 35);
            label2.TabIndex = 0;
            label2.Text = "y";
            // 
            // scaleBtn
            // 
            scaleBtn.Location = new Point(50, 198);
            scaleBtn.Margin = new Padding(6, 7, 6, 7);
            scaleBtn.Name = "scaleBtn";
            scaleBtn.Size = new Size(217, 65);
            scaleBtn.TabIndex = 5;
            scaleBtn.Text = "Применить";
            scaleBtn.UseVisualStyleBackColor = true;
            scaleBtn.Click += ScaleBtn_Click;
            // 
            // scaleYNumUD
            // 
            scaleYNumUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            scaleYNumUD.Location = new Point(197, 57);
            scaleYNumUD.Margin = new Padding(6, 7, 6, 7);
            scaleYNumUD.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            scaleYNumUD.Name = "scaleYNumUD";
            scaleYNumUD.Size = new Size(100, 41);
            scaleYNumUD.TabIndex = 2;
            scaleYNumUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // scaleXNumUD
            // 
            scaleXNumUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            scaleXNumUD.Location = new Point(54, 57);
            scaleXNumUD.Margin = new Padding(6, 7, 6, 7);
            scaleXNumUD.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            scaleXNumUD.Name = "scaleXNumUD";
            scaleXNumUD.Size = new Size(100, 41);
            scaleXNumUD.TabIndex = 2;
            scaleXNumUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 62);
            label8.Margin = new Padding(6, 0, 6, 0);
            label8.Name = "label8";
            label8.Size = new Size(29, 35);
            label8.TabIndex = 0;
            label8.Text = "x";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(intersectionPointLabel);
            groupBox6.Controls.Add(posRelativeToPolygonLabel);
            groupBox6.Controls.Add(posRelativeToSegmentLabel);
            groupBox6.Location = new Point(731, 11);
            groupBox6.Margin = new Padding(6, 7, 6, 7);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(6, 7, 6, 7);
            groupBox6.Size = new Size(1128, 310);
            groupBox6.TabIndex = 8;
            groupBox6.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(20, 230);
            label7.Name = "label7";
            label7.Size = new Size(422, 35);
            label7.TabIndex = 32;
            label7.Text = "Принадлежит ли точка полигону:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(20, 131);
            label6.Name = "label6";
            label6.Size = new Size(469, 35);
            label6.TabIndex = 31;
            label6.Text = "Положение точки относительно ребра";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(20, 39);
            label3.Name = "label3";
            label3.Size = new Size(240, 35);
            label3.TabIndex = 30;
            label3.Text = "Точка пересечения";
            // 
            // intersectionPointLabel
            // 
            intersectionPointLabel.AutoSize = true;
            intersectionPointLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            intersectionPointLabel.Location = new Point(517, 41);
            intersectionPointLabel.Margin = new Padding(6, 0, 6, 0);
            intersectionPointLabel.Name = "intersectionPointLabel";
            intersectionPointLabel.Size = new Size(30, 35);
            intersectionPointLabel.TabIndex = 9;
            intersectionPointLabel.Text = "-";
            // 
            // posRelativeToPolygonLabel
            // 
            posRelativeToPolygonLabel.AutoSize = true;
            posRelativeToPolygonLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            posRelativeToPolygonLabel.Location = new Point(517, 230);
            posRelativeToPolygonLabel.Margin = new Padding(6, 0, 6, 0);
            posRelativeToPolygonLabel.Name = "posRelativeToPolygonLabel";
            posRelativeToPolygonLabel.Size = new Size(30, 35);
            posRelativeToPolygonLabel.TabIndex = 24;
            posRelativeToPolygonLabel.Text = "-";
            // 
            // posRelativeToSegmentLabel
            // 
            posRelativeToSegmentLabel.AutoSize = true;
            posRelativeToSegmentLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            posRelativeToSegmentLabel.Location = new Point(517, 131);
            posRelativeToSegmentLabel.Margin = new Padding(6, 0, 6, 0);
            posRelativeToSegmentLabel.Name = "posRelativeToSegmentLabel";
            posRelativeToSegmentLabel.Size = new Size(30, 35);
            posRelativeToSegmentLabel.TabIndex = 29;
            posRelativeToSegmentLabel.Text = "-";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rotationAroundPointCB);
            groupBox5.Controls.Add(angle90Btn);
            groupBox5.Controls.Add(rotationBtn);
            groupBox5.Controls.Add(angleNumUD);
            groupBox5.Controls.Add(label1);
            groupBox5.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox5.Location = new Point(395, 0);
            groupBox5.Margin = new Padding(6, 7, 6, 7);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(6, 7, 6, 7);
            groupBox5.Size = new Size(324, 310);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "Поворот";
            // 
            // rotationAroundPointCB
            // 
            rotationAroundPointCB.AutoSize = true;
            rotationAroundPointCB.Location = new Point(52, 138);
            rotationAroundPointCB.Margin = new Padding(6, 7, 6, 7);
            rotationAroundPointCB.Name = "rotationAroundPointCB";
            rotationAroundPointCB.Size = new Size(200, 39);
            rotationAroundPointCB.TabIndex = 6;
            rotationAroundPointCB.Text = "Вокруг точки";
            rotationAroundPointCB.UseVisualStyleBackColor = true;
            // 
            // angle90Btn
            // 
            angle90Btn.Location = new Point(228, 57);
            angle90Btn.Margin = new Padding(6, 7, 6, 7);
            angle90Btn.Name = "angle90Btn";
            angle90Btn.Size = new Size(67, 41);
            angle90Btn.TabIndex = 1;
            angle90Btn.Text = "90";
            angle90Btn.UseVisualStyleBackColor = true;
            angle90Btn.Click += Angle90_Click;
            // 
            // rotationBtn
            // 
            rotationBtn.Location = new Point(52, 212);
            rotationBtn.Margin = new Padding(6, 7, 6, 7);
            rotationBtn.Name = "rotationBtn";
            rotationBtn.Size = new Size(217, 64);
            rotationBtn.TabIndex = 5;
            rotationBtn.Text = "Применить";
            rotationBtn.UseVisualStyleBackColor = true;
            rotationBtn.Click += RotationBtn_Click;
            // 
            // angleNumUD
            // 
            angleNumUD.Location = new Point(95, 57);
            angleNumUD.Margin = new Padding(6, 7, 6, 7);
            angleNumUD.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            angleNumUD.Name = "angleNumUD";
            angleNumUD.Size = new Size(106, 41);
            angleNumUD.TabIndex = 0;
            angleNumUD.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 62);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 35);
            label1.TabIndex = 0;
            label1.Text = "Угол";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1874, 1229);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Margin = new Padding(6, 7, 6, 7);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Афинные преобразования";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)biasYNumUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)biasXNumUD).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scaleYNumUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)scaleXNumUD).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)angleNumUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private RadioButton polygonRB;
        private RadioButton segmentRB;
        private RadioButton pointRB;
        private Button clearBtn;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private NumericUpDown biasYNumUD;
        private NumericUpDown biasXNumUD;
        private Label label5;
        private Label label4;
        private NumericUpDown scaleYNumUD;
        private NumericUpDown scaleXNumUD;
        private Label label8;
        private Button biasBtn;
        private Button scaleBtn;
        private CheckBox scaleAroundPointCB;
        private Label label2;
        private GroupBox groupBox6;
        private Label posRelativeToPolygonLabel;
        private Label posRelativeToSegmentLabel;
        private Label intersectionPointLabel;
        private GroupBox groupBox5;
        private CheckBox rotationAroundPointCB;
        private Button angle90Btn;
        private Button rotationBtn;
        private NumericUpDown angleNumUD;
        private Label label1;
        private Label label7;
        private Label label6;
        private Label label3;
    }
}