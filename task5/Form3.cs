using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form3 : Form
    {
        private List<Point> controlPoints;
        private List<Point> curvePoints;
        private int selectedPointIndex;
        private bool isMovingPoint;
        private Point lastMouseLocation;

        public Form3()
        {
            InitializeComponent();
            controlPoints = new List<Point>();
            curvePoints = new List<Point>();
            selectedPointIndex = -1;
            isMovingPoint = false;
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Отрисовка опорных точек
            foreach (Point point in controlPoints)
            {
                e.Graphics.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 6, 6);
            }

            // Отрисовка кривой
            if (curvePoints.Count > 1)
            {
                e.Graphics.DrawLines(Pens.Red, curvePoints.ToArray());
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            // Поиск ближайшей опорной точки
            for (int i = 0; i < controlPoints.Count; i++)
            {
                int distanceX = Math.Abs(e.X - controlPoints[i].X);
                int distanceY = Math.Abs(e.Y - controlPoints[i].Y);
                if (distanceX <= 3 && distanceY <= 3)
                {
                    selectedPointIndex = i;
                    break;
                }
            }

            // Добавление новой опорной точки
            if (selectedPointIndex == -1)
            {
                controlPoints.Add(new Point(e.X, e.Y));
                curvePoints = CalculateBezierCurve(controlPoints, 100);
                Invalidate();
                return;
            }

            // Удаление опорной точки
            if (e.Button == MouseButtons.Right)
            {
                controlPoints.RemoveAt(selectedPointIndex);
                selectedPointIndex = -1;
                curvePoints = CalculateBezierCurve(controlPoints, 100);
                Invalidate();
            }
            else
            {
                isMovingPoint = true;
                lastMouseLocation = e.Location;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedPointIndex != -1 && isMovingPoint)
            {
                Point delta = new Point(e.X - lastMouseLocation.X, e.Y - lastMouseLocation.Y);
                controlPoints[selectedPointIndex] = new Point(controlPoints[selectedPointIndex].X + delta.X, controlPoints[selectedPointIndex].Y + delta.Y);

                curvePoints = CalculateBezierCurve(controlPoints, 100);
                lastMouseLocation = e.Location;

                Invalidate();
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isMovingPoint = false;
        }

        private List<Point> CalculateBezierCurve(List<Point> points, int resolution)
        {
            List<Point> curve = new List<Point>();

            for (double t = 0; t <= 1; t += 1.0 / resolution)
            {
                Point point = CalculateBezierPoint(t, points);
                curve.Add(point);
            }

            return curve;
        }

        private Point CalculateBezierPoint(double t, List<Point> points)
        {
            int n = points.Count - 1;
            double x = 0;
            double y = 0;
            for (int i = 0; i <= n; i++)
            {
                
                // Расчет биномиального коэффициента по формуле Триангля Паскаля
                var k = i;
                if (k > n - k)
                {
                    k = n - k;
                }

                int c = 1;
                for (int j = 0; j < k; ++j)
                {
                    c *= (n - j);
                    c /= (j + 1);
                }
                double coefficient = c * Math.Pow(1 - t, n - i) * Math.Pow(t, i);
                x += coefficient * points[i].X;
                y += coefficient * points[i].Y;
            }
            return new Point((int)x, (int)y);
        }


    }
}