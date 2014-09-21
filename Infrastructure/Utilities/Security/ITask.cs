using System;

namespace Controls.Security
{
    public interface ITask
    {
        Int16 ID { get; }

        string Code { get; }

        string Name { get; }

        ITaskLimit Limit { get; }
    }
}