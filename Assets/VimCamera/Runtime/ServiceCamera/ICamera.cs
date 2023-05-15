using UnityEngine;

namespace VimCamera.Runtime.ServiceCamera
{
    public interface ICamera
    {
        UnityEngine.Camera Camera { get; }
        Transform Transform { get; }
        void Look(CameraState cameraState);
        void Focus(CameraState state);
        void Unfocus(CameraState cameraState);
        bool CantSee(Transform point);
    }
}