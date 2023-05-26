using UnityEngine;
using VimCommons.Stacking.Runtime;
using VimCore.Runtime.MVVM;

namespace VimCommons.StackingBuildings.Runtime.StackableRecycler
{
    public class ModelStackableRecycler : ModelBehaviour
    {
        public StackableDefinition[] recyclable;
        
        private Transform _transform;
        public Transform Transform => _transform ??= GetComponent<Transform>(); 

        public void OnStack(SignalStackInteract signal)
        {
            var stack = signal.Stack;
            var stackable = stack.Peek(recyclable);
            if (!stackable) return;
            var unstackPoint = Transform.position;
            stackable.TweenRemove(unstackPoint);
        }
    }
}
