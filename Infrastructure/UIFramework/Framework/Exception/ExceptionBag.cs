using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class ExceptionBag
    {
        private static readonly Dictionary<Int64, IExceptionConfig> _nameExceptionParamMap
           = new Dictionary<Int64, IExceptionConfig>();

        internal static void Add(Int64 errorId, IExceptionConfig @params)
        {
            _nameExceptionParamMap.Add(errorId, @params);
        }

        public static IExceptionConfig Get(string errorId)
        {
            IExceptionConfig exception = null;
            _nameExceptionParamMap.TryGetValue(Int64.Parse(errorId), out exception);
            if (exception == null)
            {
                exception = new JsonException(Int64.Parse(errorId));
                (exception as JsonException).AddCommands("Close","$.unblockUI", "");
            }
            return exception;
        }
    }
}
