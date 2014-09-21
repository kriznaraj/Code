using Framework.Interfaces;

namespace BallyTech.UI.Web.Framework.Extensions
{
    public class MenuItem : IMenuItem
    {
        public string ModuleName { get; set; }

        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public MenuType MenuType { get; set; }

        public int GroupId { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int SecurityCodeId { get; set; }

        public string Image { get; set; }

        public string MenuSize { get; set; }

        public string CSSClass { get; set; }

    }
}
