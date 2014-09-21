using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Controls.Framework.CustomBinder
{
    /// <summary>
    /// This Custom Json Binder will be usefule to bind the List of Objects from the View to the View Model
    /// </summary>
    public class CustomJsonModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var data = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                JavaScriptSerializer js = new JavaScriptSerializer();
                var temp = js.Deserialize(data.AttemptedValue, bindingContext.ModelType);
                return temp;
            }
            catch
            {
                return null;
            }
        }
    }
}
