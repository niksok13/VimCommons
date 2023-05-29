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
        public float cooldown = 0.1f;

        private static readonly Filter<ModelStackableEmitter> Filter = Locator.Filter<ModelStackableEmitter>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private float _readyTime;
        
        public void OnStack(SignalStackInteract signal)
        {
            if (Time.realtimeSinceStartup > _readyTime) return;
            var stack = signal.Stack;

            if (!stack.Ready(LevelData.cooldown)) return;
            if (!stack.HaveSpace(definition.weight)) return;
            
            var stackable = definition.Spawn();
            stackable.Init(spawnPoint.position);
            stack.Push(stackable);
            _readyTime = Time.realtimeSinceStartup + cooldown;
        }
    }
}