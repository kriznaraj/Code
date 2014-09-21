using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Security.Cryptography;
using Controls.Configuration;
using Controls.Logging;
using Controls.ExceptionHandling;
using System.Configuration;
using Controls.Framework.Interfaces;
using Microsoft.CSharp;

namespace Controls.Framework
{
    public class ControllerFactory : DefaultControllerFactory, IUserSessionService
    {
        private const string TOKEN = "TOKEN";
        private const string TRANACCOUNT = "TRANACCOUNT";

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            ExecutionContext executionContext = new ExecutionContext();
            ISessionContext sessionContext = null;
            IController controller = null;
            try
            {
                ControllerCreateParams controllerConfig = ControllerBag.Get(controllerName);
                FillExecutionContext(executionContext, requestContext, controllerConfig);
                string token = String.Empty;
                string tranAccount = string.Empty;
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    token = HttpContext.Current.Request.Headers[TOKEN];
                    tranAccount = HttpContext.Current.Request.Headers[TRANACCOUNT];
                }
                else
                {
                    token = HttpContext.Current.Request.Params[TOKEN];
                    tranAccount = HttpContext.Current.Request.Params[TRANACCOUNT];

                }
                sessionContext = Validate(token);

                if (sessionContext != null || controllerConfig.AllowAnonymous)
                {
                    //if (AuthenticateCommand(sessionContext, controllerConfig.TaskCode, controllerConfig.AllowAnonymous))
                    //{
                    SetCulture(sessionContext);
                    dynamic command = Activator.CreateInstance(controllerConfig.CommandType);

                    command.TaskId = controllerConfig.TaskId;
                    command.TranAccount = tranAccount;

                    object resultBuilder = ActionResultBuilderFactory.Create(controllerConfig.ResultBuilder, controllerConfig.ViewName);
                    controller = Activator.CreateInstance(controllerConfig.ControllerType, executionContext, sessionContext, command, resultBuilder) as System.Web.Mvc.Controller;
                    //}
                    //else
                    //{
                    //    throw new FrameworkException(3, "User not authorized to perform this action.");
                    //}

                }
                else if (sessionContext == null && !controllerConfig.AllowAnonymous && !String.IsNullOrEmpty(token))
                {
                    //HttpContext.Current.Response.Redirect("/common_login_logout/Process");
                    throw new FrameworkException(2, "Session expired.");
                }

            }
            catch (FrameworkException exception)
            {
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("ControllerFactory.CreateController", exception);
                controller = GetExceptionController(exception, executionContext, sessionContext);
            }
            catch (Exception exception)
            {
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("ControllerFactory.CreateController", exception);
                throw exception;
            }
            return controller;
        }

        private void SetCulture(ISessionContext sessionContext)
        {
            if (sessionContext == null || sessionContext.UserProfile == null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["culture"]);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["culture"]);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = sessionContext.UserProfile.UserCultureInfo;
                System.Threading.Thread.CurrentThread.CurrentUICulture = sessionContext.UserProfile.UserCultureInfo;
            }
        }

        private IController GetExceptionController(FrameworkException exception, ExecutionContext executionContext, ISessionContext sessionContext)
        {
            System.Web.Mvc.Controller controller = null;
            IExceptionConfig exceptionConfig = ExceptionBag.Get(exception.ErrorId.ToString());
            object command = Activator.CreateInstance(typeof(HandledExceptionCommand), exception);
            object resultBuilder = ActionResultBuilderFactory.Create(exceptionConfig.ResponseType, null);
            Type controllerType = typeof(Requestor<,>).MakeGenericType(
                        new Type[] { typeof(HandledExceptionCommand), typeof(JsonErrorMessage) });
            ISafeBlockProvider safeBlockProvider = ControllerConfigurator.utilityProvider.GetSafeBlockProvider();
            executionContext.SafeActionBlock = safeBlockProvider.Create("UIDefault");
            controller = Activator.CreateInstance(controllerType, executionContext, sessionContext, command, resultBuilder) as System.Web.Mvc.Controller;
            return controller;
        }

        private IExecutionContext FillExecutionContext(ExecutionContext _executionContext, RequestContext requestContext, ControllerCreateParams controllerConfig)
        {
            if (!String.IsNullOrEmpty(controllerConfig.DivId))
            {
                _executionContext.ResponseHeader.Add(Constants.REFRESHDIV, controllerConfig.DivId);
            }
            ISafeBlockProvider safeBlockProvider = ControllerConfigurator.utilityProvider.GetSafeBlockProvider();
            _executionContext.SafeActionBlock = safeBlockProvider.Create(controllerConfig.ExceptionPolicy);
            return _executionContext;
        }

        public ISessionContext Validate(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                ISessionContext sessionInfo = SessionStore.Get<ISessionContext>("SessionContext");
                MD5 crypto = MD5.Create();

                if (sessionInfo == null)
                    return sessionInfo;

                if (Security.VerifyHash(crypto, token, sessionInfo.SessionToken + HttpContext.Current.Session.SessionID))
                {
                    return sessionInfo;
                }
                else
                {
                    throw new FrameworkException(1, "Invalid Session.Logging Out");
                }
            }
            else
                return null;
        }

        public bool Authorize(IExecutionContext exeContext)
        {
            //TODO: Check user authorization
            return true;
        }

        public bool AuthenticateCommand(ISessionContext sessionContext, string taskCode, bool isAnonymousUser)
        {
            bool result = false;
            if (string.IsNullOrEmpty(taskCode) || isAnonymousUser)
            {
                return true;
            }

            if (sessionContext != null && sessionContext.UserTask != null)
            {
                var userTask = sessionContext.UserTask.Find(o => o.TaskCode == taskCode);
                if (userTask != null)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}