using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimAds.Runtime.Interstitial
{
    public class ServiceInterstitial : MonoBehaviour, IInterstitial
    {
        public int firstTimeout = 90;
        public int activeTimeout = 60;
        public int idleTimeout = 90;
        public int damageTimeout = 5;

        private static readonly ServiceContainer<IInterstitial> Container = Locator.Single<IInterstitial>();
        private void Awake()
        {
            Container.Attach(this);
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PreUpdate -= Tick;
        }

        private float _timerActive;
        private float _timerIdle;
        private float _timerDamage;
        
        private void Start() => _timerActive = firstTimeout;

        public void Tick()
        {
            _timerActive -= Time.deltaTime;
            _timerDamage -= Time.deltaTime;
            _timerIdle -= Time.deltaTime;
            if (_timerActive > 0) return;
            if (_timerDamage > 0) return;
            if (_timerIdle > 0) return;
            ShowInterstitial();
        }

        public void UpdateLastAd()
        {
            _timerActive = activeTimeout;
            _timerIdle = idleTimeout;
        }

        public void ResetIdle() => _timerIdle = 0;

        public void AddCooldown() => _timerDamage = damageTimeout;

        public virtual void ShowInterstitial() { }
    }
}