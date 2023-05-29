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

        private static readonly Filter<ModelStackableConverter> Filter = Locator.Filter<ModelStackableConverter>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;
        private ModelStackableBatch _batch;
        public ModelStackableBatch Batch => _batch ??= GetComponentInChildren<ModelStackableBatch>(); 

        private ObservableDictionary<StackableDefinition, int> StackableCount { get; } = new();
        public ObservableData<bool> IsWork { get; } = new();

        private float _timer;

        public void Update()
        {
            if (NodeLevel.Value < 1) return;
            TickConversion();
        }

        public void OnStack(SignalStackInteract signal)
        {
            var stack = signal.Stack;

            foreach (var entry in LevelData.conversionFormula.source)
            {
                if (StackableCount[entry.type] >= entry.capacity) continue;
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
            stackable.Init(unstackAnchor.position);
            stackable.Transform.rotation = unstackAnchor.rotation;
            Batch.Push(stackable);
        }

        public bool HaveResult() => Batch.Count > 0;

        public bool IsEmpty() => Batch.Count < 1;

    }
}
