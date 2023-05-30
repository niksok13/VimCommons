using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Navigation.Runtime.AgentConveyorSystem
{
    public class ServiceAgentConveyorSystem: MonoBehaviour
    {
        private static readonly Filter<AgentConveyor> Filter = Locator.Filter<AgentConveyor>();

        private void LateUpdate()
        {
            foreach (var conveyor in Filter) conveyor.Tick();
        }
    }
}