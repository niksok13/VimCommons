using UnityEngine;
using VimCore.Runtime.EZTween;

namespace VimCore.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenTRS : AViewModel<GameObject>
    {
        public float duration = 0.3f;
        public AnimationCurve xPos = AnimationCurve.Linear(0,0,1,0);
        public AnimationCurve yPos = AnimationCurve.Linear(0,0,1,0);
        public AnimationCurve zPos = AnimationCurve.Linear(0,0,1,0);

        public AnimationCurve xRot = AnimationCurve.Linear(0,0,1,0);
        public AnimationCurve yRot = AnimationCurve.Linear(0,0,1,0);
        public AnimationCurve zRot = AnimationCurve.Linear(0,0,1,0);

        public AnimationCurve xScale = AnimationCurve.Linear(0,1,1,1);
        public AnimationCurve yScale = AnimationCurve.Linear(0,1,1,1);
        public AnimationCurve zScale = AnimationCurve.Linear(0,1,1,1);

        private readonly EZ _ez = EZ.Spawn();

        public override void OnSignal()
        {
            _ez.Forward();
            _ez.Tween(duration, ez =>
            {
                var t = ez.Linear;

                var xp = xPos.Evaluate(t);
                var yp = yPos.Evaluate(t);
                var zp = zPos.Evaluate(t);
                transform.localPosition = new Vector3(xp, yp, zp);

                var xr = xRot.Evaluate(t);
                var yr = yRot.Evaluate(t);
                var zr = zRot.Evaluate(t);
                transform.localEulerAngles = new Vector3(xr, yr, zr);

                var xs = xScale.Evaluate(t);
                var ys = yScale.Evaluate(t);
                var zs = zScale.Evaluate(t);
                transform.localScale = new Vector3(xs, ys, zs);
            });
        }

        private void OnDisable() => _ez.Forward();
    }
}