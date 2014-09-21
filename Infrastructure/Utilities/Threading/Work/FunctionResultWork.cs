using Controls.Threading.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal class FunctionResultWork<TInput, TResult> : Work<TResult>
    {
        private readonly Func<TInput, TResult> func;
        private readonly TInput input;

        internal FunctionResultWork(ITaskScheduler taskScheduler, Func<TInput, TResult> func, TInput input)
            : base(taskScheduler)
        {
            this.func = func;
            this.input = input;
        }

        protected override TResult Execute()
        {
            return this.func.Invoke(this.input);
        }
    }
}