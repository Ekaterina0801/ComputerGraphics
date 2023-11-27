using System.Drawing.Drawing2D;
using System.Numerics;

namespace task9
{
    public class Camera
    {
        public Vector Position { get; set; }
        public double AngleY { get; set; }
        public double AngleX { get; set; }
        public Matrix Projection { get; set; }

        public Vector Forward { get { return new Vector(0, 0, -1) * Transformer.RotateX(AngleX) * Transformer.RotateY(AngleY); } }
        public Vector Left { get { return new Vector(-1, 0, 0) * Transformer.RotateX(AngleX) * Transformer.RotateY(AngleY); } }
        public Vector Up { get { return new Vector(0, 1, 0) * Transformer.RotateX(AngleX) * Transformer.RotateY(AngleY); } }
        public Vector Right { get { return -Left; } }
        public Vector Backward { get { return -Forward; } }
        public Vector Down { get { return -Up; } }

        public Matrix ViewProjection
        {
            get
            {
                return Transformer.Translate(-Position)
                    * Transformer.RotateY(-AngleY)
                    * Transformer.RotateX(-AngleX)
                    * Projection;
            }
        }

        public Camera(Vector position, double angleY, double angleX, Matrix projection)
        {
            Position = position;
            AngleY = angleY;
            AngleX = angleX;
            Projection = projection;
        }
    }
}
