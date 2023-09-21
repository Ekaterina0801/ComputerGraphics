namespace lab2
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            originalPictureBox = new PictureBox();
            redPictureBox = new PictureBox();
            greenPictureBox = new PictureBox();
            bluePictureBox = new PictureBox();
            openButton = new Button();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            comboBox1 = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)redPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)greenPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bluePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            SuspendLayout();
            // 
            // originalPictureBox
            // 
            originalPictureBox.Location = new Point(7, 26);
            originalPictureBox.Margin = new Padding(2);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(255, 204);
            originalPictureBox.TabIndex = 0;
            originalPictureBox.TabStop = false;
            // 
            // redPictureBox
            // 
            redPictureBox.Location = new Point(292, 26);
            redPictureBox.Margin = new Padding(2);
            redPictureBox.Name = "redPictureBox";
            redPictureBox.Size = new Size(255, 204);
            redPictureBox.TabIndex = 1;
            redPictureBox.TabStop = false;
            // 
            // greenPictureBox
            // 
            greenPictureBox.Location = new Point(585, 26);
            greenPictureBox.Margin = new Padding(2);
            greenPictureBox.Name = "greenPictureBox";
            greenPictureBox.Size = new Size(255, 204);
            greenPictureBox.TabIndex = 2;
            greenPictureBox.TabStop = false;
            // 
            // bluePictureBox
            // 
            bluePictureBox.Location = new Point(877, 26);
            bluePictureBox.Margin = new Padding(2);
            bluePictureBox.Name = "bluePictureBox";
            bluePictureBox.Size = new Size(255, 204);
            bluePictureBox.TabIndex = 3;
            bluePictureBox.TabStop = false;
            // 
            // openButton
            // 
            openButton.Location = new Point(460, 565);
            openButton.Margin = new Padding(2);
            openButton.Name = "openButton";
            openButton.Size = new Size(264, 46);
            openButton.TabIndex = 4;
            openButton.Text = "Открыть изображение";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart.Legends.Add(legend2);
            chart.Location = new Point(286, 268);
            chart.Margin = new Padding(2);
            chart.Name = "chart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart.Series.Add(series2);
            chart.Size = new Size(642, 280);
            chart.TabIndex = 5;
            chart.Text = "chart1";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Red", "Green", "Blue" });
            comboBox1.Location = new Point(1021, 335);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            comboBox1.Text = "Red";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(1012, 397);
            button1.Name = "button1";
            button1.Size = new Size(160, 88);
            button1.TabIndex = 7;
            button1.Text = "Нарисовать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1256, 630);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(chart);
            Controls.Add(openButton);
            Controls.Add(bluePictureBox);
            Controls.Add(greenPictureBox);
            Controls.Add(redPictureBox);
            Controls.Add(originalPictureBox);
            Margin = new Padding(2);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)redPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)greenPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)bluePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox originalPictureBox;
        private PictureBox redPictureBox;
        private PictureBox greenPictureBox;
        private PictureBox bluePictureBox;
        private Button openButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private ComboBox comboBox1;
        private Button button1;
    }
}