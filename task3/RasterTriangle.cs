using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab3
{
    public partial class RasterTriangle : Form
    {
        private Bitmap bitmap;
        private Point[] triangleVertices = new Point[3];
        int selected = 0;

        public RasterTriangle()
        {
            InitializeComponent();
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Создаем треугольник со случайными вершинами и цветами
            if (triangleVertices[2] != Point.Empty)
            {

                Color[] triangleColors = GenerateTriangleColors();

                // Получаем границы треугольника
                int minX = Math.Min(Math.Min(triangleVertices[0].X, triangleVertices[1].X), triangleVertices[2].X);
                int minY = Math.Min(Math.Min(triangleVertices[0].Y, triangleVertices[1].Y), triangleVertices[2].Y);
                int maxX = Math.Max(Math.Max(triangleVertices[0].X, triangleVertices[1].X), triangleVertices[2].X);
                int maxY = Math.Max(Math.Max(triangleVertices[0].Y, triangleVertices[1].Y), triangleVertices[2].Y);

                // Рисуем треугольник
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (IsPointInTriangle(x, y, triangleVertices))
                            {
                                float barycentricU, barycentricV, barycentricW;
                                CalculateBarycentricCoordinates(x, y, triangleVertices, out barycentricU, out barycentricV, out barycentricW);

                                Color interpolatedColor = InterpolateColor(triangleColors[0], triangleColors[1], triangleColors[2], barycentricU, barycentricV, barycentricW);

                                bitmap.SetPixel(x, y, interpolatedColor);
                            }
                        }
                    }
                }

                // Отрисовываем треугольник в окне
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }

        // Генерация случайных вершин треугольника
        private Point[] GenerateTriangleVertices(int width, int height)
        {
            Random random = new Random();

            Point[] vertices = new Point[3];
            vertices[0] = new Point(random.Next(width), random.Next(height));
            vertices[1] = new Point(random.Next(width), random.Next(height));
            vertices[2] = new Point(random.Next(width), random.Next(height));

            return vertices;
        }

        // Генерация случайных цветов вершин
        private Color[] GenerateTriangleColors()
        {
            Random random = new Random();

            Color[] colors = new Color[3];
            colors[0] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            colors[1] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            colors[2] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            return colors;
        }
       
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
              
                if (selected == 3)
                {
                    selected = 0;
                    triangleVertices[0] = triangleVertices[1] = triangleVertices[2] = Point.Empty;
                    bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

                }
                triangleVertices[selected] = new Point(e.X, e.Y);
                selected += 1;

                // Перерисовываем треугольник
                Refresh();
            }
        }

        // Проверка, находится ли точка внутри треугольника
        private bool IsPointInTriangle(int x, int y, Point[] triangleVertices)
        {
            Point p1 = triangleVertices[0];
            Point p2 = triangleVertices[1];
            Point p3 = triangleVertices[2];

            float d1 = Sign(x, y, p1.X, p1.Y, p2.X, p2.Y);
            float d2 = Sign(x, y, p2.X, p2.Y, p3.X, p3.Y);
            float d3 = Sign(x, y, p3.X, p3.Y, p1.X, p1.Y);

            bool hasNegative = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPositive = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNegative && hasPositive);
        }

        private float Sign(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            return (x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3);
        }

        // Вычисление барицентрических координат точки в треугольнике
        private void CalculateBarycentricCoordinates(int x, int y, Point[] triangleVertices, out float u, out float v, out float w)
        {
            Point p0 = triangleVertices[0];
            Point p1 = triangleVertices[1];
            Point p2 = triangleVertices[2];

            float denom = (p1.Y - p2.Y) * (p0.X - p2.X) + (p2.X - p1.X) * (p0.Y - p2.Y);

            u = ((p1.Y - p2.Y) * (x - p2.X) + (p2.X - p1.X) * (y - p2.Y)) / denom;
            v = ((p2.Y - p0.Y) * (x - p2.X) + (p0.X - p2.X) * (y - p2.Y)) / denom;
            w = 1 - u - v;
        }

        // Интерполяция цвета с помощью барицентрических координат
        private Color InterpolateColor(Color color0, Color color1, Color color2, float u, float v, float w)
        {
            byte r = (byte)(color0.R * u + color1.R * v + color2.R * w);
            byte g = (byte)(color0.G * u + color1.G * v + color2.G * w);
            byte b = (byte)(color0.B * u + color1.B * v + color2.B * w);

            return Color.FromArgb(r, g, b);
        }
    }
}