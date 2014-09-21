using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Controls.Framework
{

    public class ViewException : Exception, IViewExceptionConfig
    {
        private const string DEFAULTVIEW = "Error";
        public ViewException()
        {
            ResponseType = ResultType.PartialView;
        }
        public long ErrorId { get; set; }
        public ResultType ResponseType { get; private set; }
        public string ViewName { get; set; }
        public Dictionary<string, ExceptionActionConfig> ViewConfig { get; set; }
        public object ErrorData { get; set; }


        public ActionResult BuildException(IExceptionConfig exceptionConfig)
        {
            string viewName = ((ViewException)exceptionConfig).ViewName;
            IActionResultBuilder resultBuilder = ActionResultBuilderFactory.Create(exceptionConfig.ResponseType, viewName);
            return resultBuilder.Build(exceptionConfig.ErrorData);
        }
    }


}
