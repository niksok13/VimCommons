using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Utils;

namespace VimCommons.Stacking.Runtime
{
    public class StackComponent: MonoBehaviour
    {
        public bool ignoreCooldown;
        public int capacity = 4;

        private static readonly Filter<StackComponent> Filter = Locator.Filter<StackComponent>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private static readonly Filter<StackInteractor> Interactors = Locator.Filter<StackInteractor>();
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        private readonly List<ModelStackable> _stack = new();
        private readonly Vector3[] _items = new Vector3[300];
        private int _pending;
        private float _timeout;
        
        private void Update()
        {
            foreach (var item in Interactors) 
                item.Interact(this);
        }

        private void LateUpdate()
        {
            var voffset = 0f;
            var anchor = Transform.position;
            var up = Transform.up;
            var delta = LoopUtil.Delta;
            var rot = Transform.rotation;
            for (var i = 0; i < _stack.Count; i++)
            {
                var item = _stack[i];
                var targetPos = anchor + up * voffset;
                voffset += item.height;
                var lerpArg = delta * 30 / (voffset + 1);
                _items[i] = Vector3.Lerp(_items[i], targetPos, lerpArg);
                item.Transform.position = _items[i];
                item.Transform.rotation = rot;
            }
        }

        public event Action<List<ModelStackable>> OnUpdate;

        public bool Full => Count + _pending >= capacity;
        public int Count => _stack.Count;

        public bool Ready(float cooldown)
        {
            if (ignoreCooldown) return true;
            _timeout += Time.deltaTime;
            if (_timeout < cooldown) return false;
            _timeout = 0;
            return true;
        }
        
        private int CalculateWeight()
        {
            var sum = 0;
            foreach (var i in _stack) 
                sum += i.Weight;
            return sum + _pending;
        }
        
        public bool HaveSpace(int weight) => CalculateWeight() + weight <= capacity;

        public Vector3 TopPoint(float resHeight)
        {
            var offset = resHeight;
            for (var i = 0; i < _stack.Count; i++) 
                offset += _stack[i].height;
            return Transform.position + Transform.up * offset;
        }

        public int CountTyped(StackableDefinition definition)
        {
            return _stack.Count(i=> i.Definition == definition);
        }

        public bool HaveAny(params StackableDefinition[] variants)
        {
            return _stack.Any(i => variants.Contains(i.Definition));
        }

        public bool Push(ModelStackable res)
        {
            if (!HaveSpace(res.Weight)) return false;
            if (!res.Ready) return false;
            res.Pick();
            var posFrom = res.Transform.position;
            var posTo = TopPoint(res.height);
            var rotFrom = res.Transform.rotation;
            var rotTo = Transform.rotation;
            _pending += res.Weight;
            EZ.Spawn().Tween(ez =>
            {
                res.Transform.position = Helper.LerpParabolic(posFrom, posTo, ez.QuadOutIn, 2);
                res.Transform.rotation = Quaternion.Lerp(rotFrom, rotTo, ez.Linear);
                res.Transform.localScale = Vector3.one * ez.BackOut;
            }).Call(_ =>
            {
                _pending -= res.Weight;
                _items[_stack.Count] = posTo;
                _stack.Add(res);
                OnUpdate?.Invoke(_stack);
            });
            return true;
        }

        public ModelStackable Peek(params StackableDefinition[] needs)
        {
            foreach (var stackable in _stack)
                if (needs.Contains(stackable.Definition))
                {
                    _stack.Remove(stackable);
                    OnUpdate?.Invoke(_stack);
                    return stackable;
                }
            return null;
        }
    }
}