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
        public Point startP, endP;

        public Segment() { startP = new Point(); endP = new Point(); }

        public Segment(Point l, Point r) { startP = l; endP = r; }
    }
}
