using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime.Stackable;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Stacking.Runtime.StackableEmitter
{
    public class ModelStackableEmitter: ProgressionBuilding<StackableEmitterLevelData>
    {
        public StackableDefinition definition;
        public Transform spawnPoint;

        private static readonly Filter<ModelStackableEmitter> Filter = Locator.Filter<ModelStackableEmitter>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private static readonly Filter<StackComponent> Stacks = Locator.Filter<StackComponent>();

        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        private float _fullCooldown;

        public void TickUpdate()
        {
            if (NodeLevel.Value < 1) return;
            
            foreach (var stack in Stacks)
            {
                if (Helper.SqrDistance(Transform.position, stack.Transform.position) > 6) continue;
                if (!stack.Ready(LevelData.cooldown)) continue;
                if (!stack.HaveSpace(definition.weight)) continue;
                var stackable = definition.Spawn();
                stackable.Init(spawnPoint.position);
                stack.Pick(stackable);
            }
            _fullCooldown -= Time.deltaTime;
            if (_fullCooldown > 0) return;
            _fullCooldown = 2;
        }
    }
}