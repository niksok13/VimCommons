using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Stacking.Runtime
{
    public class StackInteractor: ASignalEmitter<SignalStackInteract>
    {
        private static readonly Filter<StackInteractor> Filter = Locator.Filter<StackInteractor>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private Collider _hitbox;
        public Collider Hitbox => _hitbox ??= GetComponent<Collider>();


        public void Interact(StackComponent stack)
        {
            if (!Hitbox.bounds.Contains(stack.Transform.position)) return;
            Emit(new SignalStackInteract(stack));
        }
    }
}