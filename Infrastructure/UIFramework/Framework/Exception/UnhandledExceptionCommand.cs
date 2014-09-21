using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class UnhandledExceptionCommand : ProcessCommand<ExceptionViewModel, JsonErrorMessage>
    {

        public override JsonErrorMessage Process(IExecutionContext executionCtx, ISessionContext sessionCtx, ExceptionViewModel viewModel)
        {
            string uri = "common_login_logout\\Process";
            executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "JSON");
            JsonErrorMessage exceptionMessage = new JsonErrorMessage { Message = "Unable to process request.Logging Out..", ActionCommand = new List<ExceptionActionConfig>() { new ExceptionActionConfig() { Name = "Close", URI = uri } } };
            return exceptionMessage;
        }
    }
}
