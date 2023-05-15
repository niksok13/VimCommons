using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using VimCore.Runtime.EZTween;

namespace VimRope.Runtime.JointRope
{
    public class ModelJointRope : MonoBehaviour
    {
        public int amount = 4;
    
        private LineRenderer _line;
        public LineRenderer Line => _line ??= GetComponent<LineRenderer>();

        public readonly Stack<Rigidbody> Chain = new();

        public Rigidbody Tail { get; private set; }
        private Transform Anchor { get; set; }
        public bool IsAttached { get; private set; }

        
        private void Start()
        {
            var tailRig = GetComponent<Rigidbody>();
            for (int i = 0; i < amount; i++)
            {
                var node = new GameObject();
                node.layer = gameObject.layer;
                node.transform.position = transform.position;
                var joint = node.AddComponent<SpringJoint>();
                var col = node.AddComponent<SphereCollider>();
                col.radius = 0.1f;
                joint.connectedBody = tailRig;
                joint.spring = 100;
                joint.damper = 10;
                tailRig = node.GetComponent<Rigidbody>();
                tailRig.mass = 0.1f;
                tailRig.drag = 0.1f;
                tailRig.angularDrag = 0.1f;
                tailRig.solverIterations = 16;
                tailRig.solverVelocityIterations = 16;
                tailRig.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                Chain.Push(tailRig);
            }

            Tail = Chain.Pop();
            Tail.isKinematic = true;
            Tail.AddForce(Vector3.forward);
            Anchor = transform;
            Line.enabled = false;
        }

        private void LateUpdate()
        {
            Tail.position = Anchor.position;
            var positions = Chain.Select(i => new BezierKnot(i.position)).ToList();
            positions.Insert(0, new BezierKnot(Tail.position));
            positions.Add(new BezierKnot(transform.position));
            var smooth = new Spline(positions);
            smooth.SetTangentMode(TangentMode.AutoSmooth);
            var smoothed = new Vector3[128];
            for (int i = 0; i < 128; i++)
            {
                smoothed[i] = smooth.EvaluatePosition(i / 127f);
            }
            Line.positionCount = 128;
            Line.SetPositions(smoothed);
        }

        public void Attach(Transform anchor)
        {
            foreach (var node in Chain)
            {
                node.isKinematic = false;
            }

            IsAttached = true;
            Line.enabled = true;
            Anchor = anchor;
        }
        
        public void Detach()
        {
            foreach (var node in Chain)
            {
                node.isKinematic = true;
            }
            Anchor = transform;

            IsAttached = false;
            EZ.Spawn().Tween(ez =>
            {
                foreach (var node in Chain) 
                    node.position = Vector3.Lerp(node.position, transform.position, ez.QuadIn);
            }).Call(_ =>
            {
                Anchor = transform;
                IsAttached = false;
                Line.enabled = false;
            });
        }

        public bool IsAttachedTo(Transform anchor) => Anchor == anchor;
    }
}
