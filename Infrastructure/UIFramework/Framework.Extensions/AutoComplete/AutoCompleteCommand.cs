using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework.Extensions
{
    public abstract class AutoCompleteCommand<T, V> : ProcessCommand<T, V>
        where T : ViewModelBase
        where V : ViewModelBase
    {

        public abstract V GetAutoCompleteItem(T viewModel);


        public override V Process(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel)
        {
            return GetAutoCompleteItem(viewModel);
        }
    }
}
