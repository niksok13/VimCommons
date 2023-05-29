using System.Collections.Generic;
using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    public class ModelStackableConverter : ProgressionBuilding<StackableConverterLevelData>
    {
        public float radius = 2;
        public Transform unstackAnchor;
        public float unstackStep;

        private static readonly Filter<ModelStackableConverter> Filter = Locator.Filter<ModelStackableConverter>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private static readonly Filter<StackComponent> Stacks = Locator.Filter<StackComponent>();
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        private ObservableDictionary<StackableDefinition, int> StackableCount { get; } = new();
        public ObservableData<bool> IsWork { get; } = new();
        public ObservableData<bool> CanInteract { get; } = new();

        public Stack<ModelStackable> Result { get; } = new();

        private float _timer;

        public void Update()
        {
            if (NodeLevel.Value < 1) return;
            TickTopup();
            TickWithdraw();
            TickConversion();
            TickTrigger();
        }

        private void TickTrigger()
        {
            CanInteract.Value = Result.Count > 0;
        }

        private void TickWithdraw()
        {
            if (Result.Count < 1) return;
            var top = Result.Pop();
            foreach (var stack in Stacks)
            {
                if (!Helper.WithinRadius(stack.Transform, Transform, radius)) continue;
                if (stack.Push(top)) return;
            }
            Result.Push(top);
        }

        private void TickTopup()
        {
            foreach (var entry in LevelData.conversionFormula.source)
            {
                if (StackableCount[entry.type] >= entry.capacity) continue;
                foreach (var stack in Stacks)
                {
                    if (!Helper.WithinRadius(stack.Transform, Transform, radius)) continue;
                    var stackable = stack.Pop(entry.type);
                    if (stackable)
                    {
                        var posTo = Transform.position;
                        stackable.TweenRemove(posTo);
                        StackableCount[entry.type] += 1;
                        return;
                    }
                }
            }
        }

        private void TickConversion()
        {
            foreach (var entry in LevelData.conversionFormula.source)
            {
                if (StackableCount[entry.type] < entry.requirement)
                {
                    IsWork.Value = false;
                    return;
                }
            }
            IsWork.Value = true;
            
            if (!IsWork.Value) return;
            
            _timer += Time.deltaTime;
            if (_timer < LevelData.conversionTime) return;
            _timer = 0;
            
            FinishConversion();
        }

        private void FinishConversion()
        {
            foreach (var entry in LevelData.conversionFormula.source) 
                StackableCount[entry.type] -= entry.requirement;
            DropResult();
        }

        private void DropResult()
        {
            var stackable = LevelData.conversionFormula.result.Spawn();
            stackable.Init(unstackAnchor.position + unstackAnchor.up * unstackStep * Result.Count);
            stackable.Transform.rotation = unstackAnchor.rotation;
            Result.Push(stackable);
        }

        public bool HaveResult() => Result.Count > 0;

        public bool IsEmpty() => Result.Count < 1;

        public void Clear()
        {
            while (Result.TryPop(out var item)) item.Remove();
        }
    }
}
