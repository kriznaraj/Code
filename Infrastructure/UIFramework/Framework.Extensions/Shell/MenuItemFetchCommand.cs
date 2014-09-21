// Created By :    urajamannar
// Created Time:  11/13/2013 12:48:30 AM

using Controls.Framework;
//using Framework.Interfaces;
using System.Collections.Generic;

namespace Controls.Framework.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MenuItemFetchCommand<T, V> : ProcessCommand<T, V>
        where T : MenuItemInputParam
        where V : MenuItemOutputParam
    {
        public MenuItemFetchCommand()
        {

        }

        public abstract V GetMenuListItem(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionCtx">Execution infomation for current request </param>
        /// <param name="sessionCtx">Current session information </param>
        /// <param name="requestModel">Input for Command </param>
        /// <returns></returns>
        public override V Process(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel)
        {
            return GetMenuListItem(executionCtx, sessionCtx,viewModel);
        }
    }
}
