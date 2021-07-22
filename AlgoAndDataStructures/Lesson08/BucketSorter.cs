using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lesson08
{
    public static class BucketSorter
    {

        public static void BucketSort(this int[] array, int min, int max)
        {
            static int FindIndex(int[] array, int bucketCount, int min, int max, int i)
            { // формулу взял в википедии
                return (int)Math.Floor((double)(bucketCount * (array[i] - min) / (max - min)));
            }

            int bucketCount = 10;
            //int min = 0, max = 100;
            List<int>[] buckets = new List<int>[bucketCount];

            for (int i = 0; i < bucketCount; i++)
                buckets[i] = new List<int>();

            for (int i = 0; i < array.Length; i++)
                buckets[FindIndex(array, bucketCount, min, max, i)].Add(array[i]);

            int index = 0;
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i].Sort();
                for (int j = 0; j < buckets[i].Count; j++)
                    array[index++] = buckets[i][j];
            }

        }
    }
}
