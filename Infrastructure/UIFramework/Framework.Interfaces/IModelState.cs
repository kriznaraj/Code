namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelState
    {
        string GetErrorMessage(string propertyName);
        string ConfigurationKey{ get; set; }
    }
}
