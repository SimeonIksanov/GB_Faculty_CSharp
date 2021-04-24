using System;

namespace Lesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Как ваше имя?: ");
            string username = Console.ReadLine();
            string todayDate = DateTime.Today.ToString("dd-MM-yyyy");
            Console.WriteLine($"Привет, {username}, сегодня {todayDate}");
        }
    }
}
