using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading
{
    public interface IThreadPool
    {
        Task Invoke(Action method);

        Task Invoke<TInput>(Action<TInput> action, TInput input);

        Task<TResult> Invoke<TResult>(Func<TResult> target);

        Task<TResult> Invoke<TInput, TResult>(Func<TInput, TResult> target, TInput input);
    }
}