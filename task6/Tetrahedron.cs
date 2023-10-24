using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using lab63D;

namespace lab63D
{
    class Tetrahedron:Primitive
    {
        // первые 3 точки - основание тетраэдра, четвертая точка - вершина
        private List<Point3D> points = new List<Point3D>();

        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return faces; } }

        public Point3D Center
        {
            get
            {
                Point3D p = new Point3D(0, 0, 0);
                for (int i = 0; i < 4; i++)
                {
                    p.X += Points[i].X;
                    p.Y += Points[i].Y;
                    p.Z += Points[i].Z;
                }
                p.X /= 4;
                p.Y /= 4;
                p.Z /= 4;
                return p;
            }
        }

        public Tetrahedron(double size)
        {
            double h = Math.Sqrt(2.0 / 3.0) * size;
            points = new List<Point3D>
            {
                new Point3D(-size / 2, 0, h / 3),
                new Point3D(0, 0, -h * 2 / 3),
                new Point3D(size / 2, 0, h / 3),
                new Point3D(0, h, 0)
            };

            // Основание тетраэдра
            Faces.Add(new Face(new Point3D[] { points[0], points[1], points[2] }));
            // Левая грань
            Faces.Add(new Face(new Point3D[] { points[1], points[3], points[0] }));
            // Правая грань
            Faces.Add(new Face(new Point3D[] { points[2], points[3], points[1] }));
            // Передняя грань
            Faces.Add(new Face(new Point3D[] { points[0], points[3], points[2] }));
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var point in Points)
                point.MultiplyWithTransformMatr(t);
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            foreach (var Face in Faces)
                Face.Draw(g, projection, width, height);
        }
    }
}
