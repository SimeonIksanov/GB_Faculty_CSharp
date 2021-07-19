using System;

namespace Lesson07
{
    class KnightsTour
    {
        private int deskSize;
        private int[,] desk;
        private (int x, int y)[] possibleMove = { (-1, -2), (-1, 2), (1, 2), (1, -2), (-2, -1), (-2, 1), (2, 1), (2, -1) };
        private int x0, y0;

        public KnightsTour(int size, int startX, int startY)
        {
            deskSize = size;
            desk = new int[size, size];
            x0 = startX;
            y0 = startY;
        }

        private void PrintDesk()
        {
            for (int i = 0; i < deskSize; i++)
            {
                for (int j = 0; j < deskSize; j++)
                {
                    Console.Write($"{desk[i, j],2} ");
                }
                Console.WriteLine();
            }
        }

        public void Start()
        {
            desk[x0, y0] = 1; // стартовая позиция
            SearchKnightsPath(desk, x0, y0, 2);
            PrintDesk();
        }

        private bool SearchKnightsPath(int[,] desk, int x0, int y0, int turn)
        {
            // x0, y0 - положение коня в данный момент
            // turn - порядковый номер следующего хода
            int x, y;

            if (turn > deskSize * deskSize)
            {
                return true;
            }

            for (int i = 0; i < 8; i++)
            {
                x = x0 + possibleMove[i].x; y = y0 + possibleMove[i].y;
                if (x >= 0 && x < deskSize && y >= 0 && y < deskSize && desk[x, y] == 0)
                {
                    desk[x, y] = turn;
                    if (SearchKnightsPath(desk, x, y, turn + 1)) return true;
                    desk[x, y] = 0;
                }
            }
            return false;
        }
    }
}
