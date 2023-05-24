using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Progression.Runtime.Trigger
{
    public class ProgressionTriggerInvoker : MonoBehaviour
    {
        private static readonly Filter<ModelProgressionTrigger> Triggers = Locator.Filter<ModelProgressionTrigger>();
        
        private Transform _transform;
        public Transform Transform => _transform ??= GetComponent<Transform>();

        public ObservableData<bool> IsWorking { get; } = new();

        private ModelProgressionTrigger _current;

        public void SetWorkAnimation(bool state) => IsWorking.Value = state;

        private void Update()
        {
            if (_current)
                TickStay();
            else
                TickSearch(); 
        }

        private void TickStay()
        {
            if (Helper.WithinRadius(Transform, _current.transform, _current.radius))
                _current.OnStay(this);
            else
            {
                _current.OnExit(this);
                _current = null;
            }
        }

        private void TickSearch()
        {
            foreach (var trigger in Triggers)
                if (Helper.WithinRadius(Transform, trigger.transform, trigger.radius))
                {
                    _current = trigger;
                    _current.OnEnter(this);
                    return;
                }
        }
    }
}