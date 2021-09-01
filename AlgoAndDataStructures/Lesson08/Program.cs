using System;
using System.Linq;

namespace Lesson08
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array;
            array = CreateRandomArray(count: 100, maxValue: 1000);
            //array = new[] { 5, 7, 1, 4, 3, 9, 2, 8, 6, 0 };
            //array = new[] { 1, 3, -1, 4, 2, 5 };
            Console.WriteLine("Unsorted array: ");
            PrintArray(array);

            Console.WriteLine("Sorted array: ");

            #region QuickSort
            array.QuickSort();
            #endregion

            #region MergeSort
            //array.MergeSort();
            #endregion

            #region HeapSort
            //array.HeapSort();
            #endregion

            #region BucketSort
            //array.BucketSort(min: 0, max: 100);
            #endregion

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