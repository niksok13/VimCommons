using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Navigation.Runtime.NavMeshAgentConveyorSystem
{
    public class ServiceNMAgentConveyorRunner: MonoBehaviour
    {
        private static readonly Filter<NMAgentConveyor> Filter = Locator.Filter<NMAgentConveyor>();

        private void LateUpdate()
        {
            foreach (var conveyor in Filter) conveyor.Tick();
        }
    }
}