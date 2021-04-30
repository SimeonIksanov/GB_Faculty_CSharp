using System;
using System.Globalization;
using System.Linq;

namespace Lesson02
{
    class Program
    {
        static void Main(string[] args)
        {
            Task01();
            Task02();
            Task03();
            Task04();

            ExtraTask();
        }

        [Flags]
        enum Weekday : byte
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64,
        }

        private static void Task01()
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine("Запросить у пользователя минимальную и максимальную температуру за сутки и вывести среднесуточную температуру");
            Console.WriteLine("Запросить у пользователя порядковый номер текущего месяца и вывести его название");
            Console.WriteLine("Если пользователь указал месяц из зимнего периода, а средняя температура > 0, вывести сообщение «Дождливая зима»");
            Console.WriteLine();

            double minTemp = AskTemperature("Введите минимальную температуру за сутки: ");
            double maxTemp = AskTemperature("Введите максимальную температуру за сутки: ");
            int monthNumber = AskCurrentMonth();
            Console.WriteLine();

            double mid = (minTemp + maxTemp) / 2;

            Console.WriteLine("Сейчас {0}", new DateTime(2021, monthNumber, 1).ToString("MMMM", new CultureInfo("ru-RU")));
            Console.WriteLine("Средняя температура за сутки: {0}", mid);
            if (mid > 0 && new[] { 1, 2, 3, 11, 12 }.Contains(monthNumber))
                Console.WriteLine("Дождливая зима");
        }

        private static double AskTemperature(string question)
        {
            double temp;
            string asString = string.Empty;
            do
            {
                Console.Write(question);
                asString = Console.ReadLine().Replace(',', '.');
            } while (!double.TryParse(asString, NumberStyles.Any, new NumberFormatInfo() { CurrencyDecimalSeparator = "." }, out temp));
            return temp;
        }

        private static int AskCurrentMonth()
        {
            int num;
            string asString = string.Empty;
            do
            {
                Console.Write("Введите номер месяца, 1-12: ");
                asString = Console.ReadLine();
            } while (!int.TryParse(asString, out num) || num < 1 || num > 12);
            return num;
        }

        private static void Task02()
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine("Определить, является ли введённое пользователем число чётным");
            Console.WriteLine();

            int num;
            string asString = string.Empty;
            do
            {
                Console.Write("Введите целое число: ");
                asString = Console.ReadLine();
            } while (!int.TryParse(asString, out num));

            bool isEven = num % 2 == 0;
            Console.WriteLine("Число {0} - {1}", num, isEven ? "четное" : "нечетное");
        }

        private static void Task03()
        {
            Console.WriteLine("Task 3:");
            Console.WriteLine("Схематично нарисуйте чек в консоли");
            Console.WriteLine();
            string date = DateTime.Today.ToString("dd.MM.yy"), time = DateTime.Now.ToString("HH:mm:ss");
            string cardNo = "1234567890123456";
            int cash = 15_000;
            int operationNo = 1442;
            int bankomat = 60003344;
            int authCode = 129471;
            string str = $@"
            ПАО СБЕРБАНК
          ул Репина, 94
    ТЕЛЕФОН СЛУЖБЫ ПОМОЩИ КЛИЕНТАМ:
    900 (БЕСПЛАТНО С МОБИЛЬНЫХ ПО РФ)
        +7 495 500 55 50
          ВЗНОС НАЛИЧНЫХ
{date}                {time}
ВНЕСЕНО НА КАРТУ:     {cash}  RUB
КАРТА: {cardNo}

НОМЕР ОПЕРАЦИИ: {operationNo}
БАНКОМАТ: {bankomat}
КОД АВТОРИЗАЦИИ: {authCode}


            СПАСИБО
";
            Console.WriteLine(str);
        }

        private static void Task04()
        {
            Console.WriteLine("Task 4:");
            Console.WriteLine("Создать универсальную структуру расписания недели");
            Console.WriteLine();

            var officeWorkingDays = Weekday.Monday | Weekday.Tuesday | Weekday.Thursday | Weekday.Wednesday | Weekday.Friday;
            Console.WriteLine("дни работы офиса 1: {0}", officeWorkingDays);

            officeWorkingDays = Weekday.Friday | Weekday.Saturday | Weekday.Sunday;
            Console.WriteLine("дни работы офиса 2: {0}", officeWorkingDays);
        }

        private static void ExtraTask()
        {
            Console.WriteLine("Extra Task:");
            Console.WriteLine("Перевернуть введенное число, 123 -> 321");
            Console.WriteLine();

            Console.WriteLine("введите число: ");
            var strNum = Console.ReadLine();
            try
            {
                int num = Convert.ToInt32(strNum);
                Console.WriteLine(Turnover(num));

            }
            catch (OverflowException)
            {
                Console.WriteLine("Okay, Houston...we've had a problem here");
                Console.WriteLine("too much");
            }
        }

        private static int Turnover(int num)
        {
            string resultAsString = new string(num.ToString().Reverse().ToArray());
            int result;
            if (Int32.TryParse(resultAsString, out result))
                return result;
            else
                throw new OverflowException();
        }
    }
}
