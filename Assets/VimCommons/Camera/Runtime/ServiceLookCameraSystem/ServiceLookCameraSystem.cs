using System.Linq;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using VimCommons.Camera.Runtime.ServiceCamera;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Camera.Runtime.ServiceLookCameraSystem
{
    public class ServiceLookCameraSystem: MonoBehaviour
    {
        private static readonly Filter<LookCameraComponent> LookCameraComponents = Locator.Filter<LookCameraComponent>();
        private static ICamera Camera => Locator.Resolve<ICamera>();
        
        private JobHandle _jobHandle;
        private TransformAccessArray _native;
        private void Start() => TickNative();

        private void OnDestroy() => _native.Dispose();

        private void Update()
        {
            var job = new LookCameraJob
            {
                Rotation = Camera.Transform.rotation,
            };
            _jobHandle = job.Schedule(_native);
        }

        private void LateUpdate()
        {
            _jobHandle.Complete();
            if (Time.frameCount % 16 > 0) return;
            TickNative();
        }
        
        
        private void TickNative()
        {
            var arr = LookCameraComponents.Select(i => i.Transform).ToArray();
            if(_native.isCreated)
                _native.Dispose();
            _native = new TransformAccessArray(arr);
        }
    }
}