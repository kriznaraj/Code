using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class ActionResultBuilderFactory
    {
        public static IActionResultBuilder Create(ResultType type, string viewName)
        {
            switch (type)
            {
                case ResultType.JSON:
                    return new JsonResultBuilder(viewName);
                case ResultType.PartialView:
                    return new PartialViewResultBuilder(viewName);
                case ResultType.View:
                    return new ViewResultBuilder(viewName);
                default:
                    return new JsonResultBuilder(viewName);
            }
        }
    }
}
