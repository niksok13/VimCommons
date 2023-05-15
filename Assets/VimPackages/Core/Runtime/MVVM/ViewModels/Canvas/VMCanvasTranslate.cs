using UnityEngine;
using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Canvas
{
    public class VMCanvasTranslate : VMCanvas
    {
        public Vector2 offscreen;

        private EZ _ez;
        
        private RectTransform _window;
        protected RectTransform Window => _window ??= transform.GetChild(0) as RectTransform;

        protected override void OnValue(bool value)
        {
            if (Component.enabled == value) return;
            Component.enabled = true;
            Cg.enabled = true;
            if (Cs) Cs.enabled = true;
            if (Gr) Gr.enabled = false;

            var colorFrom = Cg.alpha;
            var posFrom = value ? offscreen :  Vector2.zero;

            var colorTo = value ? 1 : 0;
            var posTo = value ? Vector2.zero : offscreen;
            
            _ez?.Forward();
            _ez = EZ.Spawn().Tween(ez =>
            {
                Window.anchoredPosition = Vector2.LerpUnclamped(posFrom, posTo, ez.BackInOut);
                Cg.alpha = Mathf.Lerp(colorFrom, colorTo, ez.Linear);
            }).Call(_ =>
            {
                Window.anchoredPosition = value ? Vector2.zero : offscreen;
                Component.enabled = value;
                Cg.enabled = value;
                Cg.alpha = value ? 1 : 0;
                if (Gr) Gr.enabled = value;
                if (Cs) Cs.enabled = value;
            });
        }
    }
}