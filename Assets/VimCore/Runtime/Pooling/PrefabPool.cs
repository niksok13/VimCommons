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
        private readonly GameObject _poolHolder;

        private static readonly Dictionary<T, PrefabPool<T>> Pools = new();

        private PrefabPool(T prefab)
        {
            _prototype = prefab;
            _poolHolder = new GameObject($"PrefabPool<{prefab.name}>");
            Object.DontDestroyOnLoad(_poolHolder);
        }

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
            if (_pool.Count<1) 
                return Object.Instantiate(_prototype, _poolHolder.transform);
            var result = _pool.Pop();
            result.gameObject.SetActive(true);
            return result;
        }

        public void Remove(T item)
        {
            if (Equals(item, null)) return;
            if (item.transform.parent!=_poolHolder.transform)
                throw new Exception("Couldn't remove reparented objects");
            item.gameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}