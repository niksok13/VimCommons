using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Ads.Runtime.Interstitial
{
    public class ServiceInterstitial : MonoBehaviour, IInterstitial
    {
        private static readonly ServiceContainer<IInterstitial> Container = Locator.Single<IInterstitial>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        public virtual void Show() { }
    }
}