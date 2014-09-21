using Controls.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public interface IEDCUIConfig
    {
        /// <summary>
        /// Insert an entry to UILayout config table, the data will be pushed from EDC  for reloading the UI layout (Tiles menu) while changing the Inventory.
        /// </summary>
        /// <param name="messageObject"></param>
        void Post(UILayoutConfig messageObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageObject"></param>
        void Update(UILayoutConfig messageObject);
    }
}
