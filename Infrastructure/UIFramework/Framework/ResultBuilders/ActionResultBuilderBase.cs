using System.Web.Mvc;

namespace Controls.Framework
{
    internal abstract class ActionResultBuilderBase : IActionResultBuilder
    {
        protected readonly string _viewName;

        internal ActionResultBuilderBase(string viewName)
        {
            _viewName = viewName;
        }

        public abstract ActionResult Build(object data);
    }
}