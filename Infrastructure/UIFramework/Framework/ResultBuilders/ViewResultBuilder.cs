using System.Web.Mvc;

namespace Controls.Framework
{
    internal sealed class ViewResultBuilder : ActionResultBuilderBase
    {
        public ViewResultBuilder(string viewName)
            : base(viewName)
        {
        }

        public override ActionResult Build(object data)
        {
            ActionResult actionResult = new System.Web.Mvc.ViewResult() { ViewData = new ViewDataDictionary(data), ViewName = _viewName };
            return actionResult;
        }
    }
}