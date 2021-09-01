using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lesson08
{
    public static class HeapSorter
    {
        public static void HeapSort(this IList<int> array)
        {
            void Heapify(IList<int> array, int length, int start)
            {
                int largest = start;
                int left = 2 * start + 1;
                int right = 2 * start + 2;

                if (left < length && array[left] > array[largest])
                    largest = left;

                if (right < length && array[right] > array[largest])
                    largest = right;

                if (largest != start)
                {
                    Swap(array, start, largest);
                    Heapify(array, length, largest);
                }
            }

            for (int i = array.Count / 2; i >= 0; i--)
                Heapify(array, array.Count, i);

            for (int i = array.Count - 1; i >= 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                Heapify(array, i, 0);
            }
        }

        private static void Swap(IList<int> array, int item1, int item2)
        {
            int temp = array[item1];
            array[item1] = array[item2];
            array[item2] = temp;
        }
    }
}
