using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task7_1
{
    class Point3D: Primitive
    {
        private double[] edges = new double[] { 0, 0, 0, 1 };
        private List<Point3D> points = new List<Point3D>();
        private List<Face> verges = new List<Face>();
        public List<Point3D> Points { get { return points; } set { points = value; } }
        public List<Face> Faces { get { return verges; } set { verges = value; } }
        public double X { get { return edges[0]; } set { edges[0] = value; } }
        public double Y { get { return edges[1]; } set { edges[1] = value; } }
        public double Z { get { return edges[2]; } set { edges[2] = value; } }

        public Point3D() { }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        private Point3D(double[] points)
        {
            edges = points;
        }

        public Point3D Center
        {
            get { return this; }
        }

        public static Point3D FromPoint(Point point)
        {
            return new Point3D(point.X, point.Y, 0);
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            double[] newEdges = new double[4];
            for (int i = 0; i < 4; ++i)
            {
                newEdges[i] = 0;
                for (int j = 0; j < 4; ++j)
                    newEdges[i] += edges[j] * t.Matrix[j, i];
            }
            edges = newEdges;
        }

        public Point3D TransformPoint(AffineTransformer t)
        {
            var p = new Point3D(X, Y, Z);
            p.MultiplyWithTransformMatr(t);
            return p;
        }

        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            var projected = TransformPoint(projection);
            if (Z < -1 || 1 < Z) return;
            var x = (projected.X + 1) / 2 * width;
            var y = (-projected.Y + 1) / 2 * height;
            g.DrawEllipse(new Pen(Color.Black, 2), (float)x-1, (float)y-1, 2, 2);
        }

        /// <summary>
        /// Преобразует нормализованные координаты из диапазона ([-1, 1], [-1, 1], [-1, 1]) в координаты экрана ([0, width), [0, height), [-1, 1]).
        /// </summary>
        /// <param name="width">Ширина экрана.</param>
        /// <param name="height">Высота экрана.</param>
        /// <returns>Точка в координатах экрана.</returns>
        public Point3D NormalizedToDisplay(int width, int height)
        {
            // Проверка входных данных
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height must be positive numbers.", nameof(width));
            }

            if (X < -1 || X > 1 || Y < -1 || Y > 1 || Z < -1 || Z > 1)
            {
                throw new ArgumentException("Normalized coordinates must be in the range [-1, 1].", nameof(X));
            }

            // Высчитывание координат
            var normalizedX = (X / edges[3] + 1) / 2 * width;
            var normalizedY = (-Y / edges[3] + 1) / 2 * height;

            // Создание и возврат новой точки
            return new Point3D(normalizedX, normalizedY, Z);
        }

        public int CompareTo(object p)
        {
            double rad_XZ = Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Z, 2));
            double rad_XZp = Math.Sqrt(Math.Pow(((Point3D)p).X, 2) + Math.Pow(((Point3D)p).Z, 2));
            if (this.Y > ((Point3D)p).Y)
            {
                return 1;
            }
            else if (this.Y < ((Point3D)p).Y)
            {
                return -1;
            }
            else
            {
                short c1 = this.PositionXZ(this.X, this.Z);
                short c2 = this.PositionXZ(((Point3D)p).X, ((Point3D)p).Z);
                if (c1 != c2)
                    return c1 - c2;
                double cos = this.X / rad_XZ;
                double cos_p = ((Point3D)p).X / rad_XZp;
                double sin = this.Z / rad_XZ;
                double sin_p = ((Point3D)p).Z / rad_XZp;
                Math.Acos(cos);
            }
            return 0;
        }

        public short PositionXZ(double x, double z)
        {
            if (x >= 0 && z >= 0)
            {
                return 1;
            }
            else if (x >= 0 && z <= 0)
            {
                return 2;
            }
            else if (x <= 0 && z <= 0)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}

