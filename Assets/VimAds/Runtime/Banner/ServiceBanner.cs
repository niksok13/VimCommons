using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimAds.ServiceAds.Banner
{
    public class ServiceBanner : MonoBehaviour, IBanner
    {
        private static readonly ServiceContainer<IBanner> Container = Locator.Single<IBanner>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        
        private bool _enabled;
        public bool Enabled => _enabled;

    }
}