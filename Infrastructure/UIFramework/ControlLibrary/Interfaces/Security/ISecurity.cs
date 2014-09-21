
namespace Controls.ControlLibrary
{
    public interface ISecurity
    {
        #region "Properties"

        /// <summary>
        /// Specifies the type of access the validator should handle
        /// </summary>
        AccessType AccessType { get; }

        /// <summary>
        /// 
        /// </summary>
        string TaskCode { get; }

        #endregion        
    }
}
