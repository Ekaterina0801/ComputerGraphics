using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Point pointLocation, startPoint, endPoint, minPolygonCoord, maxPolygonCoord;
        // список отрезков
        List<Segment> segments = new List<Segment>();
        // полигон
        List<Point> polygon = new List<Point>();
        // Галочку поставили на отрезок или полигон(не на точку)
        bool isChecked = false;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
        }

        // MouseDown возникает, когда пользователь
        // нажимает кнопку мыши на элементе управления
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Если поставлены галочки(нарисовать отрезок или полигон)
            isChecked = segmentRB.Checked || polygonRB.Checked ? true : false;
            // Для отрезка 
            startPoint = segmentRB.Checked ? e.Location : startPoint;
            // Для полигона
            if (polygonRB.Checked && polygon.Count == 0)
            {
                    startPoint = e.Location;
                    polygon.Add(startPoint);
                    minPolygonCoord = e.Location;
                    maxPolygonCoord = e.Location;
 
            }
        }
        // Перемещение мыши для черчения отрезка или полигона
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            endPoint = isChecked ? e.Location : endPoint;
            pictureBox1.Invalidate();
        }

        // происходит, когда кнопка мыши отпускается (поднимается) после нажатия.
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pointLocation = pointRB.Checked ? e.Location : pointLocation;
            if (segmentRB.Checked)
            {
                isChecked = false;
                //при мгновенном отпускании(start = end)
                if (endPoint == Point.Empty)
                    return;
                // Добавить новый отрезок
                segments.Add(new Segment(startPoint, endPoint));
                startPoint = Point.Empty;
                endPoint = Point.Empty;

            }
            if (polygonRB.Checked)
            {
                isChecked = false;
                //при мгновенном отпускании(start = end)
                if (endPoint == Point.Empty)
                    return;
                polygon.Add(endPoint);
                minPolygonCoord.X = Math.Min(minPolygonCoord.X, endPoint.X);
                minPolygonCoord.Y = Math.Min(minPolygonCoord.Y, endPoint.Y);
                maxPolygonCoord.X= Math.Max(maxPolygonCoord.X, endPoint.X);
                maxPolygonCoord.Y = Math.Max(maxPolygonCoord.Y, endPoint.Y);
                startPoint = endPoint;
                endPoint = Point.Empty;
            }
            // перерисовка 
            pictureBox1.Invalidate();
        }

        private void ButtonClear(object sender, EventArgs e)
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            segments.Clear();
            polygon.Clear();
            pointLocation = Point.Empty;
            pictureBox1.Invalidate();
        }
        // смещение на dx, dy
        private void transferCoordinates(double dx, double dy)
        {
            //матрица переноса из лекции 4 слайд 15
            Matrix M = new Matrix(3, 3);
            Matrix v = new Matrix(1, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                    {
                        M[i, j] = 1;
                        continue;
                    }
                    if (i == 2)
                    {
                        M[i, j] = j == 0 ? dx : dy;
                        continue;
                    }
                    M[i, j] = 0;
                }
            }
            if (polygonRB.Checked)
            {
                for (int i = 0; i < polygon.Count; ++i)
                {
                    v[0, 0] = polygon[i].X;
                    v[0, 1] = polygon[i].Y;
                    v[0, 2] = 1;
                    v *= M; // слайд 17 (x,y,1)*M
                    polygon[i] = new Point((int)v[0, 0], (int)v[0, 1]);
                }
            }
            if (segmentRB.Checked)
            {
                for (int i = 0; i < segments.Count; ++i)
                {
                    // (x,y,1)
                    v[0, 0] = segments[i].startP.X;
                    v[0, 1] = segments[i].startP.Y;
                    v[0, 2] = 1; 
                    v *= M; // слайд 17 (x,y,1)*M
                    Point p = new Point((int)v[0, 0], (int)v[0, 1]);
                    v[0, 0] = segments[i].endP.X;
                    v[0, 1] = segments[i].endP.Y;
                    v[0, 2] = 1;
                    v *= M;
                    segments[i] = new Segment(p, new Point((int)v[0, 0], (int)v[0, 1]));
                }
            }
        }

        private void BiasBtn_Click(object sender, EventArgs e)
        {
            transferCoordinates((int)biasXNumUD.Value, (int)biasYNumUD.Value);
            pictureBox1.Invalidate();
        }


        private void ScaleBtn_Click(object sender, EventArgs e)
        {
            double x = (double)scaleXNumUD.Value / 100;
            double y = (double)scaleYNumUD.Value / 100;
            //матрица сжатия/растяжения
            Matrix M = new Matrix(3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                    {
                        switch (i)
                        {
                            case 0:
                                M[i, j] = x; break;
                            case 1:
                                M[i, j] = y; break;
                            case 2:
                                M[i, j] = 1; break;
                            default:
                                break;
                        }
                    }
                    else M[i, j] = 0;
                }
             }
            rotateOrScale(M, true);
            pictureBox1.Invalidate();
        }
        private void rotateOrScale(Matrix M, Boolean action)
        {
            CheckBox aroundPointCB;
            if (action)
                aroundPointCB = scaleAroundPointCB;
            else
                aroundPointCB = rotationAroundPointCB;

            if (segmentRB.Checked)
            {
                for (int i = 0; i < segments.Count; ++i)
                {
                    //точка, относительно которой масштабировать
                    PointF translationPoint;

                    //окружение заданной точки
                    if (aroundPointCB.Checked)
                    {
                        if (pointLocation == Point.Empty)
                            return;
                        translationPoint = pointLocation;
                    }
                    //вокруг центра отрезка
                    else
                        translationPoint = new PointF((segments[i].startP.X + segments[i].endP.X) / 2,
                                                          (segments[i].startP.Y + segments[i].endP.Y) / 2);
                    //начало координат
                    transferCoordinates(-1 * translationPoint.X, -1 * translationPoint.Y);

                    //масштабирование
                    Matrix vec = new Matrix(1, 3);
                    vec[0, 0] = segments[i].startP.X;
                    vec[0, 1] = segments[i].startP.Y;
                    vec[0, 2] = 1;
                    vec *= M;
                    Point leftP = new Point((int)vec[0, 0], (int)vec[0, 1]);
                    vec[0, 0] = segments[i].endP.X;
                    vec[0, 1] = segments[i].endP.Y;
                    vec[0, 2] = 1;
                    vec *= M;
                    Point rightP = new Point((int)vec[0, 0], (int)vec[0, 1]);
                    segments[i] = new Segment(leftP, rightP);
                    //перенос обратно
                    transferCoordinates(translationPoint.X, translationPoint.Y);
                }
            }
            else if (polygonRB.Checked)
            {
                for (int i = 0; i < polygon.Count; ++i)
                {
                    //точка, относительно которой масштабировать
                    PointF translationPoint;
                    //вокруг заданной точки
                    if (aroundPointCB.Checked)
                    {
                        if (pointLocation == Point.Empty)
                            return;
                        translationPoint = pointLocation;
                    }
                    //вокруг центра полигона
                    else
                        translationPoint = new PointF((minPolygonCoord.X + maxPolygonCoord.X) / 2,
                                                      (minPolygonCoord.Y + maxPolygonCoord.Y) / 2);
                    //перенос в начало координат
                    transferCoordinates(-1 * translationPoint.X, -1 * translationPoint.Y);
                    //масштабирование
                    Matrix vec = new Matrix(1, 3);
                    vec[0, 0] = polygon[i].X;
                    vec[0, 1] = polygon[i].Y;
                    vec[0, 2] = 1;
                    vec *= M;
                    polygon[i] = new Point((int)vec[0, 0], (int)vec[0, 1]);
                    //перенос обратно
                    transferCoordinates(translationPoint.X, translationPoint.Y);
                }
            }
        }

        private void Angle90_Click(object sender, EventArgs e)
        {
            angleNumUD.Value = 90;
        }

        private void RotationBtn_Click(object sender, EventArgs e)
        {
            double angle = (double)angleNumUD.Value;
            //матрица сжатия/растяжения
            Matrix M = new Matrix(3, 3);
            M[0, 2] = M[1, 2] = M[2, 0] = M[2, 1] = 0;
            M[2, 2] = 1;
            M[0, 0] = Math.Cos(angle * Math.PI / 180);
            M[0, 1] = Math.Sin(angle * Math.PI / 180);
            M[1, 0] = -Math.Sin(angle * Math.PI / 180);
            M[1, 1] = Math.Cos(angle * Math.PI / 180);
            rotateOrScale(M, false);
            pictureBox1.Invalidate();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            if (segments.Count > 0)
            {
                foreach (Segment seg in segments)
                    g.DrawLine(Pens.Red, seg.startP, seg.endP);
            }

            if (polygon.Count > 1)
            {
                for (int i = 0; i < polygon.Count - 1; ++i)
                {
                    g.DrawLine(Pens.Red, polygon[i], polygon[i + 1]);
                }
                g.DrawLine(Pens.Red, polygon[0], polygon[polygon.Count - 1]);

            }
            //растягивание ребра
            if (startPoint != Point.Empty && endPoint != Point.Empty)
                g.DrawLine(Pens.Red, startPoint, endPoint);
            //точка
            g.DrawEllipse(Pens.Blue, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);
            g.FillEllipse(Brushes.Blue, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);

            updateText();
        }

        //обновление информации
        private void updateText()
        {
            if (segments.Count > 0)
            {
                int n = segments.Count - 1; //индекс последнего ребра
                int pos = findPoint(pointLocation, segments[n].startP, segments[n].endP);
                if (pos == 0)
                    posRelativeToSegmentLabel.Text = "Лежит на линии";
                else if (pos > 0)
                    posRelativeToSegmentLabel.Text = "Лежит слева от линии";
                else
                    posRelativeToSegmentLabel.Text = "Лежит справа от линии";
            }

            if (polygon.Count > 2)
            {
                if (isInPoligon(polygon, pointLocation))
                    posRelativeToPolygonLabel.Text = "Точка принадлежит полигону";
                else
                    posRelativeToPolygonLabel.Text = "Точка не принадлежит полигону";
            }

            if (segments.Count > 1)
            {
                PointF intersection = new PointF(-1, -1);
                int n = segments.Count - 1;
                intersection = pointOfIntersection(segments[n - 1].startP, segments[n - 1].endP, segments[n].startP, segments[n].endP);
                if (intersection.X == -1 && intersection.Y == -1)
                    intersectionPointLabel.Text = "Отрезки не пересекаются";
                else
                {
                    intersectionPointLabel.Text = " " + intersection.ToString();
                    g.DrawEllipse(Pens.Green, intersection.X - 2, intersection.Y - 2, 5, 5);
                    g.FillEllipse(Brushes.Green, intersection.X - 2, intersection.Y - 2, 5, 5);
                    pictureBox1.Invalidate();
                }
            }
        }

        // Классифицирует положение точки относительно ребра     
        int findPoint(PointF p, Point A, Point B)
        {
            return (int)((p.X - A.X) * (B.Y - A.Y) - (p.Y - A.Y) * (B.X - A.X));
        }

        //определяет принадлежит ли точка полигону
        bool isInPoligon(List<Point> polygon, Point p)
        {
            int n = polygon.Count;
            if (n < 3) return false;

            PointF extreme = new PointF(pictureBox1.Width, p.Y);

            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;
                PointF intersection = pointOfIntersection(polygon[i], polygon[next], p, extreme);
                if (intersection.X != -1)
                {
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                        return on_segment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            return count % 2 == 1;
        }

        //ориентация
        int orientation(PointF p, PointF q, PointF r)
        {
            float val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);
            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;
        }

        bool on_segment(PointF q, PointF p, PointF r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }

        //точка пересечения
        PointF pointOfIntersection(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            PointF i = new PointF(-1, -1);
            PointF s1 = new PointF();
            PointF s2 = new PointF();
            s1.X = p1.X - p0.X;
            s1.Y = p1.Y - p0.Y;
            s2.X = p3.X - p2.X;
            s2.Y = p3.Y - p2.Y;
            float s, t;
            s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                i.X = p0.X + (t * s1.X);
                i.Y = p0.Y + (t * s1.Y);
            }
            return i;
        }
    }
}