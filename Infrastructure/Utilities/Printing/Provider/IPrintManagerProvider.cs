using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    public interface IPrintManagerProvider
    {
        IPrintResult Print(IPrintData printData);
    }
}