using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal interface IPrintServiceNotify
    {
        void NotifyHandlerNotFound(IPrintResult printResponse);
    }
}