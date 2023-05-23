using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Pooling;
using VimCore.Runtime.Utils;

namespace VimLooting.Runtime.Core
{
    [CreateAssetMenu]
    public class LootableDefinition: ScriptableObjectWithGuid
    {
        public Lootable prefab;
        public Sprite icon;
        public string iconLabel;
        
        private static readonly Filter<Lootable> Filter = Locator.Filter<Lootable>();

        private PrefabPool<Lootable> _pool;
        public PrefabPool<Lootable> Pool => _pool ??= PrefabPool<Lootable>.Instance(prefab);
        
        public Lootable Spawn()
        {
            var instance = Pool.Spawn();
            instance.Init(this);
            return instance;
        }
        
        public void Spawn(Vector3 posFrom, Vector3 posTo)
        {
            var result = Spawn();
            result.Transform.position = posFrom;
            result.Transform.localScale = Vector3.zero;
            
            EZ.Spawn().Tween(0.3f, ez => {
                result.Transform.localPosition = Helper.LerpParabolic(posFrom, posTo, ez.Linear, 2);
                result.Transform.localScale = Vector3.one * ez.BackOut;
            }).Call(_ => {
                Filter.Add(result);
            });
        }

        public void Remove(Lootable lootable, Transform target)
        {
            Filter.Remove(lootable);
            var from = lootable.Transform.position;
            EZ.Spawn().Tween(ez => {
                lootable.Transform.localScale = Vector3.one*(1-ez.BackOut);
                lootable.Transform.position = Helper.LerpParabolic(from, target.position + Vector3.up, ez.Linear);
            }).Call(_ =>  {
                Pool.Remove(lootable);
            });
        }
    }
}