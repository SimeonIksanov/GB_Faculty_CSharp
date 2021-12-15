using System;
using System.Diagnostics;
using CoderLib;
using FiguresLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task01();
            // XUnit tests aplied

            Task02();
        }

        static void Task02()
        {
            Figure[] figures = new Figure[]
            {
                new Point(10, 10, true),
                new Circle(20, 20, true, 15),
                new Rectangle(30, 30, true, 10, 5)
            };

            foreach (Figure figure in figures)
            {
                Console.WriteLine(figure);
                Console.WriteLine();
            }
        }

        private static string Encrypt(ICoder coder, string inputString)
        {
            return coder.Encode(inputString);
        }

        private static string Decrypt(ICoder coder, string inputString)
        {
            return coder.Decode(inputString);
        }
    }
}
