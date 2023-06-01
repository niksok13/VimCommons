using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;
using VimCore.Runtime.MVVM;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    public class ModelStackableConverter : ProgressionBuilding<StackableConverterLevel>
    {
        public Transform spawnPoint;
        public ModelStackableBatch batch;
        
        private Transform _transform;
        public Transform Transform => _transform ??= transform;

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

            foreach (var entry in LevelData.source)
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
            foreach (var entry in LevelData.source)
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
            if (_timer < LevelData.duration) return;
            _timer = 0;
            
            FinishConversion();
        }

        private void FinishConversion()
        {
            foreach (var entry in LevelData.source) 
                StackableCount[entry.type] -= entry.requirement;
            DropResult();
        }

        private void DropResult()
        {
            var stackable = LevelData.result.Spawn();
            stackable.Init(spawnPoint);
            batch.Push(stackable);
        }
    }
}
