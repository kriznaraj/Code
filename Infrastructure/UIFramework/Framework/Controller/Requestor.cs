using Controls.ControlLibrary;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Controls.Configuration;
using Controls.ExceptionHandling;
using Controls.Logging;
using Controls.Types;

namespace Controls.Framework
{
    public sealed class Requestor<TCommand, TOutput> : System.Web.Mvc.Controller
        where TCommand : RequestCommand<TOutput>
        where TOutput : ViewModelBase
    {
        private ActionResult _actionResult = null;
        private readonly TCommand _command;
        private readonly IActionResultBuilder _resultBuilder;
        private readonly IExecutionContext _executionCtx;
        private readonly ISessionContext _sessionCtx;

        public Requestor(IExecutionContext executionCtx, ISessionContext sessionCtx, TCommand command, IActionResultBuilder resultBuilder)
        {
            _command = command;
            _resultBuilder = resultBuilder;
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
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Requestor.Process.ViewException", viewException);
                _actionResult = viewException.BuildException(viewException);
                _executionCtx.ResponseHeader.Add(Constants.EXCEPTIONHEADER, "VIEW");
            }
            catch (JsonException jsonException)
            {
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Requestor.Process.JsonException", jsonException);
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
                TOutput response = _command.Get(_executionCtx, _sessionCtx);

                //Setting the configuration key in model from the command
                if (response != null)
                    response.ConfigurationKey = _command.GetConfigurationKey();

                _actionResult = _resultBuilder.Build(response);
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