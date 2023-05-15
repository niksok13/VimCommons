using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimVibration.Runtime
{
    public class ServiceVibration: ModelBehaviour, IVibration
    {
        private static readonly ServiceContainer<IVibration> Container = Locator.Single<IVibration>();

        private float _ready;

        public ObservableData<bool> IsActive { get; } = new();

        private void Awake()
        {
            Container.Attach(this);
            IsActive.ConnectPref("vibrationActive", true);
        }

        public bool Ready
        {
            get
            {
                if (Application.isEditor) return false;
                if (!IsActive.Value) return false;
                var now = Time.realtimeSinceStartup;
                if (now < _ready) return false;
                _ready = now + 0.1f;
                return true;
            }
        }

        public void Light()
        {
            if (!Ready) return;
            Taptic.Light();
        }

        public void Medium()
        {
            if (!Ready) return;
            Taptic.Medium();
        }

        public void Heavy()
        {
            if (!Ready) return;
            Taptic.Heavy();
        }

        
        public void Selection()
        {
            if (!Ready) return;
            Taptic.Selection();
        }

        public void Success()
        {
            if (!Ready) return;
            Taptic.Success();
        }

        public void Warning()
        {
            if (!Ready) return;
            Taptic.Warning();
        }

        public void Failure()
        {
            if (!Ready) return;
            Taptic.Failure();
        }
    }
}