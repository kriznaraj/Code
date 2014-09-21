using System.Web.Mvc;

namespace Controls.Framework
{
    internal sealed class JsonResultBuilder : ActionResultBuilderBase
    {
        public JsonResultBuilder(string viewName)
            : base(viewName)
        {
        }

        public override ActionResult Build(object data)
        {
            return new JsonResult() 
            { 
                Data = data, 
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = System.Int32.MaxValue
            };
        }
    }
}