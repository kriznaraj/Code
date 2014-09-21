using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal interface IWork
    {
        void Execute();
    }

    internal interface IWork<TResult>
    {
        TResult Execute();
    }
}