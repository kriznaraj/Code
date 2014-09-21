using System;
using System.Xml.Serialization;

namespace Controls.ExceptionHandling
{
    [Serializable]
    [XmlRoot("ExceptionHandlerConfig")]
    public sealed class ExceptionHandlerConfig
    {
        /// <summary>
        /// Type of Exception for which the handler is has been defined.
        /// </summary>
        [XmlIgnore]
        public Type ExceptionType
        {
            get
            {
                return Type.GetType(this.ExceptionTypeName, true);
            }
        }

        [XmlAttribute("exceptionType")]
        public string ExceptionTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// Action to perform after the Exception Handler had handled the exception.
        /// </summary>
        [XmlAttribute("handlerAction")]
        public PostHandleAction HandlerAction
        {
            get;
            set;
        }

        /// <summary>
        /// The index at which this handler should be invoked
        /// </summary>
        [XmlAttribute("invokeSequence")]
        public int InvokeSequence
        {
            get;
            set;
        }

        /// <summary>
        /// Type of exception handler, can of type WrapHandler, ReplaceHandler, LoggingHandler
        /// </summary>
        [XmlIgnore]
        public Type Type
        {
            get
            {
                return Type.GetType(this.TypeName, true);
            }
        }

        [XmlAttribute("type")]
        public string TypeName
        {
            get;
            set;
        }
    }
}