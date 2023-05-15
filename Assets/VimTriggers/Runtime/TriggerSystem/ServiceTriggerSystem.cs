using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimTriggers.Runtime.TriggerSystem
{
    public class ServiceTriggerSystem : MonoBehaviour
    {
        private static readonly Filter<TriggerInvoker> Invokers = Locator.Filter<TriggerInvoker>();

        private void Awake() => LoopUtil.PreUpdate += Tick;

        private void OnDestroy() => LoopUtil.PreUpdate -= Tick;

        private void Tick()
        {
            foreach (var invoker in Invokers) 
                invoker.TickUpdate();
        }
    }
}