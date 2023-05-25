using System.Collections.Generic;
using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime.Stackable;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Stacking.Runtime.StackableConverter
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
        
        public Stack<ModelStackable> result = new();
        private StackableConversionFormula Formula => LevelData.conversionFormula;

        private float _timer;

        public void TickUpdate()
        {
            TickTopup();
            TickWithdraw();
            TickConversion();
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
            foreach (var entry in Formula.source)
            {
                if (StackableCount[entry.type] >= entry.capacity) continue;
                foreach (var stack in Stacks)
                {
                    if (!Helper.WithinRadius(stack.Transform, Transform, radius)) continue;
                    if (stack.TryPeek(entry.type, out var item))
                    {
                        var posTo = Transform.position;
                        item.TweenRemove(posTo);
                        StackableCount[entry.type] += 1;
                        return;
                    }
                }
            }
        }

        private void TickConversion()
        {
            foreach (var entry in Formula.source)
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
            foreach (var entry in Formula.source) 
                StackableCount[entry.type] -= entry.requirement;

            var item = Formula.result.Spawn();
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
