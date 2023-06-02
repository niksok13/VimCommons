using Unity.AI.Navigation;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Navigation.Runtime.NavMeshSurfaceWrapper
{
    public class ServiceNMSurfaceWrapper : MonoBehaviour, IWalkableSurfaceWrapper
    {
        private static readonly ServiceContainer<IWalkableSurfaceWrapper> Container = Locator.Single<IWalkableSurfaceWrapper>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private NavMeshSurface _surface;
        public NavMeshSurface Surface => _surface ??= GetComponent<NavMeshSurface>();
        
        private float _timeout; 
        private bool _dirty;

        private void Start() => Surface.BuildNavMesh();

        private void Update()
        {
            _timeout -= Time.deltaTime;
            if (!_dirty) return;
            if (_timeout>0) return;
            _timeout = 1;
            Surface.UpdateNavMesh(Surface.navMeshData);
            _dirty = false;
        }
        
        public void SetDirty() => _dirty = true;
    }
}

