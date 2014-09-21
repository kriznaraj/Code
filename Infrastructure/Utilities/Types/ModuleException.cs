using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Types
{
    /// <summary>
    /// Exception to be thrown for all business validation failure
    /// </summary>
    public class ModuleException : Exception
    {
        private const string ERROR_CODE = "ERROR_CODE";
        private const string PARAMS = "PARAMS";

        /// <summary>
        /// Initializes a new instance of module exception class
        /// </summary>
        /// <param name="errorCode">Error Code for the exception</param>
        /// <param name="message">Default message associated with the exception</param>
        /// <param name="param">Parameter collection to be used for the exception message</param>
        public ModuleException(long errorCode, string message, params Pair<string, string>[] param)
            : base(message)
        {
            this.ErrorCode = errorCode;
            this.Params = param;
        }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">he System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ModuleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ErrorCode = info.GetInt64(ERROR_CODE);
            this.Params = ModuleException.GetParamCollection(info.GetString(PARAMS));
        }

        /// <summary>
        /// Gets the error code of the exception
        /// </summary>
        public long ErrorCode { get; private set; }

        /// <summary>
        /// Gets the params collection
        /// </summary>
        public Pair<string, string>[] Params { get; private set; }

        /// <summary>
        /// Add information about the instance for serialization
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">he System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ERROR_CODE, this.ErrorCode);
            info.AddValue(PARAMS, ModuleException.WriteParamCollection(this.Params));
            base.GetObjectData(info, context);
        }

        private static Pair<string, string>[] GetParamCollection(string data)
        {
            Pair<string, string>[] obj = null;

            if (false == string.IsNullOrWhiteSpace(data))
            {
                using (JsonReader reader = new JsonTextReader(new StringReader(data)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    obj = serializer.Deserialize<Pair<string, string>[]>(reader);
                }
            }

            return obj;
        }

        private static string WriteParamCollection(Pair<string, string>[] param)
        {
            string data = string.Empty;
            if (null != param && param.Length > 0)
            {
                using (StringWriter writer = new StringWriter())
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, param);
                    data = writer.ToString();
                }
            }

            return data;
        }
    }
}