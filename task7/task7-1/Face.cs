using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace task7_1
{
    class Face
    {

        private IList<Point3D> points = new List<Point3D>();
        public IList<Point3D> Points { get { return points; } set { points = value; } }
        public Face() { }

        public Face(IList<Point3D> points)
        {
            this.points = points;
        }

        public Point3D Center
        {
            get
            {
                double sumX = 0, sumY = 0, sumZ = 0;
                int n = 0;
                foreach (Point3D p in points)
                {
                    sumX += p.X;
                    sumY += p.Y;
                    sumZ += p.Z;
                    n += 1;
                }
                Point3D center = new Point3D(sumX/n, sumY/n, sumZ/n);
                return center;
            }
        }

        public void AddPoint(Point3D p)
        {
            points.Add(p);
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            if (Points.Count == 1)
                Points[0].Draw(g, projection, width, height);
            else
            {
                for (int i = 0; i < Points.Count - 1; ++i)
                {
                    var line = new Line3D(Points[i], Points[i + 1]);
                    line.Draw(g, projection, width, height);
                }
                (new Line3D(Points[Points.Count - 1], Points[0])).Draw(g, projection, width, height);
            }
        }
    }
}
