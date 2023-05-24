using System;
using UnityEditor;
using UnityEngine;
using VimCommons.Stacking.Runtime.Stackable;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Stacking.Runtime
{
    public sealed class UnstackPoint: MonoBehaviour
    {
        private static readonly Filter<UnstackPoint> Filter = Locator.Filter<UnstackPoint>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        public float radius = 2;
        
        public StackableDefinition[] Needs { get; set; }
        public event Action<StackableDefinition> OnUnstack;
        public void Unstack(StackableDefinition id) => OnUnstack?.Invoke(id);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() => Handles.DrawWireDisc(transform.position, Vector3.up, radius);
#endif
    }
}