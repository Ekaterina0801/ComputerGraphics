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
        // √алочку поставили на отрезок или полигон(не на точку)
        bool isChecked = false;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
        }

        // MouseDown возникает, когда пользователь
        // нажимает кнопку мыши на элементе управлени€
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // ≈сли поставлены галочки(нарисовать отрезок или полигон)
            isChecked = segmentRB.Checked || polygonRB.Checked ? true : false;
            // ƒл€ отрезка 
            startPoint = segmentRB.Checked ? e.Location : startPoint;
            // ƒл€ полигона
            if (polygonRB.Checked && polygon.Count == 0)
            {
                startPoint = e.Location;
                polygon.Add(startPoint);
                minPolygonCoord = e.Location;
                maxPolygonCoord = e.Location;

            }
        }
        // ѕеремещение мыши дл€ черчени€ отрезка или полигона
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            endPoint = isChecked ? e.Location : endPoint;
            pictureBox1.Invalidate();
        }

        // происходит, когда кнопка мыши отпускаетс€ (поднимаетс€) после нажати€.
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pointLocation = pointRB.Checked ? e.Location : pointLocation;
            if (segmentRB.Checked)
            {
                isChecked = false;
                //при мгновенном отпускании(start = end)
                if (endPoint == Point.Empty)
                    return;
                // ƒобавить новый отрезок
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
                maxPolygonCoord.X = Math.Max(maxPolygonCoord.X, endPoint.X);
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
            posRelativeToSegmentLabel.Text = "-";
            posRelativeToPolygonLabel.Text = "-";
            intersectionPointLabel.Text = "-";
            pictureBox1.Invalidate();
        }
        // смещение на dx, dy
        private void Transform(float dx, float dy)
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
                    prodPolygon(M, i);
                }
            }
            if (segmentRB.Checked)
            {
                for (int i = 0; i < segments.Count; ++i)
                {
                    prodSegments(M, i);
                }
            }
        }



        //—жатие/раст€жение
        private void ScaleButton(object sender, EventArgs e)
        {      
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
                                M[i, j] = (float)scaleXNumUD.Value / 100; break;
                            case 1:
                                M[i, j] = (float)scaleYNumUD.Value / 100; break;
                            case 2:
                                M[i, j] = 1; break;
                            default:
                                break;
                        }
                    }
                    else M[i, j] = 0;
                }
            }
            ScaleRotate(M, true);
            pictureBox1.Invalidate();
        }

        private void prodSegments(Matrix M, int i)
        {
            Matrix vec = new Matrix(1, 3);
            // (x,y,1)
            vec[0, 0] = segments[i].startP.X;
            vec[0, 1] = segments[i].startP.Y;
            vec[0, 2] = 1;
            vec *= M;// слайд 17 (x,y,1)*M
            Point leftP = new Point((int)vec[0, 0], (int)vec[0, 1]);
            vec[0, 0] = segments[i].endP.X;
            vec[0, 1] = segments[i].endP.Y;
            vec[0, 2] = 1;
            vec *= M;
            Point rightP = new Point((int)vec[0, 0], (int)vec[0, 1]);
            segments[i] = new Segment(leftP, rightP);
        }

        private void prodPolygon(Matrix M, int i)
        {
            Matrix vec = new Matrix(1, 3);
            vec[0, 0] = polygon[i].X;
            vec[0, 1] = polygon[i].Y;
            vec[0, 2] = 1;
            vec *= M;// слайд 17 (x,y,1)*M
            polygon[i] = new Point((int)vec[0, 0], (int)vec[0, 1]);
        }
        private void MoveButton(object sender, EventArgs e)
        {
            Transform((int)biasXNumUD.Value, (int)biasYNumUD.Value);
            pictureBox1.Invalidate();
        }
        private void ScaleRotate(Matrix M, bool action)
        {
            CheckBox aroundPointCB;
            aroundPointCB = action ? scaleAroundPointCB : rotationAroundPointCB;

            if (segmentRB.Checked)
            {
                for (int i = 0; i < segments.Count; ++i)
                {
                    PointF translationPoint;

                    //производить действи€ вокруг точки
                    if (aroundPointCB.Checked)
                    {
                        if (pointLocation != Point.Empty)
                            translationPoint = pointLocation;
                        else
                            return;
                    }
                    //производить действи€ вокруг отрезка
                    else
                    {
                        var PX = (segments[i].startP.X + segments[i].endP.X) / 2;
                        var PY = (segments[i].startP.Y + segments[i].endP.Y) / 2;
                        translationPoint = new PointF(PX, PY);
                    }
                    //переходим к новой системе координат
                    Transform(-1 * translationPoint.X, -1 * translationPoint.Y);

                    //поворачиваем или масштабируем
                    prodSegments(M, i);
                    //возвращаемс€ к старой системе
                    Transform(translationPoint.X, translationPoint.Y);
                }
            }
           if (polygonRB.Checked)
            {
                for (int i = 0; i < polygon.Count; ++i)
                {
                    PointF translationPoint;
                    //производить действи€ вокруг точки
                    if (aroundPointCB.Checked)
                    {
                        if (pointLocation != Point.Empty)
                            translationPoint = pointLocation;
                        else
                            return;
                    }
                    //производить действи€ вокруг полигона
                    else
                    {
                        var PX = (minPolygonCoord.X + maxPolygonCoord.X) / 2;
                        var PY = (minPolygonCoord.Y + maxPolygonCoord.Y) / 2;
                        translationPoint = new PointF(PX, PY);
                    }
                    //переходим к новой системе координат
                    Transform(-1 * translationPoint.X, -1 * translationPoint.Y);
                    //поворачиваем или масштабируем
                    prodPolygon(M, i);
                    //возвращаемс€ к старой системе
                    Transform(translationPoint.X, translationPoint.Y);
                }
            }
        }

        private void DefAngle(object sender, EventArgs e)
        {
            angleNumUD.Value = 90;
        }
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
                    if (FindOrientation(polygon[i], p, polygon[next]) == 0)
                        return IsInSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            return count % 2 == 1;
        }
        bool IsInSegment(PointF q, PointF p, PointF r)
        {
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y) ? true : false;
        }
        private void RotationButton(object sender, EventArgs e)
        {
            float angle = (float)angleNumUD.Value;
            //матрица вращени€
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
                                M[i,j] = (float)Math.Cos(angle * Math.PI / 180); 
                                break;
                            case 1:
                                M[i, j] = (float)Math.Cos(angle * Math.PI / 180);
                                break;
                            case 2:
                                M[i, j] = 1;
                                break;
                            default:
                                break;
                        }
                        continue;
                    }
                    if (i == 0 && j == 1)
                    {
                        M[i, j] = (float)Math.Sin(angle * Math.PI / 180);
                        continue;
                    }
                    if (i == 1 && j == 0)
                    {
                        M[i, j] = (float)-Math.Sin(angle * Math.PI / 180);
                        continue;
                    }
                    M[i, j] = 0;
                }
            }
            ScaleRotate(M, false);
            pictureBox1.Invalidate();
        }



        //  лассифицирует положение точки относительно ребра     
        int GetPointLocation(PointF p, Point A, Point B)
        {
            if (p.IsEmpty)
            {
                return -404;
            }
            return (int)((p.X - A.X) * (B.Y - A.Y) - (p.Y - A.Y) * (B.X - A.X));
        }

        //определ€ет принадлежит ли точка полигону

        int FindOrientation(PointF p, PointF q, PointF r)
        {
            float val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);
            switch (val)
            {
                case 0:
                    return 0;
                default:
                    return (val > 0) ? 1 : 2;
            }
        }



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

        private void Draw(object sender, PaintEventArgs e)
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
                // автоматически достраивает до полигона
                g.DrawLine(Pens.Red, polygon[0], polygon[polygon.Count - 1]);

            }
            // нужно чтобы было видно что будет нарисовано при отпускании мыши
            if (startPoint != Point.Empty && endPoint != Point.Empty)
                g.DrawLine(Pens.Red, startPoint, endPoint);

            // рисуем полигон
            g.DrawEllipse(Pens.Blue, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);
            g.FillEllipse(Brushes.Blue, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);

            ChangeText();
        }

        private void ChangeText()
        {
            if (segments.Count > 0)
            {
                int pos = GetPointLocation(pointLocation, segments[segments.Count - 1].startP,
                    segments[segments.Count - 1].endP);

                if (pos >= 0)
                    posRelativeToSegmentLabel.Text = "Ћежит слева от линии";
                else if (pos == -404)
                    posRelativeToSegmentLabel.Text = "-";
                else
                    posRelativeToSegmentLabel.Text = "Ћежит справа от линии";
            }

            if (polygon.Count > 2)
            {
                posRelativeToPolygonLabel.Text = isInPoligon(polygon, pointLocation)
                    ? "“очка принадлежит полигону" : "“очка не принадлежит полигону";
                if (pointLocation.IsEmpty)
                {
                    posRelativeToPolygonLabel.Text = "-";
                }
            }

            if (segments.Count > 1)
            {
                PointF intersection = new PointF(-1, -1);
                int n = segments.Count - 1;
                intersection = pointOfIntersection(segments[n - 1].startP, segments[n - 1].endP, segments[n].startP, segments[n].endP);
                if (intersection.X == -1 && intersection.Y == -1)
                    intersectionPointLabel.Text = "ќтрезки не пересекаютс€";
                else
                {
                    intersectionPointLabel.Text = " " + intersection.ToString();
                    g.DrawEllipse(Pens.Green, intersection.X - 2, intersection.Y - 2, 5, 5);
                    g.FillEllipse(Brushes.Green, intersection.X - 2, intersection.Y - 2, 5, 5);
                    pictureBox1.Invalidate();
                }
            }
        }
    }
}