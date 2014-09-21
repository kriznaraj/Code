using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading
{
    public interface IThreadPoolFactory
    {
        IThreadPool CreateThreadPool(int noOfWorkers);
    }
}