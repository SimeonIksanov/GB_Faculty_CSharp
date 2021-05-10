using System;
using System.Text;

namespace Lesson04
{
    class Program
    {
        static void Main(string[] args)
        {
            Task01();
            Task02();
            Console.WriteLine(Task03());
            Task04();
            Task05();
        }

        static void Task01()
        {
            /*
             * Написать метод GetFullName(string firstName, string lastName, string patronymic),
             * принимающий на вход ФИО в разных аргументах и возвращающий объединённую строку с ФИО.
             * Используя метод, написать программу, выводящую в консоль 3–4 разных ФИО.
             */
            var fioList = new (string lastname, string firstname, string patronymic)[]
            {
                ("ivanov", "ivan", "ivanovich"),
                ("petrov", "petr", "petrovich"),
                ("sidorov","sidr","sidorovich"),
                ("zhukov","zhuk","zhukovich")
            };

            foreach (var fio in fioList)
                Console.WriteLine(GetFullName(fio.firstname, fio.lastname, fio.patronymic));
        }

        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            var sb = new StringBuilder();
            sb.AppendJoin(' ', lastName, firstName, patronymic);
            return sb.ToString();
        }

        static void Task02()
        {
            /*
             * Написать программу, принимающую на вход строку — набор чисел, разделенных пробелом,
             * и возвращающую число — сумму всех чисел в строке.
             * Ввести данные с клавиатуры и вывести результат на экран.
             */
            double sum = 0;
            Console.Write("введите набор чисел, разделенных пробелом: ");
            var inputNums = Console.ReadLine();

            foreach (string num in inputNums.Split(' '))
            {
                sum += Convert.ToDouble(num);
            }
            Console.WriteLine(sum);
        }

        static Seasons Task03()
        {
            /*
             * Написать метод по определению времени года. На вход подаётся число – порядковый номер месяца.
             * На выходе — значение из перечисления (enum) — Winter, Spring, Summer, Autumn. Написать метод,
             * принимающий на вход значение из этого перечисления и возвращающий название времени года
             * (зима, весна, лето, осень). Используя эти методы, ввести с клавиатуры номер месяца и вывести
             * название времени года. Если введено некорректное число, вывести в консоль текст «Ошибка: введите число от 1 до 12».
             */
            int month;
            string question01 = "Введите порядковый номер месяца: ";
            string question02 = "Ошибка: введите число от 1 до 12: ";
            string userInput;
            do
            {
                Console.Write(question01);
                userInput = Console.ReadLine();
                question01 = question02;
            } while (!Int32.TryParse(userInput, out month) || month > 12 || month < 1);

            switch (month)
            {
                case 12:
                case 1:
                case 2: return Seasons.Winter;
                case 3:
                case 4:
                case 5: return Seasons.Spring;
                case 6:
                case 7:
                case 8: return Seasons.Summer;
                case 9:
                case 10:
                case 11: return Seasons.Autumn;
                default:
                    throw new ArgumentException();
            }
        }
        enum Seasons
        {
            Winter,
            Spring,
            Summer,
            Autumn
        }

        static void Task04()
        {
            /*
             * Написать программу, вычисляющую число Фибоначчи для заданного значения рекурсивным способом.
             */
            int n = 20;
            Console.WriteLine(Fibo2(n));
        }

        static int Fibo2(int n)
        {
            if (n <= 2)
                return 1;

            return Fibo2(n - 1) + Fibo2(n - 2);
        }

        static void Task05()
        {
            //вычислить факториал не рекурсивным способом

            int n = 10;
            Console.WriteLine(Factorial(n));
        }

        static int Factorial(int n)
        {
            int f = 1;
            while (n > 1)
                f *= n--;
            return f;
        }
    }
}
