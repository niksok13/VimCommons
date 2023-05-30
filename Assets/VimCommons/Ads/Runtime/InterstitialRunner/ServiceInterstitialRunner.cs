using UnityEngine;
using VimCommons.Ads.Runtime.Interstitial;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Ads.Runtime.InterstitialRunner
{
    public class ServiceInterstitialRunner : MonoBehaviour, IInterstitialRunner
    {
        public int firstTimeout = 90;
        public int activeTimeout = 60;
        public int idleTimeout = 90;
        public int damageTimeout = 5;
        
        private static readonly ServiceContainer<IInterstitialRunner> Container = Locator.Single<IInterstitialRunner>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private static IInterstitial Interstitial => Locator.Resolve<IInterstitial>();

        private float _timerActive;
        private float _timerIdle;
        private float _timerDamage;
        
        private void Start() => _timerActive = firstTimeout;

        public void Update()
        {
            _timerActive -= Time.deltaTime;
            _timerDamage -= Time.deltaTime;
            _timerIdle -= Time.deltaTime;
            if (_timerActive > 0) return;
            if (_timerDamage > 0) return;
            if (_timerIdle > 0) return;
            Interstitial.Show();
        }

        public void UpdateLastAd()
        {
            _timerActive = activeTimeout;
            _timerIdle = idleTimeout;
        }

        public void ResetIdle() => _timerIdle = 0;

        public void AddCooldown() => _timerDamage = damageTimeout;
    }
}