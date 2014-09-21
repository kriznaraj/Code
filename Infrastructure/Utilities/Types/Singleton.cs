namespace Controls.Types
{
    public static class Singleton<T>
        where T : new()
    {
        private static readonly T instance;

        static Singleton()
        {
            instance = new T();
        }

        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}