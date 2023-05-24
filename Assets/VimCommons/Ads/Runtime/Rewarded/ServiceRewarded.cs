using System;
using UnityEngine;
using VimCommons.Ads.Runtime.InterstitialRunner;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Ads.Runtime.Rewarded
{
    public class ServiceRewarded : MonoBehaviour, IRewarded
    {
        private static readonly ServiceContainer<IRewarded> Container = Locator.Single<IRewarded>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        
        private static IInterstitialRunner Interstitial => Locator.Resolve<IInterstitialRunner>();
        
        public ObservableData<bool> Ready { get; } = new();
        
        public virtual void Show(Action callback)
        {
            UpdateLastAd();
            callback.Invoke();
        }

        protected void UpdateLastAd() => Interstitial?.UpdateLastAd();
    }
}