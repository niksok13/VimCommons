using UnityEngine;
using UnityEngine.AI;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Navigation.Runtime.AgentConveyorSystem
{
    public class ConveyorAgentComponent : MonoBehaviour
    {
        private static readonly Filter<ConveyorAgentComponent> Filter = Locator.Filter<ConveyorAgentComponent>();

        private void OnEnable() => Filter.Add(this);

        private void OnDisable() => Filter.Remove(this);
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent ??= GetComponent<NavMeshAgent>(); 

        public Vector3 Position => transform.position;

        public void Translate(Vector3 direction) => Agent.Move(direction);
    }
}
