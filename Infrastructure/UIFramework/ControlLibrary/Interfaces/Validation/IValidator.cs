
namespace Controls.ControlLibrary
{
    public interface IValidator
    {
        #region "Properties"

        /// <summary>
        /// Property to indicate the validation is ON/OFF
        /// </summary>
        bool DoValidate { get;  }

        /// <summary>
        /// Message to display after the validation
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Specifies the type of validation the validator should handle
        /// </summary>
        ValidatorsType Type { get; }

        string MessageKey { get; }

        #endregion

        #region "Methods"
        /// <summary>
        /// Validates the data of the model properties based on the configuration
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Validate(object data, string expression);

        #endregion
    }
}
