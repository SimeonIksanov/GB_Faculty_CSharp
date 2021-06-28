using System;
using System.Collections.Generic;

namespace Snake
{
    class HorizontalLine : Figure
    {
        public HorizontalLine(int xLeft, int xRight, int y, char sym)
        {
            pList = new List<Point>();

            for (int x = xLeft; x <= xRight; x++)
                pList.Add(new Point(x, y, sym));
        }
    }
}
