using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Environment.Runtime.GrassSystem
{
    public class GrassEffector : MonoBehaviour
    {
        private static readonly Filter<GrassEffector> Filter = Locator.Filter<GrassEffector>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private Transform _transform;
        public Transform Transform => _transform ??= transform;
    }
}