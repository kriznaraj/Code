using System;
using Controls.Types;
using Controls.Framework.Interfaces;

namespace Controls.Framework
{
    public abstract class ExecutorCommand: ICommand
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
        public abstract void Do(IExecutionContext executionCtx, ISessionContext sessionCtx);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public virtual Exception ExceptionHandler(Exception exception)
        {
            string errorMSG = ControllerConfigurator.iResourceService.GetLiteral((exception as ModuleException).ErrorCode.ToString());
            IExceptionConfig exceptionConfig = ExceptionBag.Get((exception as ModuleException).ErrorCode.ToString());
            if (exceptionConfig.ResponseType == ResultType.JSON)
            {
                exceptionConfig.ErrorData = exception.Message;
                throw exceptionConfig as JsonException;
            }
            else
            {
                throw exceptionConfig as ViewException;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public  Exception UIExceptionHandler(Exception exception)
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