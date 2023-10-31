using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using task7_1;

namespace task7_1
{
    class RotationFigure : Primitive
    {
        private List<Point3D> points = new List<Point3D>();

        private List<Face> faces = new List<Face>();

        public List<Point3D> Points { get { return points; } }
        public List<Face> Faces { get { return faces; } }

        public Point3D Center => throw new NotImplementedException();

        public RotationFigure(List<Point3D> p, List<Face> v, int count_start_points)
        {
            points = p;
            faces = v;
        }

        public RotationFigure(List<Point3D> p, int axis, int density)
        {
            if (axis < 0 || axis > 2)
                return;

            points.AddRange(p);
            List<Point3D> rotatedPoints = new List<Point3D>();
            Func<double, AffineTransformer> rotation;
            switch (axis)
            {
                case 0: rotation = AffineTransformer.RotateX; break;
                case 1: rotation = AffineTransformer.RotateY; break;
                default: rotation = AffineTransformer.RotateZ; break;
            }

            for (int i = 1; i < density - 1; ++i)
            {
                double angle = 2 * Math.PI * i / (density - 1);
                foreach (var point in p)
                    rotatedPoints.Add(point.TransformPoint(rotation(angle)));
                points.AddRange(rotatedPoints);
                rotatedPoints.Clear();
            }
            var n = p.Count;
            for (int i = 0; i < density - 1; ++i)
                for (int j = 0; j < n - 1; ++j)
                    Faces.Add(new Face(new List<Point3D> {
                        points[i * n + j], points[(i + 1) % (density - 1) * n + j],
                        points[(i + 1) % (density - 1) * n + j + 1], points[i * n + j + 1] }));
        }

        public void MultiplyWithTransformMatr(AffineTransformer t)
        {
            foreach (var face in Faces)
                foreach (var point in face.Points)
                    point.MultiplyWithTransformMatr(t);
        }
        public void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1)
        {
            foreach (var Face in Faces)
                Face.Draw(g, projection, width, height);
        }

        override public string ToString()
        {
            return "Rotation Figure";
        }

      
    }
}