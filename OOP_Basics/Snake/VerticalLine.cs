using System;
using System.Collections.Generic;

namespace Snake
{
    class VerticalLine : Figure
    {
        public VerticalLine(int yTop, int yBottom, int x, char sym)
        {
            pList = new List<Point>();

            for (int y = yTop; y <= yBottom; y++)
                pList.Add(new Point(x, y, sym));
        }
    }
}
