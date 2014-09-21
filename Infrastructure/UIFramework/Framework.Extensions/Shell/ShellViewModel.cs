using Framework.Interfaces;
using System.Collections.Generic;

namespace BallyTech.UI.Web.Framework.Extensions
{
    public class ShellViewModel : ViewModelBase, IShellView
    {
        public List<IMenuItem> MenuList { get; set; }
        public IUserInfo UserInformation { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string CopyRightInfo { get; set; }
    } 
}
