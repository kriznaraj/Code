using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.UI.Web.Framework
{
    public class BaseException : System.Exception
    {
        public long ErrorId
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            private set;
        }

        public object ErrorData
        {
            get;
            private set;
        }

        public SeverityLevel Severity
        {
            get;
            private set;
        }

        public BaseException()
        {
        }

        public BaseException(long error, string errorMessage):base(errorMessage)
        {
            this.ErrorId = error;
            this.ErrorMessage = errorMessage;
        }

        public BaseException(long error, string errorMessage, SeverityLevel serverityLevel)
        {
            this.ErrorId = error;
            this.ErrorMessage = errorMessage;
            this.Severity = serverityLevel;
        }
    }
}

