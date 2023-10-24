using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    class Octahedron : Primitive
    {
        // кол-во вершин = 6
        private List<Point3D> points = new List<Point3D>();

        // кол-во граней = 8
        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return Faces; } }

        public Point3D Center
        {
            get
            {
                Point3D p = new Point3D(0, 0, 0);
                for (int i = 0; i < 6; i++)
                {
                    p.X += Points[i].X;
                    p.Y += Points[i].Y;
                    p.Z += Points[i].Z;
                }
                p.X /= 6;
                p.Y /= 6;
                p.Z /= 6;
                return p;
            }
        }

        public Octahedron(double size)
        {

            points = new List<Point3D>();

            points.Add(new Point3D(-size / 2, 0, 0));
            points.Add(new Point3D(0, -size / 2, 0));
            points.Add(new Point3D(0, 0, -size / 2));
            points.Add(new Point3D(size / 2, 0, 0));
            points.Add(new Point3D(0, size / 2, 0));
            points.Add(new Point3D(0, 0, size / 2));


            Faces.Add(new Face(new Point3D[] { points[0], points[2], points[4] }));
            Faces.Add(new Face(new Point3D[] { points[2], points[4], points[3] }));
            Faces.Add(new Face(new Point3D[] { points[4], points[5], points[3] }));
            Faces.Add(new Face(new Point3D[] { points[0], points[5], points[4] }));
            Faces.Add(new Face(new Point3D[] { points[0], points[5], points[1] }));
            Faces.Add(new Face(new Point3D[] { points[5], points[3], points[1] }));
            Faces.Add(new Face(new Point3D[] { points[0], points[2], points[1] }));
            Faces.Add(new Face(new Point3D[] { points[2], points[1], points[3] }));
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height,short pen=1)
        {
            if (Points.Count != 6) return;

            foreach (var Face in Faces)
                Face.Draw(g, projection, width, height);
        }
    }
}
