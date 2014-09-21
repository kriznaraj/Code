namespace Controls.ControlLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccessPolicy
    {
        /// <summary>
        /// Property based on which the secure settings of control will be handled
        /// </summary>
        //string SecurityCode { get; set; }

        /// <summary>
        /// Name of the policy
        /// </summary>
        //string SecurityName { get; set; }

        /// <summary>
        /// Property to make control readonly or editable based on policy configuration
        /// </summary>
        bool ReadOnly { get; set; }

        /// <summary>
        /// Property to mask or unmask control based on policy configuration
        /// </summary>
        bool Masking { get; set; }

        /// <summary>
        /// Property to make control enabled or disabled based on policy configuration
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Property to make control visible or hidden based on policy configuration
        /// </summary>
        bool Visibility { get; set; }
    }
}
