
using System;
using Controls.Types;
using Controls.ControlLibrary;
using Controls.Framework.Interfaces;
using Controls.Security;
namespace Controls.Framework
{
    public abstract class RequestCommand<T>:ICommand
        where T : ViewModelBase
    {
        #region "Properties"

        protected internal int TaskId { get; set; }

        protected internal string TranAccount { get; set; }

        #endregion

        /// <summary>
        /// Request Command
        /// </summary>
        /// <param name="executionCtx"></param>
        /// <param name="sessionCtx"></param>
        /// <returns></returns>
        public abstract T Get(IExecutionContext executionCtx, ISessionContext sessionCtx);

        /// <summary>
        /// Module Exception Handler
        /// </summary>
        /// <param name="exception"></param>
        public virtual Exception ExceptionHandler(Exception exception)
        {
            string errorMSG = ControllerConfigurator.iResourceService.GetLiteral((exception as ModuleException).ErrorCode.ToString());
            IExceptionConfig exceptionConfig = ExceptionBag.Get((exception as ModuleException).ErrorCode.ToString());
            if (exceptionConfig.ResponseType == ResultType.JSON)
            {
                exceptionConfig.ErrorData = exception.Message;
                return exceptionConfig as JsonException;
            }
            else
            {
                return exceptionConfig as ViewException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public virtual void SetConfigurationKey()
        {
            this.ConfigKey = string.Empty;
        }

        /// <summary>
        /// UI Unhandled Exception Handler
        /// </summary>
        /// <param name="exception"></param>
        public Exception UIExceptionHandler(Exception exception)
        {
            ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Executor.ExecuteProcess", exception);
            JsonException jsonException = ExceptionBag.Get(Constants.WEB_UNHANDLED_EXCEPTIONID.ToString()) as JsonException;
            jsonException.ErrorData = exception.Message;
            throw jsonException;
        }

        #region "ICommand - Implementation"

        public string ConfigKey 
        { 
            get; 
            protected set; 
        }

        public string GetConfigurationKey()
        {
            this.SetConfigurationKey();
            return this.ConfigKey;
        }

        public int GetTaskId()
        {
            return this.TaskId;
        }

        public void SetTaskId(int taskId)
        {
            this.TaskId = taskId;
        }

        public string GetTranAccount()
        {
            return this.TranAccount;
        }

        public void SetTranAccount(string tranAccount)
        {
            this.TranAccount = tranAccount;
        }

        #endregion
    }
}