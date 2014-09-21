using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Controls.ResourceManager;

namespace Controls.Framework
{
    public class JsonException : Exception, IJsonExceptionConfig
    {
        public JsonException()
        {
            ResponseType = ResultType.JSON;
        }
        public JsonException(long errorId)
        {
            ErrorId = errorId;
            this.ErrorData = GetErrorMessage(errorId);
            this.ActionConfig = new List<ExceptionActionConfig>();

        }
        public JsonException(long errorId, string[] errorMessageParam)
        {
            ErrorId = errorId;
            this.ErrorData = GetFormatedErrorMessage(GetErrorMessage(errorId), errorMessageParam);
            this.ActionConfig = new List<ExceptionActionConfig>();
        }


        public JsonException(long errorId, string errorMessage)
            : base(errorMessage)
        {
            ErrorId = errorId;
            this.ErrorData = errorMessage;
            this.ActionConfig = new List<ExceptionActionConfig>();
        }

        public JsonException(long errorId, string errorMessage, string[] errorMessageParam)
        {
            ErrorId = errorId;
            this.ErrorData = GetFormatedErrorMessage(errorMessage, errorMessageParam);
            this.ActionConfig = new List<ExceptionActionConfig>();
        }
        public DisplayType Type { get; set; }
        public List<ExceptionActionConfig> ActionConfig { get; set; }
        public long ErrorId { get; set; }
        public ResultType ResponseType { get; private set; }
        public object ErrorData { get; set; }




        public ActionResult BuildException(IExceptionConfig exceptionConfig)
        {
            JsonErrorMessage exceptionMessage = new JsonErrorMessage() { Message = (exceptionConfig as JsonException).ErrorData.ToString(), ActionCommand = (exceptionConfig as JsonException).ActionConfig };
            IActionResultBuilder resultBuilder = ActionResultBuilderFactory.Create(ResponseType, null);
            return resultBuilder.Build(exceptionMessage);

        }

        public void AddCommands(string name, string function, string uri)
        {
            string loaclizedName = GetExternalizedKey(name);
            ActionConfig.Add(new ExceptionActionConfig() { Function = function, Name = loaclizedName, URI = uri });
        }

        public void ClearCommand()
        {
            ActionConfig = new List<ExceptionActionConfig>();
        }

        private string GetErrorMessage(long errorId)
        {
            return ControllerConfigurator.iResourceService.GetLiteral(errorId.ToString(), errorId.ToString());
        }

        private string GetExternalizedKey(string name)
        {
            return ControllerConfigurator.iResourceService.GetLiteral(name, name);
        }

        private string GetFormatedErrorMessage(string errorMessage, string[] errorParam)
        {
            return String.Format(errorMessage, errorParam);
        }
    }

    public class ExceptionActionConfig
    {
        public string Name { get; set; }
        public string URI { get; set; }
        public string Function { get; set; }
    }
}
