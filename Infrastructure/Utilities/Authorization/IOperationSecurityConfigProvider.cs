using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Authorization
{
    public interface IOperationSecurityConfigProvider
    {
        IOperationSecurityConfig Get(string apiName);
    }
}