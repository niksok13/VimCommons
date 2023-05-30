using UnityEngine;
using VimCommons.Progression.Runtime.Building;
using VimCommons.Stacking.Runtime;
using VimCore.Runtime.MVVM;

namespace VimCommons.StackingBuildings.Runtime.StackableSource
{
    public class ModelStackableSource : ProgressionBuilding<StackableSourceLevel>
    {
        public StackableDefinition result;
        public Transform unstackAnchor;
        public ModelStackableBatch batch;
        
        public ObservableData<bool> IsWork { get; } = new();

        private float _timer;

        public void Update()
        {
            if (NodeLevel.Value < 1) return;
            TickConversion();
        }



        private void TickConversion()
        {
            IsWork.Value = batch.Count < LevelData.maxAmount;
            
            if (!IsWork.Value) return;
            
            _timer += Time.deltaTime;
            if (_timer < LevelData.cooldown) return;
            _timer = 0;
            
            FinishConversion();
        }

        private void FinishConversion()
        {
            var stackable = result.Spawn();
            stackable.Init(unstackAnchor.position);
            stackable.Transform.rotation = unstackAnchor.rotation;
            batch.Push(stackable);
        }
    }
}