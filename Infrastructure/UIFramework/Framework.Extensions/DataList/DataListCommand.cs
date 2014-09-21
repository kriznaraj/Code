using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework.Extensions
{
    public abstract class DataListCommand<T, V> : ProcessCommand<T, V>
        where T : DataListInputParam
        where V : DataListOutputParam
    {

        public abstract V GetDataListItem(T viewModel);


        public override V Process(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel)
        {
            return GetDataListItem(viewModel);
        }
    }
}
