
using System.Collections.Generic;
namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccessPolicyService 
    {
        /// <summary>
        /// Gets the access policy for given key based on configuration
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        bool GetAccessPolicy(string securityCode);       
    }
}
