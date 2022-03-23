using System;
using System.Collections.Generic;
using Task02_list;

namespace Task02_List
{
    class Program
    {
        public static void Main()
        {
            MyList<int> testMyList = new MyList<int>();
            List<int> testList = new List<int>();

            StartParallel(() => AddMethod(testMyList));
            Wait();
            Console.WriteLine("my list total count: {0}",testMyList.Count);


            StartParallel(() => AddMethod(testList));
            Wait();
            Console.WriteLine("original list total count: {0}", testList.Count);
        }

        static void StartParallel(Action action)
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => action());
            }
        }

        static void AddMethod(IList<int> list)
        {
            for (int i = 0; i < 500; i++)
            {
                list.Add(i);
            }
        }

        static void Wait()
        {
            if (ThreadPool.PendingWorkItemCount != 0)
            {
                Thread.Sleep(100);
            }
        }
    }
}
