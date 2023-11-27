using System.Drawing;
using System.Numerics;

namespace task9
{
    public class Light
    {
        public Vector Coordinate { get; set; }
        public Color Color { get; set; }

        public Light(Vector coordinate, Color color)
        {
            Coordinate = coordinate;
            Color = color;
        }
    }
}
