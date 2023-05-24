using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Tweening
{
    public class VMToggleRotator: AViewModel<bool, GameObject>
    {
        public Vector3 eulerStep;
        private bool _state;


        protected override void OnValue(bool value)
        {
            _state = value;
        }

        private void LateUpdate()
        {
            if (!_state) return;
            transform.Rotate(eulerStep, Space.Self);
        }
    }
}