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
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)redPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)greenPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bluePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            SuspendLayout();
            // 
            // originalPictureBox
            // 
            originalPictureBox.Location = new Point(12, 42);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(415, 327);
            originalPictureBox.TabIndex = 0;
            originalPictureBox.TabStop = false;
            // 
            // redPictureBox
            // 
            redPictureBox.Location = new Point(475, 42);
            redPictureBox.Name = "redPictureBox";
            redPictureBox.Size = new Size(415, 327);
            redPictureBox.TabIndex = 1;
            redPictureBox.TabStop = false;
            // 
            // greenPictureBox
            // 
            greenPictureBox.Location = new Point(951, 42);
            greenPictureBox.Name = "greenPictureBox";
            greenPictureBox.Size = new Size(415, 327);
            greenPictureBox.TabIndex = 2;
            greenPictureBox.TabStop = false;
            // 
            // bluePictureBox
            // 
            bluePictureBox.Location = new Point(1425, 42);
            bluePictureBox.Name = "bluePictureBox";
            bluePictureBox.Size = new Size(415, 327);
            bluePictureBox.TabIndex = 3;
            bluePictureBox.TabStop = false;
            // 
            // openButton
            // 
            openButton.Location = new Point(748, 904);
            openButton.Name = "openButton";
            openButton.Size = new Size(429, 74);
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
            chart.Location = new Point(465, 428);
            chart.Name = "chart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart.Series.Add(series2);
            chart.Size = new Size(1044, 448);
            chart.TabIndex = 5;
            chart.Text = "chart1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1917, 1008);
            Controls.Add(chart);
            Controls.Add(openButton);
            Controls.Add(bluePictureBox);
            Controls.Add(greenPictureBox);
            Controls.Add(redPictureBox);
            Controls.Add(originalPictureBox);
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
    }
}