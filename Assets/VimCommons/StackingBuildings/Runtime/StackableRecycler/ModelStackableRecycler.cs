using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableRecycler
{
    public class ModelStackableRecycler : ProgressionBuilding
    {
        public StackableDefinition[] recyclable;
        
        private Transform _transform;
        public Transform Transform => _transform ??= GetComponent<Transform>(); 

        public void OnStack(SignalStackInteract signal)
        {
            if (NodeLevel.Value < 1) return;
            var stack = signal.Stack;
            var stackable = stack.Pop(recyclable);
            if (!stackable) return;
            var unstackPoint = Transform.position;
            stackable.TweenRemove(unstackPoint);
        }
    }
}
