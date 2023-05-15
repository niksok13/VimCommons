using UnityEngine;
using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenToggleAngle: AViewModel<bool, GameObject>
    {
        public float duration = 0.3f;
        public Vector3 onTrue;
        public Vector3 onFalse;
        
        private EZ _ez;
        
        protected override void OnValue(bool value)
        {
            _ez?.Clear();
            var angleFrom = transform.localEulerAngles;
            var angleTo = value ? onTrue : onFalse;
            _ez = EZ.Spawn().Tween(duration, ez =>
            {
                transform.localEulerAngles = Vector3.LerpUnclamped(angleFrom, angleTo, ez.BackOut);
            });
        }
    }
}