using System;
using System.Collections;
using System.Collections.Generic;

namespace Lesson08
{
    public static class MergeSorter
    {
        public static void MergeSort(this IList<int> array)
        {
            MergeSort(array, 0, array.Count - 1);
        }

        private static void MergeSort(IList<int> array, int l, int r)
        {
            if (l < r)
            {
                int m = (r - l) / 2 + l;
                MergeSort(array, l, m);
                MergeSort(array, m + 1, r);
                Merge(array, l, m, r);
            }
        }

        private static void Merge(IList<int> array, int l, int m, int r)
        {
            int[] newArray = new int[r - l + 1];
            int i = l;
            int j = m + 1;
            int min, index = 0;

            while (i <= m && j <= r)
            {
                min = array[i] < array[j] ? array[i++] : array[j++];
                newArray[index++] = min;
            }
            // copy remaining 'tail' of longer array
            for (int k = i; k <= m; k++)
            {
                newArray[index++] = array[k];
            }

            for (int k = j; k <= r; k++)
            {
                newArray[index++] = array[k];
            }

            for (int k = l; k <= r; k++)
                array[k] = newArray[k - l];
        }
    }
}
