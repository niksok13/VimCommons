using UnityEngine;
using UnityEngine.Jobs;

namespace VimCamera.Runtime.ServiceLookCameraSystem
{
    public struct LookCameraJob : IJobParallelForTransform
    {
        public Quaternion Rotation;
        
        public void Execute(int index, TransformAccess transform)
        {
            transform.rotation = Rotation;
        }
    }
}