using UnityEngine;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Pooling;

namespace VimCore.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenDrop : AViewModel<Transform>
    {
        public UnityEngine.ParticleSystem fx;
        
        public override void OnSignal()
        {
            var Tf = transform;
            var initPos = Tf.localPosition;
            var initRot = Tf.localEulerAngles;
            var upperPos = initPos + Vector3.up * 2;
            var upperRot = initRot + Vector3.forward * 45f;

            Tf.localPosition = upperPos;
            EZ.Spawn().Tween(1, ez =>
            {
                Tf.localPosition = Vector3.Lerp(initPos, upperPos, ez.QuintOut);
                Tf.localEulerAngles = Vector3.Lerp(initRot, upperRot, ez.QuadOut);
                Tf.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, ez.BackOut);
            }).Tween(0.15f, ez =>
            {
                Tf.localPosition = Vector3.Lerp(upperPos, initPos, ez.QuadOut);
                Tf.localEulerAngles = Vector3.Lerp(upperRot, initRot, ez.QuadOut);
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
