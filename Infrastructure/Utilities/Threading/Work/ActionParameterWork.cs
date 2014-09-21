using Controls.Threading.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal class ActionParameterWork<TInput> : Work
    {
        private readonly Action<TInput> action;
        private readonly TInput input;

        internal ActionParameterWork(ITaskScheduler taskScheduler, Action<TInput> action, TInput input)
            : base(taskScheduler)
        {
            this.action = action;
            this.input = input;
        }

        protected override void Execute()
        {
            this.action.Invoke(this.input);
        }
    }
}