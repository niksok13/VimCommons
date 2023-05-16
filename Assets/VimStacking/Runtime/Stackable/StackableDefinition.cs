using UnityEngine;
using VimCore.Runtime.Pooling;
using VimCore.Runtime.Utils;

namespace VimStacking.Runtime.Stackable
{
    [CreateAssetMenu]
    public class StackableDefinition: ScriptableObjectWithGuid
    {
        public ModelStackable prefab;
        public int weight;

        private PrefabPool<ModelStackable> _pool;
        public PrefabPool<ModelStackable> Pool => _pool ??= PrefabPool<ModelStackable>.Instance(prefab);

        public ModelStackable Spawn()
        {
            var instance = Pool.Spawn();
            instance.Build(this);
            return instance;
        }
        public void Remove(ModelStackable instance)
        {
            Pool.Remove(instance);
        }
    }
}