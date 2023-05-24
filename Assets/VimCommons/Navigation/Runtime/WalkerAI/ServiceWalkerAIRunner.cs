using Core.Runtime.DependencyManagement;
using Core.Runtime.Utils;
using UnityEngine;

namespace Commons.Navigation.Runtime.WalkerAI
{
    public class ServiceWalkerAIRunner : MonoBehaviour
    {
        private static readonly Filter<AWalkerAIRunner> Filter = Locator.Filter<AWalkerAIRunner>();
        private void Awake() => LoopUtil.PreUpdate += Tick;

        private void OnDestroy() => LoopUtil.PreUpdate -= Tick;

        private void Tick()
        {
            foreach (var worker in Filter) worker.Tick();
        }
    }
}