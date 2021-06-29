using System;

namespace Lesson01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            Console.WriteLine("write function with block schema");
            Console.Write("Is '3' prime number?: "); Console.WriteLine(IsPrime(3));
            Console.Write("Is '101' prime number?: "); Console.WriteLine(IsPrime(101));
            Console.Write("Is '27' prime number?: "); Console.WriteLine(IsPrime(27));
            Console.WriteLine();

            Console.WriteLine("Task 2");
            Console.WriteLine("Function complexity is O(N^3)");
            Console.WriteLine();

            Console.WriteLine("Task 3");
            Console.WriteLine("fibonachi with loop and reqursion test cases: ");

            TestFiboCase testFibo;

            testFibo = new TestFiboCase() { X = 3, Expected = 2, ExpectedException = null };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);

            testFibo = new TestFiboCase() { X = 10, Expected = 55, ExpectedException = null };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);

            testFibo = new TestFiboCase() { X = 20, Expected = 6765, ExpectedException = null };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);

            testFibo = new TestFiboCase() { X = 0, Expected = 0, ExpectedException = new ArgumentOutOfRangeException() };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);

            // not valid test
            testFibo = new TestFiboCase() { X = 1, Expected = 10, ExpectedException = null };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);

            testFibo = new TestFiboCase() { X = 11, Expected = 88, ExpectedException = null };
            TestFiboLoop(testFibo);
            TestFiboReqursion(testFibo);
        }

        static int ReadInt()
        {
            int n;
            string input;
            do
            {
                Console.Write("Input number: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out n));

            return n;
        }

        static bool IsPrime(int n)
        {
            int d = 0, i = 2;

            while (i < n)
            {
                if (n % i == 0)
                    d++;
                i++;
            }

            if (d == 0)
                return true;
            else
                return false;
        }

        static int StrangeSum(int[] inputArray)
        {
            int sum = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = 0; j < inputArray.Length; j++)
                {
                    for (int k = 0; k < inputArray.Length; k++)
                    {
                        int y = 0;

                        if (j != 0)
                        {
                            y = k / j;
                        }

                        sum += inputArray[i] + i + k + j + y;
                    }
                }
            }

            return sum;
        }

        static int FiboRecursion(int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException("number must be greater then 0");
            if (n <= 2) return 1;
            return FiboRecursion(n - 1) + FiboRecursion(n - 2);
        }

        static int FiboLoop(int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException("number must be greater then 0");
            int n1 = 1, n2 = 1;
            if (n <= 2) return 1;
            while (n-- > 2)
            {
                int t = n1 + n2;
                n1 = n2;
                n2 = t;
            }
            return n2;
        }

        static void TestFiboLoop(TestFiboCase testCase)
        {
            try
            {
                var actual = FiboLoop(testCase.X);

                if (actual == testCase.Expected)
                {
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
            catch (Exception ex)
            {
                if (testCase.ExpectedException != null)
                {
                    //TODO add type exception tests;
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
        }

        static void TestFiboReqursion(TestFiboCase testCase)
        {
            try
            {
                var actual = FiboRecursion(testCase.X);

                if (actual == testCase.Expected)
                {
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
            catch (Exception ex)
            {
                if (testCase.ExpectedException != null)
                {
                    //TODO add type exception tests;
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("INVALID TEST");
                }
            }
        }

        public class TestFiboCase
        {
            public int X { get; set; }
            public int Expected { get; set; }
            public Exception ExpectedException { get; set; }
        }


    }
}
