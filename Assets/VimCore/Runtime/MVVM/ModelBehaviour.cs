using System;
using System.Reflection;
using UnityEngine;

namespace Core.Runtime.MVVM
{
    public abstract class ModelBehaviour: MonoBehaviour
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        
        public ObservableChannel GetObservableChannel(string key)
        {
            var info = GetType().GetProperty(key, BindingAttr);
            if (info == null)
            {
                Debug.LogWarning($"ObservableChannel {key} in {GetType().Name} not found (Property required)",this);
                return null;
            }
            return info.GetValue(this) as ObservableChannel;
        }

        public ObservableData<T> GetObservableData<T>(string key)
        {
            var info = GetType().GetProperty(key, BindingAttr);
            if (info == null)
            {
                Debug.LogWarning($"ObservableData<{typeof(T).Name}> {key} in {GetType().Name} not found (Property required)",this);
                return null;
            }
            
            return info.GetValue(this) as ObservableData<T>;
        }
        
        public Action<T> GetAction<T>(string key)
        {
            var info = GetType().GetMethod(key, BindingAttr);
            if (info == null)
            {
                Debug.LogWarning($"Method {key}<{typeof(T).Name}> in {GetType().Name} not found", this);
                return null;
            }
            return info.CreateDelegate(typeof(Action<T>),this) as Action<T>;
        }
    }
}