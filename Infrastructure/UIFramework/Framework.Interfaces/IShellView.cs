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
    public interface IShellView
    {
        List<IMenuItem> MenuList { get; set; }
        IUserInfo UserInformation { get; set; }
        string ApplicationName { get; set; }
        string ApplicationVersion { get; set; }
        string CopyRightInfo { get; set; }
    }
}
