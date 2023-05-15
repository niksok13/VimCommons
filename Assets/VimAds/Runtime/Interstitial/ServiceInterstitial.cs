using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;
using VimInput.Runtime.InputTouch;

namespace VimAds.Runtime.Interstitial
{
    public class ServiceInterstitial : MonoBehaviour, IInterstitial
    {
        private static readonly ServiceContainer<IInterstitial> Container = Locator.Single<IInterstitial>();
        
        private static ITouchInput Touch => Locator.Resolve<ITouchInput>();
        
        private const string GroupA = "light";

        private float _timerActive;
        private float _timerIdle;
        private float _timerDamage;
        
        public int firstTimeout = 90;
        public int activeTimeout = 60;
        public int idleTimeout = 90;
        public int damageTimeout = 5;

        private void Awake()
        {
            Container.Attach(this);

            _timerActive = firstTimeout;
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);

            LoopUtil.PreUpdate -= Tick;
        }
        
        
        
        private void Tick()
        {
            
            if (Time.timeScale < 1) return;

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
            Touch.Release();
        }

        public void ResetIdle() => _timerIdle = 0;

        public void AddCooldown() => _timerDamage = damageTimeout;


        private void ShowInterstitial()
        {
            
        }
    }
}