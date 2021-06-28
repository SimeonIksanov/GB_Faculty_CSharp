using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Snake
{
    class Snake : Figure
    {
        private Direction direction;

        public Snake(Point tail, int length, Direction direction)
        {
            this.direction = direction;
            pList = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move()
        {
            var tail = pList.First();
            pList.Remove(tail);
            Point head = GetNextPoint();
            pList.Add(head);
            tail.Clear();
            head.Draw();
        }

        internal bool isHitTail()
        {
            Point head = pList.Last();
            for (int i = 0; i < pList.Count - 1; i++)
            {
                if (head.isHit(pList[i]))
                    return true;
            }
            return false;
        }

        private Point GetNextPoint()
        {
            Point head = pList.Last();
            Point newHead = new Point(head);
            newHead.Move(1, direction);
            return newHead;
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.LeftArrow)
                direction = Direction.Left;
            else if (keyInfo.Key == ConsoleKey.RightArrow)
                direction = Direction.Right;
            else if (keyInfo.Key == ConsoleKey.UpArrow)
                direction = Direction.Up;
            else if (keyInfo.Key == ConsoleKey.DownArrow)
                direction = Direction.Down;
        }

        internal bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.isHit(food))
            {
                food.Sym = head.Sym;
                pList.Add(food);
                return true;
            }
            return false;
        }
    }
}
