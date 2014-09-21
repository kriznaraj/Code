using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class AccessPolicyProvider : IAccessPolicyProvider
    {
        public IAccessPolicyService Current
        {
            get
            {
                return new AccessPolicyService(SessionStore.Get<SessionContext>("SessionContext") != null ? SessionStore.Get<SessionContext>("SessionContext").UserTask : null);
            }
        }
    }
}
