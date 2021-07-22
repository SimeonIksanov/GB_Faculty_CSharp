using System;
using System.Linq;

namespace Lesson08
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = CreateRandomArray(count: 100, maxValue: 100);
            Console.WriteLine("Unsorted array: ");
            PrintArray(array);

            array.BucketSort(min: 0, max: 100);
            Console.WriteLine("Sorted array: ");
            PrintArray(array);
        }

        static int[] CreateRandomArray(int count, int maxValue)
        {
            Random rnd = new Random();
            return Enumerable.Range(1, count).Select(x => rnd.Next(maxValue)).ToArray();
        }

        static void PrintArray(int[] array) =>
            Console.WriteLine(string.Join(' ', array));
    }
}