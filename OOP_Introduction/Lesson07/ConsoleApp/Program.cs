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
            //Task01();

            Task02();
        }

        static void Task01()
        {
            Console.WriteLine("=== ACoder ===");
            ICoder coder = new ACoder();

            Console.WriteLine("Encode 'amz' -> " + Encrypt(coder, "amz"));
            Console.WriteLine("Encode 'AMZ' -> " + Encrypt(coder, "AMZ"));
            Console.WriteLine("Decode 'bna' -> " + Decrypt(coder, "bna"));
            Console.WriteLine("Decode 'BNA' -> " + Decrypt(coder, "BNA"));
            Console.WriteLine("Encode 'яма' -> " + Encrypt(coder, "яма"));
            Console.WriteLine("Decode 'анб' -> " + Decrypt(coder, "анб"));

            Console.WriteLine("\n\n=== BCoder ===");
            coder = new BCoder();
            Console.WriteLine("Encode 'amz' -> " + Encrypt(coder, "amz"));
            Console.WriteLine("Decode 'zna' -> " + Decrypt(coder, "zna"));
            Console.WriteLine("Encode 'AMZ' -> " + Encrypt(coder, "AMZ"));
            Console.WriteLine("Decode 'ZNA' -> " + Decrypt(coder, "ZNA"));
            Console.WriteLine("Encode 'ая' -> " + Encrypt(coder, "ая"));
            Console.WriteLine("Decode 'АЯ' -> " + Decrypt(coder, "АЯ"));
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
