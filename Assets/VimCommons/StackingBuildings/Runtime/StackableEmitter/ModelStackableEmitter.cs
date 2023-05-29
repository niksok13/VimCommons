using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.StackingBuildings.Runtime.StackableEmitter
{
    public class ModelStackableEmitter: ProgressionBuilding<StackableEmitterLevelData>
    {
        public StackableDefinition definition;
        public Transform spawnPoint;

        public void OnStack(SignalStackInteract signal)
        {
            var stack = signal.Stack;
            if (!stack.HaveSpace(definition.weight)) return;
            var stackable = definition.Spawn();
            stackable.Init(spawnPoint.position);
            stack.Push(stackable);
        }
    }
}