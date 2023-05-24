namespace VimCore.Runtime.DependencyManagement
{
    public static class Locator
    {
        private static class Storage<TService>
        {
            public static readonly ServiceContainer<TService> Single = new();
            public static readonly Filter<TService> Filter = new();
        }
        
        public static ServiceContainer<TService> Single<TService>() => Storage<TService>.Single;
        public static Filter<TService> Filter<TService>() => Storage<TService>.Filter;

        public static TService Resolve<TService>() => Single<TService>().Instance;
    }
}