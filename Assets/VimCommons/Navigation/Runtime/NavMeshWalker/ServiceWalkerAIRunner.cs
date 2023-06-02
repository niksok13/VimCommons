using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Navigation.Runtime.NavMeshWalker
{
    public class ServiceWalkerAIRunner : MonoBehaviour
    {
        private static readonly Filter<ANMWalker> Filter = Locator.Filter<ANMWalker>();

        private void Update()
        {
            foreach (var worker in Filter) worker.Tick();
        }
    }
}