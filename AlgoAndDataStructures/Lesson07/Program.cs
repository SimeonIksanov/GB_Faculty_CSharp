using System;

namespace Lesson07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задача о ходе коня, одно из решений:");
            new KnightsTour(size: 5, startX: 0, startY: 0).Start();

            Console.WriteLine();

            Console.WriteLine("Задача о восьми ферзях, одно из решений:");
            new EightQueensPuzzle(8).Start();

        }
    }
}
