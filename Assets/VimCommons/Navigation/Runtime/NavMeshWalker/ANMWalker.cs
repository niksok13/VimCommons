using UnityEngine;
using UnityEngine.AI;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Navigation.Runtime.NavMeshWalker
{
    public abstract class ANMWalker: ModelBehaviour
    {
        private static readonly Filter<ANMWalker> Filter = Locator.Filter<ANMWalker>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent ??= GetComponent<NavMeshAgent>();
        
        public ObservableData<float> Speed { get; } = new();

        public void Tick()
        {
            Speed.Value = Agent.velocity.magnitude / Agent.speed;
            if (Agent.velocity.magnitude > 0.5f)
            {
                var lookDir = Agent.velocity;
                lookDir.y = 0;
                transform.forward = Vector3.Lerp(transform.forward, lookDir, 0.1f);
            }
            if (!Agent.isOnNavMesh) return;
            var target = GetTarget();
            if(target)
                Agent.SetDestination(target.position);
        }

        protected abstract Transform GetTarget();

        public void Init(Vector3 pos, Quaternion rot)
        {
            Agent.Warp(pos);
            transform.rotation = rot;
        }
    }
}