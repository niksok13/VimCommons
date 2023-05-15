using Unity.AI.Navigation;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimNavigation.Runtime.WalkableSurfaceWrapper
{
    public class ServiceWalkableSurfaceWrapper : MonoBehaviour, IWalkableSurfaceWrapper
    {
        private static readonly ServiceContainer<IWalkableSurfaceWrapper> Container = Locator.Single<IWalkableSurfaceWrapper>();

        private float _timeout; 
        private bool _dirty;
        private NavMeshSurface _surface;


        public void SetDirty() => _dirty = true;

        private void Awake()
        {
            Container.Attach(this);
            _surface = GetComponent<NavMeshSurface>();
            _surface.BuildNavMesh();
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PreUpdate -= Tick;
        }

        private void Tick()
        {
            _timeout -= Time.deltaTime;
            if (!_dirty) return;
            if (_timeout>0) return;
            _timeout = 1;
            _surface.UpdateNavMesh(_surface.navMeshData);
            _dirty = false;
        }
    }
}

