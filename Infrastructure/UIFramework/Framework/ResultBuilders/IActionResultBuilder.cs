using System.Web.Mvc;

namespace Controls.Framework
{
    /// <summary>
    /// Utility to create ActionResult
    /// </summary>
    public interface IActionResultBuilder
    {
        ActionResult Build(object data);
    }
}