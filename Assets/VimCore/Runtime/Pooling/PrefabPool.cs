using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace VimCore.Runtime.Pooling
{
    public class PrefabPool<T> where T: Component
    {
        private readonly T _prototype;
        private readonly Stack<T> _pool = new();

        private GameObject _holder;
        private void CheckHolder()
        {
            if (_holder) return;
            _holder = new GameObject($"PrefabPool<{_prototype.name}>");
            _pool.Clear();
        }

        private static readonly Dictionary<T, PrefabPool<T>> Pools = new();

        private PrefabPool(T prefab) => _prototype = prefab;

        public static PrefabPool<T> Instance(T prefab)
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(prefab.gameObject.scene.path), "Couldn't use scene object as poolable prefab");

            if (Pools.TryGetValue(prefab, out var pool)) return pool;
            var result = new PrefabPool<T>(prefab);
            Pools[prefab] = result;
            return result;
        }
        
        public T Spawn()
        {
            CheckHolder();
            if (_pool.Count < 1) 
                return Object.Instantiate(_prototype, _holder.transform);
            var result = _pool.Pop();
            result.gameObject.SetActive(true);
            return result;
        }

        public void Remove(T item)
        {
            CheckHolder();
            Assert.IsNotNull(item, "Null passed");
            Assert.IsTrue(item.transform.parent == _holder.transform, "Couldn't remove reparented objects");
            item.gameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}