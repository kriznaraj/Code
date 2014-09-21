using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface ICustomValidationExpressionConfiguration
    {
        CustomValidationType ValidationType { get; }

        string Expression { get; }
    }
}