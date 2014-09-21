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
    public interface IMenuItem
    {
        string ModuleName { get; set; }

        int MenuId { get; set; }

        string MenuName { get; set; }

        MenuType MenuType { get; set; }

        int GroupId { get; set; }

        string ControllerName { get; set; }

        string ActionName { get; set; }

        int SecurityCodeId { get; set; }

        string Image { get; set; }

        string MenuSize { get; set; }
    }
}
