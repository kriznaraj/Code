using System.Collections.Generic;

namespace Controls.Framework
{
    public interface IJsonExceptionConfig : IExceptionConfig
    {
        DisplayType Type { get; set; }
        List<ExceptionActionConfig> ActionConfig { get; set; }
    }
}
