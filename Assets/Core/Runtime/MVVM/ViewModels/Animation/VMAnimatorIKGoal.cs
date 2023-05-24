using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorIKGoal : AViewModel<bool, Animator>
    {
        private float _targetWeight;
        private float _weight;

        public AvatarIKGoal goal;
        public Transform target;
        
        protected override void OnValue(bool value) => _targetWeight = value ? 1 : 0;

        private void OnAnimatorIK(int layerIndex)
        {
            if (!target) return;
            _weight = Mathf.MoveTowards(_weight, _targetWeight, Time.deltaTime*5);
            Component.SetIKPosition(goal, target.position);
            Component.SetIKRotation(goal, target.rotation);
            Component.SetIKPositionWeight(goal, _weight);
            Component.SetIKRotationWeight(goal, _weight);
        }
    }
}
