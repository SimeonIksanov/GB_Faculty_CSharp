using System;
using System.IO;

namespace Snake
{
    class Point
    {
        public int X;
        public int Y;
        public char Sym;

        public Point()
        {
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Sym = p.Sym;
        }

        public Point(int x, int y, char sym)
        {
            X = x;
            Y = y;
            Sym = sym;
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Sym);
        }

        public void Move(int offset, Direction direction)
        {
            if (direction == Direction.Right)
                X = X + offset;
            else if (direction == Direction.Left)
                X = X - offset;
            else if (direction == Direction.Up)
                Y = Y - offset;
            else
                Y = Y + offset;
        }

        internal void Clear()
        {
            Sym = ' ';
            Draw();
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Sym;
        }

        internal bool isHit(Point food)
        {
            return X == food.X && Y == food.Y;
        }
    }
}
