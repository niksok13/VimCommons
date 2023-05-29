using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.Core
{
    public class LooterComponent:  ASignalEmitter<SignalLoot>
    {
        public float lootDistance = 2;

        private static readonly Filter<ModelLootable> Lootables = Locator.Filter<ModelLootable>();
        private static readonly Filter<ModelLootableBatch> Batches = Locator.Filter<ModelLootableBatch>();

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        internal void Loot(LootableDefinition type) => Emit(new SignalLoot(type));

        public void Update()
        {
            foreach (var lootable in Lootables) lootable.Tick(this);
            foreach (var batch in Batches) batch.Tick(this);
        }
    }
}