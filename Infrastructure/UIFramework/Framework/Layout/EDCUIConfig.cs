using Controls.Data;
using Controls.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class EDCUIConfig : IEDCUIConfig
    {
        /// <summary>
        /// Insert an entry to UILayout config table, the data will be pushed from EDC  for reloading the UI layout (Tiles menu) while changing the Inventory.
        /// </summary>
        /// <param name="messageObject"></param>
        public void Post(UILayoutConfig messageObject)
        {
            UILayoutConfigOperation.InsertUIConfigList(messageObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageObject"></param>
        public void Update(UILayoutConfig messageObject)
        {
            UILayoutConfigOperation.UpdateUIConfigList(messageObject);
        }

      
    }
}
