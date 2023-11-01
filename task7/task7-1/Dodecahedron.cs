using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task7_1
{
    class Dodecahedron : Primitive
    {
        // кол-во вершин = 20
        private List<Point3D> points = new List<Point3D>();

        // кол-во граней = 12
        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return faces; } }
        public Dodecahedron(List<Point3D> p)
        {
            Isocahedron iso = new Isocahedron(p);
            points = p;
            faces = new List<Face>();

            points = new List<Point3D>();

            foreach (Face f in iso.Faces)
            {
                Point3D center = f.Center;
                points.Add(center);
            }

            for (int i = 0; i <= 8; i += 2)
            {
                Faces.Add(new Face(new Point3D[] { points[i], points[i + 1], points[(i + 2) % 10], points[10 + (2 + i) % 10], points[10 + i] }));
                Faces.Add(new Face(new Point3D[] { points[i + 1], points[(i + 2) % 10], points[(i + 3) % 10], points[11 + (2 + i) % 10], points[11 + i] }));
            }
            List<Point3D> high = new List<Point3D>();
            List<Point3D> low = new List<Point3D>();
            for (int i = 0; i <= 8; i += 2)
            {
                high.Add(points[10 + i]);
                low.Add(points[11 + i]);
            }
            Faces.Add(new Face(high));
            Faces.Add(new Face(low));
        }
        public Point3D Center
        {
            get
            {
                Point3D p = new Point3D(0, 0, 0);
                for (int i = 0; i < 20; i++)
                {
                    p.X += Points[i].X;
                    p.Y += Points[i].Y;
                    p.Z += Points[i].Z;
                }
                p.X /= 20;
                p.Y /= 20;
                p.Z /= 20;
                return p;
            }
        }

        public Dodecahedron(double size)
        {
            Isocahedron iso = new Isocahedron(0.5);

            points = new List<Point3D>();

            foreach (Face f in iso.Faces)
            {
                Point3D center = f.Center;
                points.Add(center);
            }

            for (int i = 0; i <= 8; i += 2)
            {
                Faces.Add(new Face(new Point3D[] { points[i], points[i+1], points[(i+2)%10], points[10 + (2 + i)%10], points[10 + i] }));
                Faces.Add(new Face(new Point3D[] { points[i+1], points[(i+2)%10], points[(i+3)%10], points[11 + (2 + i) % 10], points[11 + i] }));
            }
            List<Point3D> high = new List<Point3D>();
            List<Point3D> low = new List<Point3D>();
            for (int i = 0; i <= 8; i += 2)
            {
                high.Add(points[10 + i]);
                low.Add(points[11 + i]);
            }
            Faces.Add(new Face(high));
            Faces.Add(new Face(low));
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen = 1)
        {
            if (Points.Count != 20) return;

            foreach (Face face in Faces)
            {
                face.Draw(g, projection, width, height);
            }
        }

        override public string ToString()
        {
            return "Dodecahedron";
        }
    }
}
