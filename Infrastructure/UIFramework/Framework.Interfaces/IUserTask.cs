using System;
namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserTask
    {
        Int64 TaskId
        {
            get;
            set;
        }

        string TaskCode
        {
            get;
            set;
        }
    }
}
