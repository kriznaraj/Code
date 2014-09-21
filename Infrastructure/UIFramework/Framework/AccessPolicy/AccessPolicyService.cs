using Controls.Types;
using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Controls.Framework
{
    public class AccessPolicyService : IAccessPolicyService
    {
        /// <summary>
        /// 
        /// </summary>
        private List<IUserTask> securityList = null;

        /// <summary>
        ///         /// </summary>
        public AccessPolicyService(List<IUserTask> userTask)
        {
            //Guard.NotNull(userTask, "UserTask", -1, "User Task list cannot be null");
            //Guard.NonZero(userTask.Count, "UserTask", -1, "User Task List count cannot be zero");
            securityList = userTask;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        public bool GetAccessPolicy(string securityCode)
        {
            if (securityList != null && securityList.Count > 0)
            {
                return securityList.Where(o => o.TaskCode == securityCode).Count() > 0 ? true : false;
            }

            return false;
        }
    }
}
