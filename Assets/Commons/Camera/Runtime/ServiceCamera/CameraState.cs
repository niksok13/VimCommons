using Core.Runtime.DependencyManagement;
using Core.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace Commons.Camera.Runtime.ServiceCamera
{
    public class CameraState : MonoBehaviour
    {
        private static ICamera Camera => Locator.Resolve<ICamera>();

        private float speed = 3;
        private float distance = 20;

        private float aspectRatio;
        public float Zoom { get; set; } = 1;
        public float Distance => distance * aspectRatio * Zoom;

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        private void Awake() => aspectRatio = Display.main.renderingHeight * 1f / Display.main.renderingWidth;

        private void OnDestroy() => Camera?.Unfocus(this);

        public void Focus() => Camera?.Focus(this);

        public void Warp()
        {
            if (Equals(Camera, null)) return;
            Camera.Focus(this);
            Camera.Transform.position = Transform.position - Transform.forward * distance;
            Camera.Transform.rotation = Transform.rotation;
        }

        public void Track(ServiceCamera cam)
        {
            var tm = LoopUtil.Delta * speed;
            cam.Transform.position = Vector3.Lerp(cam.Transform.position, Transform.position - Transform.forward * Distance, tm);
            cam.Transform.rotation = Quaternion.LerpUnclamped(cam.Transform.rotation, Transform.rotation, tm);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var cam = SceneView.currentDrawingSceneView;
            var pos = transform.position;
            var rot = transform.rotation;
            cam.LookAt(pos, rot, 20);
        }
#endif
    }
}