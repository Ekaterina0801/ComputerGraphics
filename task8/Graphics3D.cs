using System;
using System.Drawing;

namespace task8
{
    public class Graphics3D
    {
        private Graphics graphics;

        public Bitmap ColorBuffer { get; set; }
        private double[,] ZBuffer { get; set; }

        public Matrix Transformation { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Vector CamPosition;
        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        public Graphics3D(Graphics graphics, Matrix transformation, double width, double height, Vector pos)
        {
            this.graphics = graphics;
            Transformation = transformation;
            Width = width;
            Height = height;
            CamPosition = pos;

            ColorBuffer = new Bitmap((int)Math.Ceiling(Width), (int)Math.Ceiling(Height));
            ZBuffer = new double[(int)Math.Ceiling(Width), (int)Math.Ceiling(Height)];

            for (int j = 0; j < (int)Math.Ceiling(Height); ++j)
                for (int i = 0; i < (int)Math.Ceiling(Width); ++i)
                    ZBuffer[i, j] = double.MaxValue;

        }

        private Vector SpaceToNormalized(Vector v)
        {
            return Normalize(v * Transformation);
        }

        private Vector Normalize(Vector v)
        {
            return new Vector(v.X / v.W, v.Y / v.W, v.Z / v.W);
        }

        public Vector NormScr(Vector v)
        {
            return new Vector(
                (v.X + 1) / 2 * Width, 
                (-v.Y + 1) / 2 * Height,
                v.Z);
        }
        private bool Check(Vector vertex)
        {
            return ((vertex.X >= 0 && vertex.X < Width) &&
                   (vertex.Y >= 0 && vertex.Y < Height) &&
                   (vertex.Z < 1) && (vertex.Z > -1));
        }


        // перевод координат из пространственных в экранные
        private Vector SpaceToScreenCoordinate(Vector spaceVertex)
        {
            return NormScr(SpaceToNormalized(spaceVertex));
        }

        private Vertex GetScene(Vertex vertex)
        {
            return new Vertex(SpaceToScreenCoordinate(vertex.Coordinate), vertex.Normal, vertex.Color);
        }

        public void DrawLine(Vector a, Vector b)
        {
            DrawLine(a, b, Pens.Black);
        }


        public void DrawPoint(Vector a, Color color)
        {
            var t = SpaceToNormalized(a);
            if (t.Z < -1 || t.Z > 1) return;
            var A = NormScr(t);
            if (Check(A))
                ColorBuffer.SetPixel((int)Math.Ceiling(A.X), (int)Math.Ceiling(A.Y), color);
        }

        public void DrawLine(Vector a, Vector b, Pen pen)
        {
            var t = SpaceToNormalized(a);
            if (t.Z < -1 || t.Z > 1) return;
            var A = NormScr(t);
            var u = SpaceToNormalized(b);
            if (u.Z < -1 || u.Z > 1) return;
            var B = NormScr(u);
            if (Check(A))
                graphics.DrawLine(pen, (float)A.X, (float)A.Y, (float)B.X, (float)B.Y);
        }
        private void Interpolate(Vertex a, Vertex b, double f, ref Vertex v)
        {
            v.Coordinate = Interpolate(a.Coordinate, b.Coordinate, f);
            v.Normal = Interpolate(a.Normal, b.Normal, f);
            v.Color = Interpolate(a.Color, b.Color, f);
        }

        private double Interpolate(double x0, double x1, double f)
        {
            return x0 + (x1 - x0) * f;
        }

        private long Interpolate(long x0, long x1, double f)
        {
            return x0 + (long)((x1 - x0) * f);
        }
        public void DrawTriangle(Vertex first, Vertex second, Vertex third)
        {
            // Преобразуем вершины из трехмерного пространства в пространство экрана
            first = GetScene(first);
            second = GetScene(second);
            third = GetScene(third);

            // Сортировка вершин по их координатам Y
            if (first.Coordinate.Y > second.Coordinate.Y)
                Swap(ref first, ref second);
            if (first.Coordinate.Y > third.Coordinate.Y)
                Swap(ref first, ref third);
            if (second.Coordinate.Y > third.Coordinate.Y)
                Swap(ref second, ref third);

            Vertex l = new Vertex();
            Vertex r = new Vertex();
            Vertex p = new Vertex();

            for (double y = first.Coordinate.Y; y < third.Coordinate.Y; ++y)
            {
                // Пропускаем рисование, если текущая координата Y находится за пределами экрана
                if (y < 0 || y > (Height - 1))
                    continue;

                bool topHalf = y < second.Coordinate.Y;

                var f0 = first;
                var f1 = third;
                Interpolate(f0, f1, (y - f0.Coordinate.Y) / (f1.Coordinate.Y - f0.Coordinate.Y), ref l);

                var ff0 = topHalf ? first : second;
                var ff1 = topHalf ? second : third;
                Interpolate(ff0, ff1, (y - ff0.Coordinate.Y) / (ff1.Coordinate.Y - ff0.Coordinate.Y), ref r);

                if (l.Coordinate.X > r.Coordinate.X)
                    Swap(ref l, ref r);

                for (double x = l.Coordinate.X; x < r.Coordinate.X; ++x)
                {
                    // Пропускаем рисование, если текущая координата X находится за пределами экрана
                    if (x < 0 || x > (Width - 1))
                        continue;

                    Interpolate(l, r, (x - l.Coordinate.X) / (r.Coordinate.X - l.Coordinate.X), ref p);

                    // Пропускаем рисование, если текущая глубина вершины находится за пределами диапазона
                    if (p.Coordinate.Z > 1 || p.Coordinate.Z < -1)
                        continue;

                    // Обновление буфера глубины и буфера цвета
                    if (p.Coordinate.Z < ZBuffer[(int)x, (int)y])
                    {
                        ZBuffer[(int)x, (int)y] = p.Coordinate.Z;
                        ColorBuffer.SetPixel((int)x, (int)y, p.Color);
                    }
                }
            }
        }

        private Color Interpolate(Color a, Color b, double f)
        {
            return Color.FromArgb((byte)Interpolate(a.R, b.R, f), 
                (byte)Interpolate(a.G, b.G, f), (byte)Interpolate(a.B, b.B, f));
        }

        private Vector Interpolate(Vector a, Vector b, double f)
        {
            return new Vector(
                Interpolate(a.X, b.X, f),
                Interpolate(a.Y, b.Y, f),
                Interpolate(a.Z, b.Z, f));
        }
     
    }
}
