using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Navigation.Runtime.WalkerAI
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