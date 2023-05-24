using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Camera.Runtime.ServiceLookCameraSystem
{
    public class LookCameraComponent : MonoBehaviour
    {
        private static readonly Filter<LookCameraComponent> Filter = Locator.Filter<LookCameraComponent>();

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        private void OnEnable() => Filter.Add(this);

        private void OnDisable() => Filter.Remove(this);
    }
}
