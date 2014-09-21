using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading.Scheduler
{
    internal class TaskScheduler : ITaskScheduler
    {
        private readonly int noOfWorkers;
        private readonly ConcurrentQueue<Task> taskQueue;
        private int usedWorkers;

        internal TaskScheduler(int noOfWorkers)
        {
            this.noOfWorkers = noOfWorkers;
            this.usedWorkers = 0;
            this.taskQueue = new ConcurrentQueue<Task>();
        }

        public void NotifyComplete()
        {
            Interlocked.Decrement(ref this.usedWorkers);
            this.ScheduleWork();
        }

        public void Schedule(Task task)
        {
            if (this.TrySchedule(task) == false)
            {
                this.taskQueue.Enqueue(task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CheckFreeWorkers()
        {
            return (this.usedWorkers < this.noOfWorkers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ScheduleTask(Task task)
        {
            Interlocked.Increment(ref this.usedWorkers);
            task.Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ScheduleWork()
        {
            if (this.CheckFreeWorkers())
            {
                Task task = null;
                if (this.taskQueue.TryDequeue(out task))
                {
                    this.ScheduleTask(task);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TrySchedule(Task task)
        {
            bool retValue = false;
            if (this.CheckFreeWorkers())
            {
                this.ScheduleTask(task);
                retValue = true;
            }

            return retValue;
        }
    }
}