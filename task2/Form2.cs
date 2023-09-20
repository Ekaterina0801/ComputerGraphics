using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image file
                Bitmap image = new Bitmap(openFileDialog.FileName);

                // Display the original image
                originalPictureBox.Image = image;

                // Create separate images for each color channel
                Bitmap redImage = new Bitmap(image.Width, image.Height);
                Bitmap greenImage = new Bitmap(image.Width, image.Height);
                Bitmap blueImage = new Bitmap(image.Width, image.Height);

                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color pixelColor = image.GetPixel(x, y);

                        // Get the red component of the pixel color
                        int red = pixelColor.R;
                        // Set the pixel color in the red image
                        redImage.SetPixel(x, y, Color.FromArgb(red, 0, 0));

                        // Get the green component of the pixel color
                        int green = pixelColor.G;
                        // Set the pixel color in the green image
                        greenImage.SetPixel(x, y, Color.FromArgb(0, green, 0));

                        // Get the blue component of the pixel color
                        int blue = pixelColor.B;
                        // Set the pixel color in the blue image
                        blueImage.SetPixel(x, y, Color.FromArgb(0, 0, blue));
                    }
                }

                // Display the separate color channel images
                redPictureBox.Image = redImage;
                greenPictureBox.Image = greenImage;
                bluePictureBox.Image = blueImage;

                // Build histograms for each color channel
                int[] redHistogram = new int[256];
                int[] greenHistogram = new int[256];
                int[] blueHistogram = new int[256];

                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color pixelColor = image.GetPixel(x, y);

                        // Increment the respective color channel histogram
                        redHistogram[pixelColor.R]++;
                        greenHistogram[pixelColor.G]++;
                        blueHistogram[pixelColor.B]++;
                    }
                }

                // Display the histograms using a chart control
                chart.Series.Clear();
                chart.ChartAreas[0].AxisX.Title = "Color Intensity";
                chart.ChartAreas[0].AxisY.Title = "Frequency";

                Series redSeries = new Series("Red");
                redSeries.ChartType = SeriesChartType.Column;
                for (int i = 0; i < 256; i++)
                {
                    redSeries.Points.AddXY(i, redHistogram[i]);
                }
                chart.Series.Add(redSeries);

                Series greenSeries = new Series("Green");
                greenSeries.ChartType = SeriesChartType.Column;
                for (int i = 0; i < 256; i++)
                {
                    greenSeries.Points.AddXY(i, greenHistogram[i]);
                }
                chart.Series.Add(greenSeries);

                Series blueSeries = new Series("Blue");
                blueSeries.ChartType = SeriesChartType.Column;
                for (int i = 0; i < 256; i++)
                {
                    blueSeries.Points.AddXY(i, blueHistogram[i]);
                }
                chart.Series.Add(blueSeries);
            }
        }
    }
}