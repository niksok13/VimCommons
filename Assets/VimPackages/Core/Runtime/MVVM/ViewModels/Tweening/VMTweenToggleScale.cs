using UnityEngine;
using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenToggleScale: AViewModel<bool, GameObject>
    {
        public float duration = 0.3f;
        public Vector3 onTrue;
        public Vector3 onFalse;
        
        private EZ _ez;
        
        protected override void OnValue(bool value)
        {
            _ez?.Clear();
            var scaleFrom = transform.localScale;
            var scaleTo = value ? onTrue : onFalse;
            _ez = EZ.Spawn().Tween(duration, ez =>
            {
                transform.localScale = Vector3.LerpUnclamped(scaleFrom, scaleTo, ez.BackOut);
            });
        }
    }
}