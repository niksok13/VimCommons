using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Looting.Runtime.Core
{
    public class ModelLootable : ModelBehaviour
    {
        public LootableDefinition Definition { get; private set; }

        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        private static readonly Filter<ModelLootable> Filter = Locator.Filter<ModelLootable>();        
        

        public void Init(LootableDefinition definition)
        {
            Definition = definition;
            Transform.localScale = Vector3.one;
            Transform.rotation = Quaternion.identity;
        }

        public void Remove() => Definition.Remove(this, Transform);


        public void Tick(LooterComponent looter)
        {
            if (!Helper.WithinRadius(Transform, looter.Transform, looter.lootDistance)) return;
            Loot(looter);
        }
        
        private void Loot(LooterComponent looter)
        {
            looter.Loot(Definition);
            Definition.Remove(this, looter.transform);
        }
    }
}
