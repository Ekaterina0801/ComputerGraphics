using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;

namespace task9
{
    public class View3D
    {
        /*
        * Clockwise - сторона, на которой вершины идут по часовой стрелки.
        * Counterclockwise - сторона, на которой вершины идут против часовой стрелки.
        * None - никакая сторона.
        */
        public Bitmap ActiveTexture { get; set; }

        public enum Face { Clockwise, Counterclockwise, None }

        // Свойство, говорящее о том какую сторону треугольника не рисовать.
        public Face CullFace { get; set; } = Face.Clockwise;

        // List of positions of light sources
        public IList<Light> LightSources { get; set; } =
            new Light[3]
            {
                new Light(new Vector(1, 0, 0), Color.Red),
                new Light(new Vector(0, 1, 0), Color.Green),
                new Light(new Vector(0, 0, 1), Color.Blue)
            };

        // Буфер цвета
        public Bitmap colorBuffer;
        private Graphics graphics;
        // флаг буфера глубины
        public bool ZBufferEnabled { get; set; } = true;

        // Буфер глубины
        private double[,] zBuffer;

        private BitmapData bitmapData;

        private Scene sceneView;
        //private double Width;

        //private double Height;

        private Vector Center;

        private Vector CamPosition;
        private Matrix viewProjection;

        private double Width; //{ get { return sceneView.Width; } set { Width = value; } }
        private double Height;// { get { return sceneView.Height; } set { Height = value; } }

        public View3D(Scene sceneView)
        {
            this.sceneView = sceneView;
            Resize();
        }
        public View3D(Graphics g, Matrix viewProjection, double w, double h, Vector center, Vector camPos)
        {
            graphics = g;
            this.viewProjection = viewProjection;
            Width = w;
            Height = h;
            colorBuffer = new Bitmap((int)Width + 1, (int)Height + 1);
            Center = center;
            CamPosition = camPos;
        }
        public void Resize()
        {
            colorBuffer = new Bitmap((int)Width + 1, (int)Height + 1, PixelFormat.Format24bppRgb);
            zBuffer = new double[(int)Height + 1, (int)Width + 1];
        }

