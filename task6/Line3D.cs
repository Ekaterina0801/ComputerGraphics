using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    class Line3D:Primitive
    {
        private Point3D a;
        private Point3D b;

        public Point3D A { get { return a; } set { a = value; } }
        public Point3D B { get { return b; } set { b = value; } }

        public Line3D(Point3D a, Point3D b)
        {
            A = a;
            B = b;
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            A = A.TransformPoint(t);
            B = B.TransformPoint(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height)
        {
            var c = A.TransformPoint(projection).NormalizedToDisplay(width, height);
            var d = B.TransformPoint(projection).NormalizedToDisplay(width, height);
            g.DrawLine(Pens.Black, (float)c.X, (float)c.Y, (float)d.X, (float)d.Y);
        }
    }
}
