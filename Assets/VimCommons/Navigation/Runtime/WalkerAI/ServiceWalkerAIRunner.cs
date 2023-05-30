using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Navigation.Runtime.WalkerAI
{
    public class ServiceWalkerAIRunner : MonoBehaviour
    {
        private static readonly Filter<AWalkerAIRunner> Filter = Locator.Filter<AWalkerAIRunner>();

        private void Update()
        {
            foreach (var worker in Filter) worker.Tick();
        }
    }
}