        public void StartDrawing()
        {
            // Очистим изображение
            using (var g = Graphics.FromImage(colorBuffer))
                g.FillRectangle(Brushes.Black, 0, 0, (int)Width, (int)Height);
            // Очистим z-буфер
            for (int i = 0; i < Height + 1; ++i)
                for (int j = 0; j < Width + 1; ++j)
                    zBuffer[i, j] = 1;
            bitmapData = colorBuffer.LockBits(
                new Rectangle(0, 0, colorBuffer.Width, colorBuffer.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            viewProjection = sceneView.Camera.ViewProjection;
        }

        public Bitmap FinishDrawing()
        {
            colorBuffer.UnlockBits(bitmapData);
            bitmapData = null;
            return colorBuffer;
        }

        private unsafe void SetPixel(int x, int y, double z, Color color)
        {
            if (ZBufferEnabled)
                if (zBuffer[y, x] < z) return;
                else zBuffer[y, x] = z;
            var pointer = (byte*)bitmapData.Scan0.ToPointer();
            pointer[y * bitmapData.Stride + 3 * x + 0] = color.B;
            pointer[y * bitmapData.Stride + 3 * x + 1] = color.G;
            pointer[y * bitmapData.Stride + 3 * x + 2] = color.R;
        }

        private Vector SpaceToClip(Vector v)
        {
            return v * viewProjection;
        }

        private Vector ClipToScreen(Vector v)
        {
            return NormalizedToScreen(Normalize(v));
        }

        private Vector Normalize(Vector v)
        {
            return new Vector(v.X / v.W, v.Y / v.W, v.Z / v.W);
        }

        private Vector NormalizedToScreen(Vector v)
        {
            return new Vector(
                (v.X + 1) / 2 * Width,
                (-v.Y + 1) / 2 * Height,
                v.Z);
        }

        private static double Interpolate(double x0, double x1, double f)
        {
            return x0 + (x1 - x0) * f;
        }

        private static long Interpolate(long x0, long x1, double f)
        {
            return x0 + (long)((x1 - x0) * f);
        }

        private static Color Interpolate(Color a, Color b, double f)
        {
            var R = Interpolate(a.R, b.R, f);
            var G = Interpolate(a.G, b.G, f);
            var B = Interpolate(a.B, b.B, f);
            return Color.FromArgb((byte)R, (byte)G, (byte)B);
        }

        private static Vector Interpolate(Vector a, Vector b, double f)
        {
            return new Vector(
                Interpolate(a.X, b.X, f),
                Interpolate(a.Y, b.Y, f),
                Interpolate(a.Z, b.Z, f),
                Interpolate(a.W, b.W, f));
        }

        private static Vertex Interpolate(Vertex a, Vertex b, double f)
        {
            return new Vertex(
                Interpolate(a.Coordinate, b.Coordinate, f),
                Interpolate(a.Color, b.Color, f),
                Interpolate(a.Normal, b.Normal, f),
                Interpolate(a.UVCoordinate, b.UVCoordinate, f));
        }

        private class PlaneBoundary
        {
            public static readonly PlaneBoundary[] BOUNDARIES =
            {
                new PlaneBoundary(-1, 0, 0, 1),
                new PlaneBoundary(1, 0, 0, 1),
                new PlaneBoundary(0, -1, 0, 1),
                new PlaneBoundary(0, 1, 0, 1),
                new PlaneBoundary(0, 0, -1, 1),
                new PlaneBoundary(0, 0, 1, 1)
            };

            public Vector P { get; set; }

            private PlaneBoundary(double a, double b, double c, double d)
            {
                P = new Vector(a, b, c, d);
            }

            public bool IsInside(Vector point)
            {
                return Vector.DotProduct4(P, point) > 0;
            }

            public Vertex Intersect(Vertex a, Vertex b)
            {
                var denom = Vector.DotProduct4(P, b.Coordinate) - Vector.DotProduct4(P, a.Coordinate);
                if (0 == denom) return null;
                return Interpolate(a, b, -Vector.DotProduct4(P, a.Coordinate) / denom);
            }
        }

        private static bool ClipPoint(Vector a)
        {
            foreach (var boundary in PlaneBoundary.BOUNDARIES)
                if (!boundary.IsInside(a))
                    return false;
            return true;
        }

        private static bool ClipLine(ref Vertex a, ref Vertex b)
        {
            foreach (var boundary in PlaneBoundary.BOUNDARIES)
            {
                if (boundary.IsInside(b.Coordinate))
                {
                    if (!boundary.IsInside(a.Coordinate))
                        a = boundary.Intersect(a, b);
                }
                else if (boundary.IsInside(a.Coordinate))
                    b = boundary.Intersect(a, b);
                else return false;
            }
            return true;
        }

        public void DrawPoint(Vertex a)
        {
            a.Coordinate = SpaceToClip(a.Coordinate);
            if (!ClipPoint(a.Coordinate)) return;
            a.Coordinate = ClipToScreen(a.Coordinate);
            const int POINT_SIZE = 5;
            for (int dy = 0; dy < POINT_SIZE; ++dy)
            {
                var y = (int)a.Coordinate.Y + dy - POINT_SIZE / 2;
                if (y < 0 || Height <= y) return;
                for (int dx = 0; dx < POINT_SIZE; ++dx)
                {
                    var x = (int)a.Coordinate.X + dx - POINT_SIZE / 2;
                    if (x < 0 || Width <= x) return;
                    SetPixel(x, y, a.Coordinate.Z, a.Color);
                }
            }
        }
        public void DrawLine(Vertex a, Vertex b)
        {
            // Преобразование координат
            a.Coordinate = SpaceToClip(a.Coordinate);
            b.Coordinate = SpaceToClip(b.Coordinate);

            // Обрезка линии
            if (!ClipLine(ref a, ref b))
                return;

            // Преобразование координат
            a.Coordinate = ClipToScreen(a.Coordinate);
            b.Coordinate = ClipToScreen(b.Coordinate);

            // Извлечение координат
            int x0 = (int)a.Coordinate.X;
            int y0 = (int)a.Coordinate.Y;
            int x1 = (int)b.Coordinate.X;
            int y1 = (int)b.Coordinate.Y;

            // Вычисление разницы между координатами
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            // Определение направления
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            // Вычисление ошибки
            int err = dx - dy;

            // Инициализация текущих координат
            int currentX = x0;
            int currentY = y0;

            // Основной цикл отрисовки
            while (true)
            {
                // Вычисление интерполированных точек
                double f = dx < dy ? Math.Abs(currentY - a.Coordinate.Y) / dy : Math.Abs(currentX - a.Coordinate.X) / dx;
                var point = Interpolate(a, b, f);

                // Установка пикселя
                SetPixel(currentX, currentY, point.Coordinate.Z, point.Color);

                // Проверка завершения цикла
                if (currentX == x1 && currentY == y1)
                    break;

                // Вычисление новой ошибки
                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    currentX += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    currentY += sy;
                }
            }
        }


        /* Если на входе значения от 0 до 1, то на выходе тоже будут значения от 0 до 1.
         * Используется для сложения компонентов цветов. */
        private double NormalizedAdd(double x, double y)
        {
            return x + y - x * y;
        }

        private Color NormalizedAdd(Color a, Color b)
        {
            return Color.FromArgb(
                (int)(255 * NormalizedAdd(a.R / 255.0, b.R / 255.0)),
                (int)(255 * NormalizedAdd(a.G / 255.0, b.G / 255.0)),
                (int)(255 * NormalizedAdd(a.B / 255.0, b.B / 255.0)));
        }

        private Color calculateBright(Vertex a, Light light)
        {
            var i = Math.Max(0, Vector.DotProduct(a.Normal, (light.Coordinate - a.Coordinate).Normalize()));
            return Color.FromArgb(
                (byte)(a.Color.R * light.Color.R / 255.0 * i),
                (byte)(a.Color.G * light.Color.G / 255.0 * i),
                (byte)(a.Color.B * light.Color.B / 255.0 * i));
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        private static List<Vertex> ClipTriangle(Vertex a, Vertex b, Vertex c)
        {
            List<Vertex> vertices = new List<Vertex>(new Vertex[] { a, b, c });
            foreach (var boundary in PlaneBoundary.BOUNDARIES)
                vertices = ClipPolygon(vertices, boundary);
            return vertices;
        }

        //removing non visible verges
        private static List<Vertex> ClipPolygon(List<Vertex> vertices, PlaneBoundary boundary)
        {
            List<Vertex> result = new List<Vertex>(vertices.Count);
            for (int i = 0; i < vertices.Count; ++i)
            {
                var a = vertices[(i + vertices.Count - 1) % vertices.Count];
                var b = vertices[i];
                if (boundary.IsInside(b.Coordinate))
                {
                    if (!boundary.IsInside(a.Coordinate))
                        result.Add(boundary.Intersect(a, b));
                    result.Add(b);
                }
                else if (boundary.IsInside(a.Coordinate))
                    result.Add(boundary.Intersect(a, b));
            }
            return result;
        }
        public void DrawTriangle(Vertex a, Vertex b, Vertex c)
        {
            Color ac, bc, cc;
            ac = bc = cc = Color.Black;
            foreach (var lightSource in LightSources)
            {
                ac = NormalizedAdd(ac, calculateBright(a, lightSource));
                bc = NormalizedAdd(bc, calculateBright(b, lightSource));
                cc = NormalizedAdd(cc, calculateBright(c, lightSource));
            }
            a.Color = ac;
            b.Color = bc;
            c.Color = cc;
            a.Coordinate = SpaceToClip(a.Coordinate);
            b.Coordinate = SpaceToClip(b.Coordinate);
            c.Coordinate = SpaceToClip(c.Coordinate);
            var vertices = ClipTriangle(a, b, c);

            if (vertices == null)
                return;

            for (int i = 0; i < vertices.Count; ++i)
                vertices[i].Coordinate = ClipToScreen(vertices[i].Coordinate);
            DrawPolygonInternal(vertices);
        }

        // Принимает на вход координаты в пространстве экрана.
        private void DrawPolygonInternal(List<Vertex> vertices)
        {
            for (int i = 1; i < vertices.Count - 1; ++i)
                DrawTriangleInternal(vertices[0], vertices[i], vertices[i + 1]);
        }


        // Принимает на вход координаты в пространстве экрана.
        private void DrawTriangleInternal(Vertex a, Vertex b, Vertex c)
        {
            if (Face.None != CullFace)
            {
                var u = b.Coordinate - a.Coordinate;
                var v = c.Coordinate - a.Coordinate;
                if (Face.Counterclockwise == CullFace)
                    Swap(ref u, ref v);
                if (Vector.AngleVect(new Vector(0, 0, 1), Vector.CrossProduct(u, v)) > Math.PI / 2)
                    return;
            }
            if (a.Coordinate.Y > b.Coordinate.Y)
                Swap(ref a, ref b);
            if (a.Coordinate.Y > c.Coordinate.Y)
                Swap(ref a, ref c);
            if (b.Coordinate.Y > c.Coordinate.Y)
                Swap(ref b, ref c);
            for (double y = Math.Ceiling(a.Coordinate.Y); y < c.Coordinate.Y; ++y)
            {
                bool topHalf = y < b.Coordinate.Y;
                var a0 = a;
                var a1 = c;
                var left = Interpolate(a0, a1, (y - a0.Coordinate.Y) / (a1.Coordinate.Y - a0.Coordinate.Y));
                var b0 = topHalf ? a : b;
                var b1 = topHalf ? b : c;
                var right = Interpolate(b0, b1, (y - b0.Coordinate.Y) / (b1.Coordinate.Y - b0.Coordinate.Y));
                if (left.Coordinate.X > right.Coordinate.X)
                    Swap(ref left, ref right);
                for (double x = Math.Ceiling(left.Coordinate.X); x < right.Coordinate.X; ++x)
                {
                    var point = Interpolate(left, right, (x - left.Coordinate.X) / (right.Coordinate.X - left.Coordinate.X));
                    SetPixel((int)x, (int)y, point.Coordinate.Z, point.Color);
                }
            }
        }
    }
}
