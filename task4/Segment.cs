using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    // Класс Segment для задания отрезка (начало и конец отрезка) 
    class Segment
    {
        public Point leftP, rightP;

        public Segment() { leftP = new Point(); rightP = new Point(); }

        public Segment(Point l, Point r) { leftP = l; rightP = r; }
    }
}
