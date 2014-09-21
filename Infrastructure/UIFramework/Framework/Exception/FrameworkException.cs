using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class FrameworkException : Exception
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

        public FrameworkException(long errorId)
        {
            this.ErrorId = errorId;
        }

        public FrameworkException(long error, string errorMessage):base(errorMessage)
        {
            this.ErrorId = error;
            this.ErrorMessage = errorMessage;
        }

        public FrameworkException(long error, string errorMessage, Exception exception):base(errorMessage,exception)
        {
            this.ErrorId = error;
            this.ErrorMessage = errorMessage;
        }
        
    }
}
