using System;

namespace Controls.Framework
{
    internal sealed class ControllerCreateParams
    {

        public Type CommandType
        {
            get;
            set;
        }

        public Type ControllerType
        {
            get;
            set;
        }

        public Type RequestViewModel
        {
            get;
            set;
        }

        public Type ReponseViewModel
        {
            get;
            set;
        }

        public ResultType ResultBuilder
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ViewName
        {
            get;
            set;

        }

        public string DivId
        {
            get;
            set;
        }

        public bool AllowAnonymous
        {
            get;
            set;
        }

        public string ExceptionPolicy
        {
            get;
            set;
        }

        public int TaskId
        {
            get;
            set;
        }
    }
}
