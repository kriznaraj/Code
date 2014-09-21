using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework.Interfaces
{
    public interface ISiteConfigProvider
    {
        ISiteConfigService Current { get; }
    }
}
