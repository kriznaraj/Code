using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class AccessPolicyServiceFactory
    {
        public static IAccessPolicyProvider Create()
        {
            return new AccessPolicyProvider();
        }
    }
}
