using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Authorization
{
    public interface IOperationSecurityConfig
    {
        Nullable<Int32> ID { get; }
        
        Nullable<Int32> OperationID { get; }

        Boolean RequiresSession { get; }
    }
}
