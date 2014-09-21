namespace Controls.Security
{
    public interface IRole
    {
        int ID { get; }

        string Code { get; }

        string Name { get; }

        ITask[] Tasks { get; }
    }
}