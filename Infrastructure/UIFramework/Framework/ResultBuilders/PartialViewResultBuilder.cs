using System.Web.Mvc;

namespace Controls.Framework
{
    internal sealed class PartialViewResultBuilder : ActionResultBuilderBase
    {
        public PartialViewResultBuilder(string viewName)
            : base(viewName)
        {
        }

        public override ActionResult Build(object data)
        {
            return new System.Web.Mvc.PartialViewResult() 
            { 
                ViewData = new ViewDataDictionary(data), 
                ViewName = _viewName 
            };
        }
    }
}