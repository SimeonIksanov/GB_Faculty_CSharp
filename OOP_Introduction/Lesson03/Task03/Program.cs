using System;

namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            new AddressBookProcessor().Process("InputData.txt", "Output.txt");
        }
    }
}
