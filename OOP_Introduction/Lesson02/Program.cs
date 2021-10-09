using System;

namespace Lesson02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Account());
            Console.WriteLine(new Account(AccountType.Credit));
            Console.WriteLine(new Account(150));
            Console.WriteLine(new Account(AccountType.Deposit, 1100));

            var testAccount = new Account(AccountType.Deposit, 12_792);

            testAccount.Withdraw(792);
            testAccount.Deposit(1000);
            Console.WriteLine(testAccount);
        }
    }
}
