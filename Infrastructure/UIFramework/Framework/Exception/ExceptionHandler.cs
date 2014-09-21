using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BallyTech.UI.Web.Framework
{
    public static class ExceptionHandler
    {
        public static ActionResult HandleException(FrameworkException frameworkException, IExecutionContext executionCtx, ISessionContext sessionCtx)
        {
            IExceptionConfig expConfig = ExceptionBag.Get(frameworkException.ErrorId.ToString());
            IActionResultBuilder exceptionBuilder = ActionResultBuilderFactory.Create(expConfig.ResponseType, null);
            ExceptionCommand exceptionCommand = new ExceptionCommand(expConfig);
            ExceptionViewModel exceptionModel = exceptionCommand.Get(executionCtx, sessionCtx);
            return exceptionBuilder.Build(exceptionModel);
        }

    }
}
