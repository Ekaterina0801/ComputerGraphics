using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace task7_1
{
    class Line3D:Primitive
    {
        private Point3D a;
        private Point3D b;
        private List<Point3D> points = new List<Point3D>();
        private List<Face> faces = new List<Face>();
        public Point3D A { get { return a; } set { a = value; } }
        public Point3D B { get { return b; } set { b = value; } }

        public List<Point3D> Points { get { return points; } set { points = value; } }
        public List<Face> Faces { get { return Faces; } set { Faces = value; } }
        public Line3D(Point3D a, Point3D b)
        {
            A = a;
            B = b;
        }

        public Point3D Center
        {
            get
            {
                Point3D center = new Point3D((A.X + B.X) / 2, (A.Y + B.Y) / 2, (A.Y + B.Y) / 2);
                return center; 
            }
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            A = A.TransformPoint(t);
            B = B.TransformPoint(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            var c = A.TransformPoint(projection).NormalizedToDisplay(width, height);
            var d = B.TransformPoint(projection).NormalizedToDisplay(width, height);
            Pen draw_pen;
            switch (pen_color)
            {
                case 1:
                    draw_pen = Pens.Black;
                    break;
                case 2:
                    draw_pen = Pens.Red;
                    break;
                case 3:
                    draw_pen = Pens.Green;
                    break;
                default:
                    draw_pen = Pens.Black;
                    break;
            }
            g.DrawLine(draw_pen, (float)c.X, (float)c.Y, (float)d.X, (float)d.Y);
        }
    }
}
