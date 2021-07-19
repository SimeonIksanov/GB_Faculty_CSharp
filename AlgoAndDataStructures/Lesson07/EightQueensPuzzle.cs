using System;
using System.Collections.Generic;

namespace Lesson07
{
    class EightQueensPuzzle
    {
        private int _deskSize;
        private int queensCount;

        private List<(int x, int y)> listOfQueen = new List<(int x, int y)>();

        private int[,] desk;

        public EightQueensPuzzle(int count)
        {
            _deskSize = count;
            queensCount = count;
            desk = new int[_deskSize, _deskSize];
        }

        public void Start()
        {
            if (Search(1))
                PrintDesk();
            else
                Console.WriteLine("no solution");
        }

        private void PrintDesk()
        {
            int[,] queenDesk = new int[_deskSize, _deskSize];
            for (int i = 0; i < listOfQueen.Count; i++)
            {
                queenDesk[listOfQueen[i].x, listOfQueen[i].y] = i + 1;
            }
            for (int i = 0; i < _deskSize; i++)
            {
                for (int j = 0; j < _deskSize; j++)
                {
                    Console.Write($"{queenDesk[i, j],2} ");
                }
                Console.WriteLine();
            }
        }

        private void AddQueen(int x, int y, int n)
        {
            listOfQueen.Add((x, y));
            desk[x, y] += n;
            MarkHorizontal(x, y, n);
            MarkVertical(x, y, n);
            MarkDiagonal(x, y, n);
        }

        private void RemoveQueen(int x, int y, int n)
        {
            listOfQueen.Remove((x, y));
            desk[x, y] += -n;
            MarkHorizontal(x, y, -n);
            MarkVertical(x, y, -n);
            MarkDiagonal(x, y, -n);
        }

        private void MarkHorizontal(int x, int y, int n)
        {
            for (int i = 0; i < _deskSize; i++)
                if (i != x)
                    desk[i, y] += n;
        }

        private void MarkVertical(int x, int y, int n)
        {
            for (int i = 0; i < _deskSize; i++)
                if (i != y)
                    desk[x, i] += n;
        }

        private void MarkDiagonal(int x, int y, int n)
        {
            int _x, _y;
            for (int i = -_deskSize; i < _deskSize; i++)
            {
                _x = x + i;
                _y = y + i;
                if (_x >= 0 && _x < _deskSize && _y >= 0 && _y < _deskSize && i != 0)
                    desk[_x, _y] += n;

                _x = x + i;
                _y = y - i;
                if (_x >= 0 && _x < _deskSize && _y >= 0 && _y < _deskSize && i != 0)
                    desk[_x, _y] += n;
            }
        }

        private bool Search(int n)
        {
            if (n > queensCount)
                return true;

            for (int i = 0; i < _deskSize; i++)
                for (int j = 0; j < _deskSize; j++)
                {
                    if (desk[i, j] == 0)
                    {
                        AddQueen(i, j, n);
                        if (Search(n + 1)) return true;
                        RemoveQueen(i, j, n);
                    }
                }
            return false;
        }
    }
}
