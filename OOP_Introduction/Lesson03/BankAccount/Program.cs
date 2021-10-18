using System;

namespace Lesson03
{
    class Program
    {
        static void Main(string[] args)
        {

            Account from = new Account(1000);
            Console.WriteLine(from);

            Account to = new Account(500);
            Console.WriteLine(to);

            Console.WriteLine("Trying to make payment");
            Console.WriteLine(
                to.TakePayment(from, 123) ? "success" : "failure"    
            );
            

            Console.WriteLine(from);
            Console.WriteLine(to);
        }
    }
}
