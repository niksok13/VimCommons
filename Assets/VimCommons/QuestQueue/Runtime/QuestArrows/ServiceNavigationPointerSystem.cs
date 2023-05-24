using System.Collections.Generic;
using Core.Runtime.DependencyManagement;
using Core.Runtime.Pooling;
using Core.Runtime.Utils;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.QuestArrows
{
    public class ServiceNavigationPointerSystem : MonoBehaviour, INavigationPointerSystem
    {
        private static readonly ServiceContainer<INavigationPointerSystem> Container = Locator.Single<INavigationPointerSystem>();

        private readonly Dictionary<Transform, LineRenderer> _data = new();

        public float vOffset = 1;

        public LineRenderer arrowPrefab;

        private PrefabPool<LineRenderer> _pool;
        private Transform _anchorTransform;

        public void Awake()
        {
            Container.Attach(this);
            _pool = PrefabPool<LineRenderer>.Instance(arrowPrefab);
            LoopUtil.PostLateUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PostLateUpdate -= Tick;
        }


        private void Tick()
        {
            foreach (var (target, arrow) in _data)
            {
                if (!target) {
                    RemoveTarget(target);
                    return;
                }
                RunArrow(target, arrow);
            }
        }

        private void RemoveTarget(Transform target)
        {
            if (_data.Remove(target, out var arrow)) 
                _pool.Remove(arrow);
        }
        
        private void RunArrow(Transform target, LineRenderer arrow)
        {
            if (!_anchorTransform) return;

            var from = _anchorTransform.position;
            var to = target.position + Vector3.up * 3;
            
            var a = Vector3.MoveTowards(from, to, 1);
            var b = Vector3.MoveTowards(to, from, 1);
            
            a.y = vOffset;
            b.y = vOffset;

            arrow.SetPosition(0, a);
            arrow.SetPosition(1, b);
        }

        public void Add(params Transform[] targets)
        {
            foreach (var item in targets) 
                AddTarget(item);
        }

        private void AddTarget(Transform target)
        {
            if (_data.ContainsKey(target)) return; 
            var newArr = _pool.Spawn();
            newArr.transform.localScale = Vector3.one;
            newArr.positionCount = 2;

            _data[target] = newArr;
        }

        public void Remove(params Transform[] targets)
        {
            foreach (var item in targets) 
                RemoveTarget(item);
        }
        
        public void SetAnchor(Transform anchor)
        {
            _anchorTransform = anchor;
        }
    }
}