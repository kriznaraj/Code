using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Controls.Framework
{
    public class ExceptionCommand : RequestCommand<ExceptionViewModel>
    {
        private IExceptionConfig frameworkException = null;

        public ExceptionCommand(IExceptionConfig frameworkException)
        {
            this.frameworkException = frameworkException;
        }

        public override ExceptionViewModel Get(IExecutionContext executionCtx, ISessionContext sessionCtx)
        {
            executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "True");
            executionCtx.ResponseHeader.Add(Constants.REFRESHDIV, "#errordiv");
            return new ExceptionViewModel();
        }
    }
}
