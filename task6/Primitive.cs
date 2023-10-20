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
        void Draw(Graphics g, AffineTransformer projection, int width, int height);

        void MultiplyWithTransformMatr(AffineTransformer t);
    }
}
