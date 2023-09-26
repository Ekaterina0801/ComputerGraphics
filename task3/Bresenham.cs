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

namespace lab3
{
    public partial class Bresenham : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private bool drawingInProgress;
        private Point startPoint;
        private Point endPoint;

        public Bresenham()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox.Image = bitmap;
            drawingInProgress = false;
        }

        private void DrawLineBresenham(Point start, Point end)
        {
            int dx = Math.Abs(end.X - start.X);
            int dy = Math.Abs(end.Y - start.Y);
            int sx = (start.X < end.X) ? 1 : -1;
            int sy = (start.Y < end.Y) ? 1 : -1;
            int error = dx - dy;

            while (true)
            {
                bitmap.SetPixel(start.X, start.Y, Color.Black);

                if (start == end)
                    break;

                int e2 = 2 * error;
                if (e2 > -dy)
                {
                    error -= dy;
                    start.X += sx;
                }
                if (e2 < dx)
                {
                    error += dx;
                    start.Y += sy;
                }
            }

            pictureBox.Image = bitmap;
        }

        private void DrawLineDDA(Point start, Point end)
        {
            int dx = end.X - start.X;
            int dy = end.Y - start.Y;
            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
            float xIncrement = (float)dx / (float)steps;
            float yIncrement = (float)dy / (float)steps;
            float x = start.X;
            float y = start.Y;

            for (int i = 0; i <= steps; i++)
            {
                bitmap.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.Black);
                x += xIncrement;
                y += yIncrement;
            }

            pictureBox.Image = bitmap;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawingInProgress)
                return;

            startPoint = e.Location;
            drawingInProgress = true;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawingInProgress)
                return;

            endPoint = e.Location;
            bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;

            DrawLineBresenham(startPoint, endPoint);
            DrawLineDDA(startPoint, endPoint);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!drawingInProgress)
                return;

            drawingInProgress = false;
            endPoint = e.Location;
            bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;

            DrawLineBresenham(startPoint, endPoint);
            //DrawLineDDA(startPoint, endPoint);
        }
    }
}
