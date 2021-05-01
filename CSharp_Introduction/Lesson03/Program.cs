using System;
using System.Linq;

namespace Lesson03
{
    class Program
    {
        static void Main(string[] args)
        {
            Task01();
            Task02();
            Task03();
            Task04();
            Task05();
        }

        static void Task01()
        {
            Console.WriteLine("Написать программу, выводящую элементы двумерного массива по диагонали.");

            int[,] array = new int[10, 10];

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = i * j;

            Console.WriteLine(string.Join(',', Enumerable.Range(0, array.GetLength(0)).Select(x => array[x, x])));
        }

        static void Task02()
        {
            Console.WriteLine("Написать программу «Телефонный справочник»: создать двумерный массив 5х2, хранящий список телефонных контактов: первый элемент хранит имя контакта, второй — номер телефона/email.");
            var addressBook = new string[5, 2]
            {
                { "Batman" , "batman@GothamCity.ru"},
                { "Superman" , "superman@Krypton.kr"},
                { "IronMan" , "IronMan@StarkIndustries.com"},
                { "JohnDowe" , "88008776153"},
                { "JaneDowe" , "88001235678"}
            };

            for (int i = 0; i < addressBook.GetLength(0); i++)
                Console.WriteLine("Name: {0}, Email: {1}", addressBook[i, 0], addressBook[i, 1]);
        }

        static void Task03()
        {
            Console.WriteLine("Написать программу, выводящую введённую пользователем строку в обратном порядке (olleH вместо Hello).");

            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            //for (int i = input.Length - 1; i >= 0; i--)
            //    Console.Write(input[i]);

            Console.WriteLine(new string(input.Reverse().ToArray()));
        }

        static void Task04()
        {
            Console.WriteLine("фибоначи с циклом и без цикла");
            int n = 20;
            Console.WriteLine(Fibo1(n)); // фибоначи с циклом
            Console.WriteLine(Fibo2(n));  // фибоначи без циклов.. рекурсия

            // есть еще фибоначи через возведение матрицы в степень, но там тоже циклы
        }

        static int Fibo1(int n)
        {
            int p1 = 1, p2 = 1, f = 0;
            if (n <= 2)
                return 1;

            while (n-- >= 3)
            {
                f = p1 + p2;
                p1 = p2;
                p2 = f;
            }

            return f;
        }

        static int Fibo2(int n)
        {
            if (n <= 2)
                return 1;

            return Fibo2(n - 1) + Fibo2(n - 2);
        }

        static void Task05()
        {
            Console.WriteLine("«Морской бой»: вывести на экран массив 10х10, состоящий из символов X и O, где Х — элементы кораблей, а О — свободные клетки");

            char[,] map = new char[10, 10];

            var rand = new Random();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    map[i, j] = rand.Next(20) < 2 ? 'X' : 'O';

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(map[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }
        }
    }
}
