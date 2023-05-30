using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Utils;

namespace VimCommons.Camera.Runtime.ServiceCamera
{
    public class ServiceCamera : MonoBehaviour, ICamera
    {
        private static readonly ServiceContainer<ICamera> Container = Locator.Single<ICamera>();
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        private UnityEngine.Camera _camera;
        public UnityEngine.Camera Camera => _camera ??= GetComponent<UnityEngine.Camera>();
        
        private CameraState _state;
        private CameraState _tempState;
        private CameraState State => _tempState ? _tempState : _state;
        private void Awake() => Container.Attach(this);

        private void OnDestroy() => Container.Detach(this);

        private void LateUpdate()
        {
            if (!State) return;
            State.Track(this);
        }

        public void Look(CameraState cameraState)
        {
            _tempState = cameraState;
            EZ.Spawn().Delay(1.5f).Call(_ =>
            {
                _tempState = null;
            });
            
        }

        public void Focus(CameraState cameraState) => _state = cameraState;
        public void Unfocus(CameraState cameraState)
        {
            if (_state != cameraState) return;
            _state = null;
        }

        public bool CantSee(Transform point)
        {
            var screenPoint = Camera.WorldToViewportPoint(point.position);
            return screenPoint.y is < 0 or > 1 || screenPoint.x is < 0 or > 1;
        }
    }
}