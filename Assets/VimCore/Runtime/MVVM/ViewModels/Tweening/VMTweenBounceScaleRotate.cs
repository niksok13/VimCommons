using UnityEngine;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Pooling;

namespace VimCore.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenBounceScaleRotate : AViewModel<Transform>
    {
        public ParticleSystem fx;
        
        public override void OnSignal()
        {
            var Tf = transform;
            var initRot = Tf.localEulerAngles;
            var upperRot = initRot + Vector3.up * 360f;

            EZ.Spawn().Tween(0.5f, ez =>
            {
                Tf.localEulerAngles = Vector3.Lerp(initRot, upperRot, ez.QuintOut);
                Tf.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.one*2, ez.Bounce);
            }).Call(_ =>
            {
                if (fx)
                    fx.PlayOneShot(Tf.position);
            }).Tween(ez =>
            {
                Tf.localScale = new Vector3(1 + ez.Bounce / 3, 1 - ez.Bounce / 2, 1 + ez.Bounce / 3);
            });
        }
    }
}