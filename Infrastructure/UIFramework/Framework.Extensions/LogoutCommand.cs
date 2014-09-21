using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework.Extensions
{
    /// <summary>
    /// An extension of request command to provide log out support.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LogoutCommand<T> : ParameterizedActionCommand<T>
        where T : ViewModelBase
    {
        /// <summary>
        /// Override this method to do anything on logout
        /// </summary>
        /// <param name="executionCtx"></param>
        /// <param name="sessionCtx"></param>
        /// <returns></returns>
        public abstract void DoLogout(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel);

        /// <summary>
        /// Request command implementation for logout.
        /// </summary>
        /// <param name="executionCtx"></param>
        /// <param name="sessionCtx"></param>
        /// <returns></returns>
        public override void Do(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel)
        {
            DoLogout(executionCtx, sessionCtx, viewModel);
            ClearEmployeeDetails(sessionCtx);
        }

        /// <summary>
        /// Clear session on logout
        /// </summary>
        /// <param name="sessionCtx"></param>
        private void ClearEmployeeDetails(ISessionContext sessionCtx)
        {
            SessionStore.Clear();
        }
    }
}