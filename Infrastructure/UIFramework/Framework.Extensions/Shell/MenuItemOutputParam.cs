using System.Collections.Generic;

namespace Controls.Framework.Extensions
{
    public class MenuItemOutputParam : ViewModelBase
    {
        public IEnumerable<object> ResultList { get; set; }

        public IEnumerable<string> UserTaskList { get; set; }
    }
}
