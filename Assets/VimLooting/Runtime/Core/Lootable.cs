using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Utils;

namespace VimLooting.Runtime.Core
{
    public class Lootable : MonoBehaviour
    {
        public LootableDefinition Definition { get; private set; }

        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        private static readonly Filter<Lootable> Filter = Locator.Filter<Lootable>();        
        

        public void Init(LootableDefinition definition)
        {
            Definition = definition;
            Transform.localScale = Vector3.one;
            Transform.rotation = Quaternion.identity;
        }

        public void Remove()
        {
            Filter.Remove(this);
            Definition.Remove(this);
        }
        

        public void Tick(LooterComponent looter)
        {
            if (!Helper.WithinRadius(Transform, looter.Transform, looter.lootDistance)) return;
            Loot(looter);
        }
        
        private void Loot(LooterComponent looter)
        {
            Filter.Remove(this);
            var from = Transform.position;
            EZ.Spawn().Tween( ez => {
                Transform.localScale = Vector3.one*ez.BackOut;
                Transform.position = Helper.LerpParabolic(from, looter.Transform.position+Vector3.up, ez.Linear);
            }).Call(_ =>  {
                looter.Loot(Definition);
                Definition.Remove(this);
            });
        }
    }
}
