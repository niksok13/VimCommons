using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Pooling;

namespace VimCommons.QuestQueue.Runtime.QuestArrows
{
    public class ServiceQuestArrowSystem : MonoBehaviour, IQuestArrowSystem
    {
        public LineRenderer arrowPrefab;
        public float vOffset = 1;

        private static readonly ServiceContainer<IQuestArrowSystem> Container = Locator.Single<IQuestArrowSystem>();
        public void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        public PrefabPool<LineRenderer> Pool => _pool ??= PrefabPool<LineRenderer>.Instance(arrowPrefab);
        private PrefabPool<LineRenderer> _pool;
        
        private readonly Dictionary<Transform, LineRenderer> _data = new();
        private Transform _anchorTransform;

        private void Update()
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
                Pool.Remove(arrow);
        }
        
        private void RunArrow(Transform target, LineRenderer arrow)
        {
            if (!_anchorTransform) return;

            var from = _anchorTransform.position;
            var to = target.position + Vector3.up * 3;
            
            var trimFrom = Vector3.MoveTowards(from, to, 1);
            var trimTo = Vector3.MoveTowards(to, from, 1);
            
            trimFrom.y = vOffset;
            trimTo.y = vOffset;

            arrow.SetPosition(0, trimFrom);
            arrow.SetPosition(1, trimTo);
        }

        public void Add(params Transform[] targets)
        {
            foreach (var item in targets) 
                AddTarget(item);
        }

        private void AddTarget(Transform target)
        {
            if (_data.ContainsKey(target)) return; 
            var newArr = Pool.Spawn();
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