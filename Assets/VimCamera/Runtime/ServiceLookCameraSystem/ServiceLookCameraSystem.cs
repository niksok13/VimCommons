using System.Linq;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using VimCamera.Runtime.ServiceCamera;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCamera.Runtime.ServiceLookCameraSystem
{
    public class ServiceLookCameraSystem: MonoBehaviour
    {
        private static readonly Filter<LookCameraComponent> LookCameraComponents = Locator.Filter<LookCameraComponent>();
        private static ICamera Camera => Locator.Resolve<ICamera>();
        
        private JobHandle _jobHandle;
        private TransformAccessArray _native;
        private void Awake()
        {
            LoopUtil.PreUpdate += Tick;
            LoopUtil.PostLateUpdate += LateTick;
            TickNative();
        }

        private void OnDestroy()
        {
            _native.Dispose();
            LoopUtil.PreUpdate -= Tick;
            LoopUtil.PostLateUpdate -= LateTick;
        }

        private void Tick()
        {
            var job = new LookCameraJob
            {
                Rotation = Camera.Transform.rotation,
            };
            _jobHandle = job.Schedule(_native);
        }

        private void LateTick()
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