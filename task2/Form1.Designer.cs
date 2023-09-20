namespace lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            openButton = new Button();
            originalPictureBox = new PictureBox();
            grayscalePictureBox1 = new PictureBox();
            grayscalePictureBox2 = new PictureBox();
            intensityChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pictureBoxDiff = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)intensityChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDiff).BeginInit();
            SuspendLayout();
            // 
            // openButton
            // 
            openButton.Location = new Point(488, 893);
            openButton.Name = "openButton";
            openButton.Size = new Size(293, 62);
            openButton.TabIndex = 0;
            openButton.Text = "Открыть фото";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // originalPictureBox
            // 
            originalPictureBox.Location = new Point(45, 53);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(351, 270);
            originalPictureBox.TabIndex = 1;
            originalPictureBox.TabStop = false;
            // 
            // grayscalePictureBox1
            // 
            grayscalePictureBox1.Location = new Point(464, 53);
            grayscalePictureBox1.Name = "grayscalePictureBox1";
            grayscalePictureBox1.Size = new Size(351, 270);
            grayscalePictureBox1.TabIndex = 2;
            grayscalePictureBox1.TabStop = false;
            // 
            // grayscalePictureBox2
            // 
            grayscalePictureBox2.Location = new Point(874, 53);
            grayscalePictureBox2.Name = "grayscalePictureBox2";
            grayscalePictureBox2.Size = new Size(351, 270);
            grayscalePictureBox2.TabIndex = 3;
            grayscalePictureBox2.TabStop = false;
            // 
            // intensityChart
            // 
            chartArea1.Name = "ChartArea1";
            intensityChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            intensityChart.Legends.Add(legend1);
            intensityChart.Location = new Point(275, 403);
            intensityChart.Name = "intensityChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            intensityChart.Series.Add(series1);
            intensityChart.Series.Add(series2);
            intensityChart.Size = new Size(703, 377);
            intensityChart.TabIndex = 4;
            intensityChart.Text = "chart1";
            // 
            // pictureBoxDiff
            // 
            pictureBoxDiff.Location = new Point(1323, 56);
            pictureBoxDiff.Name = "pictureBoxDiff";
            pictureBoxDiff.Size = new Size(365, 267);
            pictureBoxDiff.TabIndex = 5;
            pictureBoxDiff.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1776, 981);
            Controls.Add(pictureBoxDiff);
            Controls.Add(intensityChart);
            Controls.Add(grayscalePictureBox2);
            Controls.Add(grayscalePictureBox1);
            Controls.Add(originalPictureBox);
            Controls.Add(openButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)intensityChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDiff).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button openButton;
        private PictureBox originalPictureBox;
        private PictureBox grayscalePictureBox1;
        private PictureBox grayscalePictureBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart intensityChart;
        private PictureBox pictureBoxDiff;
    }
}