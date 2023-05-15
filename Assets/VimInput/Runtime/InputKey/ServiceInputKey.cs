using System;
using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimInput.Runtime.InputKey
{
    public class ServiceInputKey: MonoBehaviour, IKeyInput
    {
        private static readonly ServiceContainer<IKeyInput> Container = Locator.Single<IKeyInput>();

        private Dictionary<KeyCode, Action> Listeners { get; } = new();

        private void Awake()
        {
            Container.Attach(this);
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PreUpdate -= Tick;
        }

        private void Tick()
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