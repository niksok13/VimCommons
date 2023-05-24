namespace VimCore.Runtime.DependencyManagement
{
    public class ServiceContainer<TService>
    {
        public TService Instance { get; private set; }
        public void Attach(TService service) => Instance = service;

        public void Detach(TService service)
        {
            if (Equals(service, Instance)) Instance = default;
        }
    }
}