namespace Controls.Types
{
    public interface IProvider
    {
        T Get<T>();
    }
}