using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VimCore.Runtime.Pooling
{
    public class PrefabPool<T> where T: Component
    {
        private readonly T _prototype;
        private readonly Stack<T> _pool = new();

        private GameObject _holder;
        private GameObject Holder
        {
            get
            {
                if (Equals(_holder, null))
                    _holder = new GameObject($"PrefabPool<{_prototype.name}>");
                return _holder;
            }
        }

        private static readonly Dictionary<T, PrefabPool<T>> Pools = new();

        private PrefabPool(T prefab) => _prototype = prefab;

        public static PrefabPool<T> Instance(T prefab)
        {
            if (!string.IsNullOrWhiteSpace(prefab.gameObject.scene.path)) 
                throw new Exception("Couldn't use scene object as poolable prefab");

            if (Pools.TryGetValue(prefab,out var pool))
                return pool;
            
            var result = new PrefabPool<T>(prefab);
            Pools[prefab] = result;
            return result;
        }
        
        public T Spawn()
        {
            if (_pool.Count < 1) 
                return Object.Instantiate(_prototype, Holder.transform);
            var result = _pool.Pop();
            result.gameObject.SetActive(true);
            return result;
        }

        public void Remove(T item)
        {
            if (Equals(item, null)) return;
            if (item.transform.parent != Holder.transform)
                throw new Exception("Couldn't remove reparented objects");
            item.gameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}