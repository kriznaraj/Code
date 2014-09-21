using Controls.Threading.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading.Provider
{
    internal interface IThreadProvider
    {
        Task<TResult> Create<TResult>(IWork<TResult> work);

        Task Create(IWork work);
    }
}