using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading
{
    public enum ThreadProvider
    {
        None = 0,
        ThreadPool,
        UserThread,
    }
}