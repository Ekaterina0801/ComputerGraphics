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
using System.Windows.Forms;
using System.Drawing;

using System;
using System.Drawing;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form3 : Form
    {
        private Bitmap originalImage;
        private Bitmap modifiedImage;

        public Form3()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                pbOriginal.Image = originalImage;
                modifiedImage = new Bitmap(originalImage.Width, originalImage.Height);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    for (int y = 0; y < originalImage.Height; y++)
                    {
                        Color originalColor = originalImage.GetPixel(x, y);
                        Color modifiedColor = ColorToHSV(originalColor);
                        modifiedImage.SetPixel(x, y, modifiedColor);
                    }
                }

                pbOriginal.Image = modifiedImage;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (modifiedImage != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    modifiedImage.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        private Color ColorToHSV(Color originalColor)
        {
            float hue, saturation, value;
            hue = originalColor.GetHue();
            saturation = originalColor.GetSaturation();
            value = originalColor.GetBrightness();

            hue += (float)numHue.Value;
            saturation += (float)numSaturation.Value / 100;
            value += (float)numValue.Value / 100;

            hue = Math.Max(0, Math.Min(360, hue));
            saturation = Math.Max(0, Math.Min(1, saturation));
            value = Math.Max(0, Math.Min(1, value));

            return ColorFromHSV(hue, saturation, value);
        }

        private Color ColorFromHSV(float hue, float saturation, float value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            float f = hue / 60 - (float)Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}