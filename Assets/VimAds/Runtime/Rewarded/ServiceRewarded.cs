using System;
using UnityEngine;
using VimAds.Runtime.Interstitial;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimAds.Runtime.Rewarded
{
    public class ServiceRewarded : MonoBehaviour, IRewarded
    {
        private static readonly ServiceContainer<IRewarded> Container = Locator.Single<IRewarded>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        
        private static IInterstitial Interstitial => Locator.Resolve<IInterstitial>();
        
        private Action _rewardedCallback;

        public ObservableData<bool> Ready { get; } = new();
        
        public virtual void Show(Action callback) => callback.Invoke();
        
        private void UpdateLastAd() => Interstitial?.UpdateLastAd();
    }
}