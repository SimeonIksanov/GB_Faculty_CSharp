using System;
using System.Threading;

namespace MyThreadPoolLib
{
    internal class MyThreadPoolItem
    {
        internal TaskQueueItem TaskItem { get; set; }
        internal Thread Thread { get; set; }
        internal TaskState State { get; set; }
    }
}
