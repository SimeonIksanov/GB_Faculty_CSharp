using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Point : Figure
    {
        public Point(int x, int y, bool isVisible)
        {
            _x = x;
            _y = y;
            _isVisible = isVisible;
        }

        public override double Area
        {
            get { return 0; }
        }
    }
}
