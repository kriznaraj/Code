using Controls.Threading.Provider;
using Controls.Threading.Scheduler;
using Controls.Threading.Work;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading
{
    internal class ThreadPool : IThreadPool
    {
        private readonly IThreadProvider provider;
        private readonly ITaskScheduler scheduler;

        internal ThreadPool(IThreadProvider provider, int noOfWorkers)
        {
            this.provider = provider;
            this.scheduler = new Controls.Threading.Scheduler.TaskScheduler(noOfWorkers);
        }

        public Task Invoke(Action method)
        {
            var task = this.provider.Create(new ActionWork(this.scheduler, method));
            this.scheduler.Schedule(task);
            return task;
        }

        public Task Invoke<TInput>(Action<TInput> action, TInput input)
        {
            var task = this.provider.Create(new ActionParameterWork<TInput>(this.scheduler, action, input));
            this.scheduler.Schedule(task);
            return task;
        }

        public Task<TResult> Invoke<TResult>(Func<TResult> target)
        {
            var task = this.provider.Create<TResult>(new FunctionWork<TResult>(this.scheduler, target));
            this.scheduler.Schedule(task);
            return task;
        }

        public Task<TResult> Invoke<TInput, TResult>(Func<TInput, TResult> target, TInput input)
        {
            var task = this.provider.Create<TResult>(new FunctionResultWork<TInput, TResult>(this.scheduler, target, input));
            this.scheduler.Schedule(task);
            return task;
        }
    }
}