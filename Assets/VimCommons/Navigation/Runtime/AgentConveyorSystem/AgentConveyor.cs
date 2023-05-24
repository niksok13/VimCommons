using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Navigation.Runtime.AgentConveyorSystem
{
    public class AgentConveyor : MonoBehaviour
    {
        public Vector3 direction = Vector3.right;

        private static readonly Filter<AgentConveyor> Filter = Locator.Filter<AgentConveyor>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private static readonly Filter<ConveyorAgentComponent> Agents = Locator.Filter<ConveyorAgentComponent>();

        private Collider _hitbox;
        public Collider Hitbox => _hitbox ??= GetComponent<Collider>();

        public void Tick()
        {
            foreach (var agent in Agents)
            {
                if(!Hitbox.bounds.Contains(agent.Position)) continue;
                agent.Translate(direction * LoopUtil.Delta);
            }
        }
    }
}
