using IngameDebugConsole;
using UnityEngine;

namespace VimCommons.Debugger.Runtime
{
    public abstract class ACheat: MonoBehaviour
    {
        protected abstract string Command { get; }
        protected abstract string Description { get; }

        private void Awake()
        {
            DebugLogConsole.AddCommand("cheat." + Command, Description, OnApply);
        }

        protected abstract void OnApply();
    }
    
    public abstract class ACheat<T>: MonoBehaviour
    {
        protected abstract string Command { get; }
        protected abstract string Description { get; }

        private void Awake()
        {
            DebugLogConsole.AddCommand<T>("cheat." + Command, Description, OnApply);
        }

        protected abstract void OnApply(T arg);
    }
}