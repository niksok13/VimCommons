using Core.Runtime.EZTween;
using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Tweening
{
    public class VMTweenClip : AViewModel<Transform>
    {
        public AnimationClip clip;
        
        private readonly EZ _ez = EZ.Spawn();

        public override void OnSignal()
        {
            _ez.Forward();
            _ez.Tween(clip.length, TickAnimation);
        }

        private void TickAnimation(EZData ez)
        {
            clip.SampleAnimation(gameObject, ez.Time);
        }
    }
}