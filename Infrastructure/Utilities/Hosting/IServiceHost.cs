namespace BallyTech.Infrastructure.Hosting
{
    public interface IServiceHost
    {
        void Run();

        void Shutdown();
    }
}