using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorIKLook : AViewModel<Transform, Animator>
    {
        public float lookWeight = 0.8f;

        private Transform _lookTarget;
        private Vector3 _lookPos;
        
        protected override void OnValue(Transform value)
        {
            _lookTarget = value;
        }

        private void LateUpdate()
        {
            _lookPos = Vector3.Lerp(_lookPos, _lookTarget.position, Time.deltaTime);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!_lookTarget) return;
            Component.SetLookAtPosition(_lookPos);
            Component.SetLookAtWeight(lookWeight);
        }
    }
}
