using System.Collections.Generic;
using Core.Runtime.DependencyManagement;
using UnityEngine;

namespace Commons.Triggers.Runtime.TriggerSystem
{
    public class TriggerInvoker: MonoBehaviour
    {
        private static readonly Filter<TriggerInvoker> Filter = Locator.Filter<TriggerInvoker>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private static readonly Filter<TriggerHitbox> Hitboxes = Locator.Filter<TriggerHitbox>();

        private Transform _transform;
        private Transform Transform => _transform ??= transform;

        private readonly HashSet<TriggerHitbox> _pendingBoxes = new();

        public void TickUpdate()
        {
            var invokerPos = Transform.position;
            foreach (var hitbox in Hitboxes)
            {
                if (hitbox.Collider.bounds.Contains(invokerPos))
                {
                    if (_pendingBoxes.Add(hitbox))
                        hitbox.Emit(new SignalTrigger(hitbox, this, TriggerState.Enter));
                    else
                        hitbox.Emit(new SignalTrigger(hitbox, this, TriggerState.Stay));
                }
                else
                {
                    if (_pendingBoxes.Remove(hitbox))
                        hitbox.Emit(new SignalTrigger(hitbox, this, TriggerState.Exit));
                }
            }

            foreach (var hitbox in _pendingBoxes)
            {
                if (Hitboxes.Contains(hitbox)) continue;
                _pendingBoxes.Remove(hitbox);
                hitbox.Emit(new SignalTrigger(hitbox, this, TriggerState.Exit));
                return;
            }
        }
    }
}