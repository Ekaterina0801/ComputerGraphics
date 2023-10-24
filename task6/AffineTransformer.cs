using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    class AffineTransformer
    {
        private Matrix matrix;

        public Matrix Matrix { get { return matrix; } }

        public AffineTransformer()
        {
            matrix = Identity().matrix;
        }

        public AffineTransformer(Matrix matrix)
        {
            this.matrix = matrix;
        }

        public static AffineTransformer RotateX(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new AffineTransformer(
                new Matrix(new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, cos, -sin, 0 },
                    { 0, sin, cos, 0 },
                    { 0, 0, 0, 1 }
                }));

        }

        public static AffineTransformer RotateY(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new AffineTransformer(
                new Matrix(new double[,]
                {
                    { cos, 0, sin, 0 },
                    { 0, 1, 0, 0 },
                    { -sin, 0, cos, 0 },
                    { 0, 0, 0, 1 }
                }));
        }

        public static AffineTransformer RotateZ(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new AffineTransformer(
                new Matrix(new double[,]
                {
                    { cos, -sin, 0, 0 },
                    { sin, cos, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                }));
        }

        public static AffineTransformer RotateLine(Line3D line, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            var l1 = line.A;
            var l2 = line.B;
            double l = Math.Sign(line.B.X - line.A.X);
            double m = Math.Sign(line.B.Y - line.A.Y);
            double n = Math.Sign(line.B.Z - line.A.Z);
            double length = Math.Sqrt(l * l + m * m + n * n);
            l /= length; m /= length; n /= length;
            var j = new Matrix(new double[,]
                {
                   { l * l + cos * (1 - l * l),   l * (1 - cos) * m + n * sin,   l * (1 - cos) * n - m * sin,  0  },
                   { l * (1 - cos) * m - n * sin,   m * m + cos * (1 - m * m),    m * (1 - cos) * n + l * sin,  0 },
                   { l * (1 - cos) * n + m * sin,  m * (1 - cos) * n - l * sin,    n * n + cos * (1 - n * n),   0 },
                   {            0,                            0,                             0,               1   }
                });
            return new AffineTransformer(
                j);

        }

        public static AffineTransformer Scale(double fx, double fy, double fz)
        {
            return new AffineTransformer(
                new Matrix(new double[,] {
                    { fx, 0, 0, 0 },
                    { 0, fy, 0, 0 },
                    { 0, 0, fz, 0 },
                    { 0, 0, 0, 1 }
                }));
        }

        public static AffineTransformer Translate(double dx, double dy, double dz)
        {
            return new AffineTransformer(
                new Matrix(new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { dx, dy, dz, 1 },
                }));
        }

        public static AffineTransformer Identity()
        {
            return new AffineTransformer(
               new Matrix( new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                }));
        }


        public static AffineTransformer ReflectX()
        {
            return Scale(-1, 1, 1);
        }

        public static AffineTransformer ReflectY()
        {
            return Scale(1, -1, 1);
        }

        public static AffineTransformer ReflectZ()
        {
            return Scale(1, 1, -1);
        }

        public static AffineTransformer OrthographicXYProjection()
        {
            return Identity();
        }

        public static AffineTransformer OrthographicXZProjection()
        {
            return Identity() * RotateX(-Math.PI / 2);
        }

        public static AffineTransformer OrthographicYZProjection()
        {
            return Identity() * RotateY(Math.PI / 2);
        }

        public static AffineTransformer IsometricProjection()
        {
            return Identity() * RotateY(Math.PI / 4) * RotateX(-Math.PI / 4);
        }

        public static AffineTransformer PerspectiveProjection()
        {
            return new AffineTransformer(
                new Matrix(new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 0, 2 },
                    { 0, 0, 0, 1 }
                }));
        }

        public static AffineTransformer operator *(AffineTransformer t1, AffineTransformer t2)
        {
            Matrix matrix = new Matrix(4, 4);
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    matrix[i, j] = 0;
                    for (int k = 0; k < 4; ++k)
                        matrix[i, j] += t1.matrix[i, k] * t2.matrix[k, j];
                }
            return new AffineTransformer(matrix);
        }
    }
}