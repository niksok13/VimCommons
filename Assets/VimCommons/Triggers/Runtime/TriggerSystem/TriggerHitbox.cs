using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;
using UnityEngine;

namespace Commons.Triggers.Runtime.TriggerSystem
{
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerHitbox : ASignalEmitter<SignalTrigger, Collider>
    {
        private static readonly Filter<TriggerHitbox> Filter = Locator.Filter<TriggerHitbox>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        
        private Collider _collider;
        public Collider Collider => _collider ??= GetComponentInChildren<Collider>(true);

        private void Awake() => gameObject.layer = 2;
        
    }
}