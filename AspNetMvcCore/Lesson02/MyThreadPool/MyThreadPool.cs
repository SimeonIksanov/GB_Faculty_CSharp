using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MyThreadPoolLib
{
    public class MyThreadPool
    {
        private const int MIN_SLEEP = 10;

        private static MyThreadPool _myThreadPool;

        private int _maxCapasity = 10;

        private int _minCapasity = 5;

        private Thread _taskScheduler = null;

        private List<MyThreadPoolItem> _threads;

        private ConcurrentQueue<TaskQueueItem> _userTaskQueue;

        private MyThreadPool()
        {
            InitializeTreadPool();
        }

        private void InitializeTreadPool()
        {
            _threads = new List<MyThreadPoolItem>(_minCapasity);
            _userTaskQueue = new ConcurrentQueue<TaskQueueItem>();

            InitPoolWithMinCapacity();

            _taskScheduler = new Thread(TaskSchedulerJob);
            _taskScheduler.IsBackground = true;
            _taskScheduler.Start();
        }

        private void InitPoolWithMinCapacity()
        {
            for (int i = 0; i < _minCapasity; i++)
            {
                var mtpi = new MyThreadPoolItem { State = TaskState.NotStarted };
                mtpi.TaskItem = new TaskQueueItem { task = () => { } };
                mtpi.Thread = new Thread(() =>
                {
                    do
                    {
                        bool Enter = false;

                        if (mtpi.State == TaskState.NotStarted)
                        {
                            mtpi.State = TaskState.Processing;
                            Enter = true;
                        }

                        if (Enter)
                        {
                            try
                            {
                                mtpi.TaskItem.task.Invoke();
                                mtpi.State = TaskState.Completed;
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        Thread.Sleep(MIN_SLEEP);
                    } while (true);
                });
                mtpi.Thread.IsBackground = true;
                _threads.Add(mtpi);
                mtpi.Thread.Start();
            }
        }

        private void TaskSchedulerJob()
        {
            do
            {
                var queueCount = _userTaskQueue.Count;
                if (_userTaskQueue.TryPeek(out TaskQueueItem topQueueItem))
                {
                    foreach (MyThreadPoolItem poolItem in _threads)
                    {
                        if (poolItem.State == TaskState.Completed)
                        {
                            poolItem.TaskItem = topQueueItem;
                            poolItem.State = TaskState.NotStarted;
                            _userTaskQueue.TryDequeue(out _); // единственный инстанс разбирает свою очередь..
                            break;
                        }
                    }
                }
                Thread.Sleep(MIN_SLEEP);
            } while (true);
        }

        public static MyThreadPool Instance
        {
            get
            {
                if (_myThreadPool != null)
                {
                    return _myThreadPool;
                }
                _myThreadPool = new MyThreadPool();
                return _myThreadPool;
            }
        }

        public int Count => _threads.Count;

        public void QueueUserTask(UserTask userTask)
        {
            TaskQueueItem taskQueueItem = new TaskQueueItem { task = userTask };
            _userTaskQueue.Enqueue(taskQueueItem);
        }
    }
}
