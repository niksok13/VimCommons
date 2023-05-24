using Core.Runtime.DependencyManagement;
using Core.Runtime.Utils;
using UnityEngine;

namespace Commons.Navigation.Runtime.AgentConveyorSystem
{
    public class ServiceAgentConveyorSystem: MonoBehaviour
    {
        private static readonly Filter<AgentConveyor> Filter = Locator.Filter<AgentConveyor>();
        private void Awake() => LoopUtil.PostLateUpdate += Tick;
        private void OnDestroy() => LoopUtil.PostLateUpdate -= Tick;

        private void Tick()
        {
            foreach (var conveyor in Filter) conveyor.Tick();
        }
    }
}