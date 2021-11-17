using System;
using FiguresLib;
using BankAccountLib;

namespace Lesson03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" === TASK 01 OUTPUT ===");
            Task01();

            Console.WriteLine();
            Console.WriteLine(" === TASK 02 OUTPUT ===");
            Task02();


            Console.WriteLine();
            Console.WriteLine(" === TASK 03 OUTPUT ===");
            Task03();
        }

        static void Task01()
        {
            Account from = new Account(1000);
            Console.WriteLine(from);

            Account to = new Account(500);
            Console.WriteLine(to);

            Console.WriteLine("Trying to make payment");
            Console.WriteLine(
                to.TakePayment(from, 123) ? "success" : "failure"
            );

            Console.WriteLine($"from == to ? : {from == to}");
            Console.WriteLine($"from != to ? : {from != to}");


            Console.WriteLine(from);
            Console.WriteLine(to);
        }

        static void Task02()
        {
            Console.WriteLine("Lord of Rings.. coming soon");
            //throw new NotImplementedException();
        }
        static void Task03()
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

            Console.WriteLine();
            Console.WriteLine("========= Move all figures and change color to green =========");
            Console.WriteLine();

            foreach (Figure figure in figures)
            {
                figure.Color = ConsoleColor.Green;
                figure.MoveHorizontal(2);
                figure.MoveVertical(3);

                Console.WriteLine(figure);
                Console.WriteLine();
            }
        }
    }
}
