using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Ads.Runtime.Banner
{
    public class ServiceBanner : MonoBehaviour, IBanner
    {
        private static readonly ServiceContainer<IBanner> Container = Locator.Single<IBanner>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        
        public virtual bool Enabled { get; set; }
    }
}