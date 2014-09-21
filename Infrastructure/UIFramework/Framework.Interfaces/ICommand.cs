
namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand 
    {
        string ConfigKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetConfigurationKey();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetTaskId();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        void SetTaskId(int taskId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetTranAccount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranAccount"></param>
        void SetTranAccount(string tranAccount);
    }
}
