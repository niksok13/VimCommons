using UnityEngine;
using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenBounce : AViewModel<Transform>
    {
        public override void OnSignal()
        {
            var tf = transform;

            EZ.Spawn().Tween(ez =>
            {
                tf.localScale = new Vector3(1 + ez.Bounce / 3, 1 - ez.Bounce / 2, 1 + ez.Bounce / 3);
            });
        }
    }
}