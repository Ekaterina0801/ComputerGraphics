using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task7_1
{
    interface Primitive
    {
        void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1);

        void MultiplyWithTransformMatr(AffineTransformer t);

        Point3D Center { get; }
        List<Point3D> Points { get; }

        List<Face> Faces { get; }
    }
}
