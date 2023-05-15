using UnityEngine;
using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenScale: AViewModel<GameObject>
    {
        public float duration = 0.3f;
        public AnimationCurve xCurve;
        public AnimationCurve yCurve;
        public AnimationCurve zCurve;
        
        private readonly EZ _ez = EZ.Spawn();

        public override void OnSignal()
        {
            _ez.Forward();
            _ez.Tween(duration, ez =>
            {
                var t = ez.Linear;
                var x = xCurve.Evaluate(t);
                var y = yCurve.Evaluate(t);
                var z = zCurve.Evaluate(t);
                transform.localScale = new Vector3(x, y, z);
            });
        }
    }
}