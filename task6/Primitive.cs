using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    interface Primitive
    {
        void Draw(Graphics g, AffineTransformer projection, int width, int height, short pen_color = 1);

        void MultiplyWithTransformMatr(AffineTransformer t);

        Point3D Center { get; }
    }
}
