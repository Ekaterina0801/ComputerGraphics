using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab5.Color_Modifier
{
    public class Color_Modifier
    {
        short a;
        short r;
        short g;
        short b;

        public Color_Modifier(short x1, short x2, short x3, short x4)
        {
            a = 0;
            r = 0;
            g = 0;
            b = 0;
        }

        public void Set(short x1, short x2, short x3, short x4)
        {
            a = x1;
            r = x2;
            g = x3;
            b = x4;
        }

        public Color Change(Color col)
        {
            int new_A = col.A + this.a;
            if (new_A > 255) new_A = 255;
            if (new_A < 0) new_A = 0;
            int new_R = col.R + this.r;
            if (new_R > 255) new_R = 255;
            if (new_R < 0) new_R = 0;
            int new_G = col.G + this.g;
            if (new_G > 255) new_G = 255;
            if (new_G < 0) new_G = 0;
            int new_B = col.B + this.b;
            if (new_B > 255) new_B = 255;
            if (new_B < 0) new_B = 0;
            return Color.FromArgb(new_A, new_R, new_G, new_B);
        }

    }
}
