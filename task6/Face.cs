using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab63D
{
    class Face:Primitive
    {

        private IList<Point3D> points = new List<Point3D>();

        public IList<Point3D> Points { get { return points; } set { points = value; } }

        public Face() { }

        public Face(IList<Point3D> points)
        {
            this.points = points;
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

        public void Draw(Graphics g, AffineTransformer projection, int width, int height)
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
