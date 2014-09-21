using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Controls.Configuration;
using Controls.ExceptionHandling;
using Controls.Logging;
using Controls.Types;

namespace Controls.Framework
{
    public sealed class Executor<TCommand> : System.Web.Mvc.Controller
        where TCommand : ExecutorCommand
    {
        private ActionResult _actionResult = null;
        private readonly TCommand _command;
        private readonly IExecutionContext _executionCtx;
        private readonly ISessionContext _sessionCtx;

        public Executor(IExecutionContext executionCtx, ISessionContext sessionCtx, TCommand command, IActionResultBuilder resultBuilder)
        {
            this._command = command;
            _executionCtx = executionCtx;
            _sessionCtx = sessionCtx;
        }

        public ActionResult Process()
        {
            try
            {
                _executionCtx.SafeActionBlock.Invoke(this.ExecuteProcess, null, null);
            }
            catch (ViewException viewException)
            {
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Executor.Process.ViewException", viewException);
                _actionResult = viewException.BuildException(viewException);
                _executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "VIEW");
            }
            catch (JsonException jsonException)
            {
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Executor.Process.JsonException", jsonException);
                _actionResult = jsonException.BuildException(jsonException);
                _executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "JSON");
            }
            finally
            {
                BuildResponseHeader();
            }
            return _actionResult;
        }

        private void BuildResponseHeader()
        {
            foreach (KeyValuePair<string, string> header in _executionCtx.ResponseHeader)
            {
                Response.AppendHeader(header.Key, header.Value);
            }
        }

        private void ExecuteProcess()
        {
            try
            {
                _command.Do(_executionCtx, _sessionCtx);
            }
            catch (ModuleException moduleException)
            {
                throw _command.ExceptionHandler(moduleException);
            }
            catch (Exception exception)
            {
                throw _command.UIExceptionHandler(exception);
            }

        }
    }
}