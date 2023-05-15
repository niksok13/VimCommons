using Cysharp.Threading.Tasks;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCamera.Runtime.ServiceCamera
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
        private void Awake()
        {
            Container.Attach(this);
            LoopUtil.PostLateUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PostLateUpdate -= Tick;
        }

        private void Tick()
        {
            if (!State) return;
            State.Track(this);
        }

        public async void Look(CameraState cameraState)
        {
            _tempState = cameraState;
            await UniTask.Delay(1500, DelayType.UnscaledDeltaTime);
            _tempState = null;
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