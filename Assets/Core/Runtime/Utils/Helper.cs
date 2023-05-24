using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Runtime.Utils
{
    public static class Helper
    {
        private static readonly System.Random Rnd = new();

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go.TryGetComponent<T>(out var result)) return result;
            return go.AddComponent<T>();
        }
        
        public static T GetRandomEnum<T>()
        {
            var array = Enum.GetValues(typeof(T));
            var index = Rnd.Next(0, array.Length);
            return (T) array.GetValue(index);
        }
        
        public static T GetRandomItem<T>(this ICollection<T> list)
        {
            var index = Rnd.Next(0, list.Count);
            return list.ElementAtOrDefault(index);
        }

        public static T GetHashedItem<T>(this ICollection<T> list, object seed)
        {
            var index = Mathf.Abs(seed.GetHashCode() % list.Count);
            return list.ElementAtOrDefault(index);
        }

        public static bool Chance(float value) => UnityEngine.Random.value < value;
        
        public static Func<float> GetPerlinSequence(float speed = 1f)
        {
            var arg = (float)Rnd.Next(int.MinValue, int.MaxValue);
            return () => Mathf.PerlinNoise(arg, Time.realtimeSinceStartup*speed);
        }

        public static string Format(this string str, params object[] args) => string.Format(str, args);

        public static Vector3 LerpParabolic(Vector3 from, Vector3 to, float pt, float height = 1)
        {
            var baseline = Vector3.Lerp(from, to, pt);
            var curve = 4 * height * pt * (1 - pt);
            return baseline + new Vector3(0,curve,0);
        }

        public static Vector3 Spread(float radius)
        {
            var result = Random.onUnitSphere;
            result.y = 0;
            return result;
        }

        public static float SqrDistance(Vector3 pos1, Vector3 pos2)
        {
            var dx = pos1.x - pos2.x;
//            var dy = pos1.y - pos2.y;
            var dz = pos1.z - pos2.z;
            return dx * dx + dz * dz;// + dy * dy;
        }

        public static bool WithinRadius(Transform first, Transform second, float distance) => SqrDistance(first.position, second.position) < distance * distance;
    }
}