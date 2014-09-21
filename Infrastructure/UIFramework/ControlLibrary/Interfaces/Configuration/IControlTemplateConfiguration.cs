using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IControlTemplateConfiguration
    {
        string TemplateKey { get; }

        string TemplateHTML { get; }
    }
}