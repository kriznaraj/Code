using Controls.Threading.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal abstract class Work : IWork
    {
        private readonly ITaskScheduler taskScheduler;

        protected Work(ITaskScheduler taskScheduler)
        {
            this.taskScheduler = taskScheduler;
        }

        void IWork.Execute()
        {
            try
            {
                this.Execute();
            }
            finally
            {
                this.taskScheduler.NotifyComplete();
            }
        }

        protected abstract void Execute();
    }

    internal abstract class Work<TResult> : IWork<TResult>
    {
        private readonly ITaskScheduler taskScheduler;

        protected Work(ITaskScheduler taskScheduler)
        {
            this.taskScheduler = taskScheduler;
        }

        TResult IWork<TResult>.Execute()
        {
            TResult result = default(TResult);
            try
            {
                result = this.Execute();
            }
            finally
            {
                this.taskScheduler.NotifyComplete();
            }

            return result;
        }

        protected abstract TResult Execute();
    }
}