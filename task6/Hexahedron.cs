using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab63D;
namespace lab63D
{
    class Hexahedron : Primitive
    {
        // кол-во вершин = 8
        private List<Point3D> points = new List<Point3D>();

        // кол-во граней = 6
        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return faces; } }

        public Point3D Center
        {
            get
            {
                Point3D p = new Point3D(0, 0, 0);
                for (int i = 0; i < 8; i++)
                {
                    p.X += Points[i].X;
                    p.Y += Points[i].Y;
                    p.Z += Points[i].Z;
                }
                p.X /= 8;
                p.Y /= 8;
                p.Z /= 8;
                return p;
            }
        }

        public Hexahedron(double size)
        {
            points = new List<Point3D>();

            points.Add(new Point3D(-size / 2, -size / 2, -size / 2));
            points.Add(new Point3D(-size / 2, -size / 2, size / 2));
            points.Add(new Point3D(-size / 2, size / 2, -size / 2));
            points.Add(new Point3D(size / 2, -size / 2, -size / 2));
            points.Add(new Point3D(-size / 2, size / 2, size / 2));
            points.Add(new Point3D(size / 2, -size / 2, size / 2));
            points.Add(new Point3D(size / 2, size / 2, -size / 2));
            points.Add(new Point3D(size / 2, size / 2, size / 2));

            Faces.Add(new Face(new Point3D[] { points[0], points[1], points[5], points[3] }));
            Faces.Add(new Face(new Point3D[] { points[2], points[6], points[3], points[0] }));
            Faces.Add(new Face(new Point3D[] { points[4], points[1], points[0], points[2] }));
            Faces.Add(new Face(new Point3D[] { points[7], points[5], points[3], points[6] }));
            Faces.Add(new Face(new Point3D[] { points[2], points[4], points[7], points[6] }));
            Faces.Add(new Face(new Point3D[] { points[4], points[1], points[5], points[7] }));

        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            if (Points.Count != 8) return;

            foreach (var Verge in Faces)
                Verge.Draw(g, projection, width, height);
        }
    }
}
