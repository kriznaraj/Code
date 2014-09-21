using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Controls.Framework
{
    public interface IExceptionConfig
    {
        long ErrorId { get; set; }
        ResultType ResponseType { get; }
        object ErrorData { get; set; }

        ActionResult BuildException(IExceptionConfig exceptionConfig);
    }
}
