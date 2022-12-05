using System;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using MyThreadPoolLib;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThreadPool myThreadPool = MyThreadPool.Instance;

            for (int i = 0; i < 30; i++)
            {
                myThreadPool.QueueUserTask(RunInThread);
            }

            Console.ReadLine();
        }

        static void RunInThread()
        {
            Console.WriteLine("Running in thread with Id: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        }
    }
}
