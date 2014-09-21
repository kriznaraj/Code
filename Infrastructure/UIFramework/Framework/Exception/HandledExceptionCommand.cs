using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class HandledExceptionCommand : RequestCommand<JsonErrorMessage>
    {
        private FrameworkException frameworkException = null;

        public HandledExceptionCommand(FrameworkException frameworkException)
        {
            this.frameworkException = frameworkException;
        }

        public override JsonErrorMessage Get(IExecutionContext executionCtx, ISessionContext sessionCtx)
        {
            executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "JSON");
            if (frameworkException != null)
            {
                IExceptionConfig exceptionConfig = ExceptionBag.Get(frameworkException.ErrorId.ToString());
                JsonErrorMessage exceptionMessage = new JsonErrorMessage() { Message = frameworkException.ErrorMessage, ActionCommand = (exceptionConfig as JsonException).ActionConfig };
                return exceptionMessage;
            }
            else
            {
                throw new System.Exception("Exception Configuration not found");
            }
        }
    }
}
