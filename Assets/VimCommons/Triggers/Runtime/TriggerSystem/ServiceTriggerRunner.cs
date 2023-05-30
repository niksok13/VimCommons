using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Triggers.Runtime.TriggerSystem
{
    public class ServiceTriggerRunner : MonoBehaviour
    {
        private static readonly Filter<TriggerInvoker> Invokers = Locator.Filter<TriggerInvoker>();

        private void Update()
        {
            foreach (var invoker in Invokers) 
                invoker.TickUpdate();
        }
    }
}