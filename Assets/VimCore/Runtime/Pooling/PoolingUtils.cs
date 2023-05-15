using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VimCore.Runtime.Pooling
{
    public static class PoolingUtils
    {
        public static async void PlayOneShot(this ParticleSystem prefab, Vector3 position)
        {
            var pool = PrefabPool<ParticleSystem>.Instance(prefab);
            var result = pool.Spawn();
            result.transform.position = position;
            result.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(prefab.main.duration));
            pool.Remove(result);
        }

        public static async Task PlayOneShot(this ParticleSystem prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            var pool = PrefabPool<ParticleSystem>.Instance(prefab);
            var result = pool.Spawn();
            var Tf = result.transform;
            Tf.position = position;
            Tf.rotation = rotation;
            Tf.localScale = scale;
            result.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(prefab.main.duration));
            pool.Remove(result);
        }
    }
}