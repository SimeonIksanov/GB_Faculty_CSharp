using System;
using System.Collections.Generic;

namespace Snake
{
    class Figure
    {
        protected List<Point> pList;

        public virtual void Draw()
        {
            foreach (Point p in pList)
                p.Draw();
        }

        internal bool isHit(Figure figure)
        {
            foreach (Point point in pList)
                if (figure.isHit(point))
                    return true;
            return false;
        }

        private bool isHit(Point otherPoint)
        {
            foreach (Point point in pList)
                if (point.isHit(otherPoint))
                    return true;
            return false;
        }
    }
}
