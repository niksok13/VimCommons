using System;
using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Input.Runtime.InputKey
{
    public class ServiceInputKey: MonoBehaviour, IKeyInput
    {
        private static readonly ServiceContainer<IKeyInput> Container = Locator.Single<IKeyInput>();

        private Dictionary<KeyCode, Action> Listeners { get; } = new();

        private void Awake() => Container.Attach(this);

        private void OnDestroy() => Container.Detach(this);

        private void Update()
        {
            if (!UnityEngine.Input.anyKeyDown) return;

            foreach (var entry in Listeners)
                if (UnityEngine.Input.GetKeyDown(entry.Key))
                {
                    entry.Value?.Invoke();
                    return;                    
                }
        }

        public void Register(KeyCode key, Action listener)
        {
            Listeners[key] = listener;
        }

        public void Release(KeyCode key, Action listener)
        {
            if (Listeners[key] == listener)
                Listeners.Remove(key);
        }
    }
}