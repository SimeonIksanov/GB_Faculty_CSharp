using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Lesson08
{
    public static class QuickSorter
    {
        public static void QuickSort(this IList<int> array)
        {
            QuickSort(array, 0, array.Count - 1);
        }

        private static void QuickSort(IList<int> array, int l, int r)
        {
            if (l < r)
            {
                int p = Partition(array, l, r);
                QuickSort(array, l, p - 1);
                QuickSort(array, p + 1, r);
            }
        }

        private static int Partition(IList<int> array, int l, int r)
        {
            int i = l - 1;
            int pivot = array[r];

            for (int j = l; j <= r - 1; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, r);
            return i + 1;
        }

        private static void Swap(IList<int> array, int a, int b)
        {
            int t = array[a];
            array[a] = array[b];
            array[b] = t;
        }
    }
}
