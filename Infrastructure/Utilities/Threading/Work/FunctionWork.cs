using Controls.Threading.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal class FunctionWork<TResult> : Work<TResult>
    {
        private readonly Func<TResult> func;

        internal FunctionWork(ITaskScheduler taskScheduler, Func<TResult> func)
            : base(taskScheduler)
        {
            this.func = func;
        }

        protected override TResult Execute()
        {
            return this.func.Invoke();
        }
    }
}