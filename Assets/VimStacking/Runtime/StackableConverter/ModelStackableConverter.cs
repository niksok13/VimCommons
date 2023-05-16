using System.Collections.Generic;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;
using VimProgression.Runtime.Building;
using VimStacking.Runtime.Stackable;

namespace VimStacking.Runtime.StackableConverter
{
    public class ModelStackableConverter : ProgressionBuilding<StackableConverterLevelData>
    {
        public float radius = 2;
        public int capacity = 4;
        public Transform unstackAnchor;
        public float unstackStep;
        public StackableDefinition sourceDefinition;

        private static readonly Filter<ModelStackableConverter> Filter = Locator.Filter<ModelStackableConverter>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private static readonly Filter<StackComponent> Stacks = Locator.Filter<StackComponent>();
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        
        public ObservableData<int> QueueCount { get; } = new();
        public ObservableData<bool> IsWork { get; } = new();
        public ObservableData<bool> CanInteract { get; } = new();
        
        public Stack<ModelStackable> result = new();

        private float _timer;

        private void Awake()
        {
            QueueCount.OnValue += OnCount;
        }

        private void OnCount(int obj)
        {
            IsWork.Value = obj > 0;
        }

        public void TickUpdate()
        {
            TickTopup();
            TickWithdraw();
            TickProcess();
            TickTrigger();
        }

        private void TickTrigger()
        {
            CanInteract.Value = result.Count > 0;
        }

        private void TickWithdraw()
        {
            if (result.Count < 1) return;
            var top = result.Pop();
            foreach (var stack in Stacks)
            {
                if (!Helper.WithinRadius(stack.Transform, Transform, radius)) continue;
                if (stack.Pick(top)) return;
            }
            result.Push(top);
        }

        private void TickTopup()
        {
            if (QueueCount.Value >= capacity) return;
            foreach (var stack in Stacks)
            {
                if (!Helper.WithinRadius(stack.Transform, Transform, radius)) continue;
                if (stack.TryPeek(sourceDefinition, out var item))
                {
                    var posTo = Transform.position;
                    item.TweenRemove(posTo);
                    QueueCount.Value += 1;
                    return;
                }
            }
        }

        private void TickProcess()
        { 
            if (QueueCount.Value < 1) return;
            _timer += Time.deltaTime;
            if (_timer < LevelData.conversionTime) return;
            _timer = 0;
            QueueCount.Value -= 1;
            var item = LevelData.targetDefinition.Spawn();
            item.Init(unstackAnchor.position + unstackAnchor.up * unstackStep * result.Count);
            item.Transform.rotation = unstackAnchor.rotation;
            result.Push(item);
        }

        public bool HaveResult() => result.Count > 0;

        public bool IsEmpty() => result.Count < 1;

        public void Clear()
        {
            while (result.TryPop(out var item)) item.Remove();
        }
    }
}
