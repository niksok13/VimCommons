using System;
using VimAds.ServiceAds.Interstitial;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimAds.ServiceAds.Rewarded
{
    public class ServiceRewarded : ModelBehaviour, IRewarded
    {
        private static readonly ServiceContainer<IRewarded> Container = Locator.Single<IRewarded>();
        
        private static IInterstitial Interstitial => Locator.Resolve<IInterstitial>();
        
        private Action _rewardedCallback;

        public ObservableData<bool> Ready { get; } = new();

        private void Awake()
        {
            Container.Attach(this);
        }
        
        private void OnDestroy()
        {
            Container.Detach(this);
        }
        

        public void Show(Action callback)
        {
        }

        
        private void UpdateLastAd() => Interstitial?.UpdateLastAd();
    }
}