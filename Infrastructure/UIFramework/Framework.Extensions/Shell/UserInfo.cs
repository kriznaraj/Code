using Framework.Interfaces;

namespace BallyTech.UI.Web.Framework.Extensions
{
    public class UserInfo : IUserInfo
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string LoginLocation { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
