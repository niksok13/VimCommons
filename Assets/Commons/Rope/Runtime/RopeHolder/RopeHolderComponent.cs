using System.Collections.Generic;
using Commons.Rope.Runtime.JointRope;
using Core.Runtime.DependencyManagement;
using UnityEngine;

namespace Commons.Rope.Runtime.RopeHolder
{
    public class RopeHolderComponent : MonoBehaviour
    {
        private static readonly Filter<RopeHolderComponent> Filter = Locator.Filter<RopeHolderComponent>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private Transform _transform;
        public Transform Transform => _transform ??= GetComponent<Transform>();
        
        public readonly HashSet<ModelJointRope> Attached = new();
        public bool IsAttached => Attached.Count > 0;

        public void Attach(ModelJointRope rope) => Attached.Add(rope);

        public void Detach(ModelJointRope rope) => Attached.Remove(rope);

        public bool Holds(ModelJointRope rope) => Attached.Contains(rope);
    }
}