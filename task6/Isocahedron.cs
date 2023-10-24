using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    class Isocahedron:Primitive
    {
        // кол-во вершин = 12
        private List<Point3D> points = new List<Point3D>();

        // кол-во граней = 20
        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return faces; } }

        public Point3D Center
        {
            get
            {
                Point3D p = new Point3D(0, 0, 0);
                for (int i = 0; i < 12; i++)
                {
                    p.X += Points[i].X;
                    p.Y += Points[i].Y;
                    p.Z += Points[i].Z;
                }
                p.X /= 12;
                p.Y /= 12;
                p.Z /= 12;
                return p;
            }
        }

        public Isocahedron(double size)
        {
            // радиус описанной сферы
            double R = (size * Math.Sqrt(2.0 * (5.0 + Math.Sqrt(5.0)))) / 4;

            // радиус вписанной сферы
            double r = (size * Math.Sqrt(3.0) * (3.0 + Math.Sqrt(5.0))) / 12;

            points = new List<Point3D>();

            for (int i = 0; i < 5; ++i)
            {
                points.Add(new Point3D(
                    r * Math.Cos(2 * Math.PI / 5 * i),
                    R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i)));
                points.Add(new Point3D(
                    r * Math.Cos(2 * Math.PI / 5 * i + 2 * Math.PI / 10),
                    -R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i + 2 * Math.PI / 10)));
            }

            points.Add(new Point3D(0, R, 0));
            points.Add(new Point3D(0, -R, 0));

            // середина
            for (int i = 0; i < 10; ++i)
                Faces.Add(new Face(new Point3D[] { points[i], points[(i + 1) % 10], points[(i + 2) % 10] }));

            for (int i = 0; i < 5; ++i)
            {
                // верхняя часть
                Faces.Add(new Face(new Point3D[] { points[2 * i], points[10], points[(2 * (i + 1)) % 10] }));
                // нижняя часть
                Faces.Add(new Face(new Point3D[] { points[2 * i + 1], points[11], points[(2 * (i + 1) + 1) % 10] }));
            }
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen=1)
        {
            if (Points.Count != 12) return;

            foreach (var Verge in Faces)
                Verge.Draw(g, projection, width, height);
        }
    }
}

