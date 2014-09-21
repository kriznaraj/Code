using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserInfo
    {
        string UserName { get; set; }
        string RoleName { get; set; }
        string LoginLocation { get; set; }
        string ProfileImageUrl { get; set; }
    }
}
