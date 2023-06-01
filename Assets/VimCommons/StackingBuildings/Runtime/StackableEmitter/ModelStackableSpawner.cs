using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableEmitter
{
    public class ModelStackableSpawner: ProgressionBuilding
    {
        public StackableDefinition definition;
        public Transform spawnPoint;

        public void OnStack(SignalStackInteract signal)
        {
            if (NodeLevel.Value < 1) return;
            var stack = signal.Stack;
            if (!stack.HaveSpace(definition.weight)) return;
            var stackable = definition.Spawn();
            stackable.Init(spawnPoint);
            stack.Push(stackable);
        }
    }
}