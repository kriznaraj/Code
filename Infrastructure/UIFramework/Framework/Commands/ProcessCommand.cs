using Controls.ControlLibrary;
using System;
using Controls.Types;
using Controls.Framework.Interfaces;
using Controls.Security;

namespace Controls.Framework
{
    public abstract class ProcessCommand<T, V> : ICommand
        where T : ViewModelBase
        where V : ViewModelBase
    {
        #region "Properties"

        protected internal int TaskId { get; set; }

        protected internal string TranAccount { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionCtx"></param>
        /// <param name="sessionCtx"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public abstract V Process(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public virtual Exception ExceptionHandler(Exception exception)
        {
            string errorMSG =ControllerConfigurator.iResourceService.GetLiteral((exception as ModuleException).ErrorCode.ToString());
            IExceptionConfig exceptionConfig = ExceptionBag.Get((exception as ModuleException).ErrorCode.ToString());
            if (exceptionConfig.ResponseType == ResultType.JSON)
            {
                exceptionConfig.ErrorData = errorMSG;
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
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public Exception UIExceptionHandler(Exception exception)
        {
            ControllerConfigurator.utilityProvider.GetLogger().LogFatal("Processor.ExecuteProcess", exception);
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