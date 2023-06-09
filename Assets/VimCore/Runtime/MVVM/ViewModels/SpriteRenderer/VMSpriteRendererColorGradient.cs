using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.SpriteRenderer
{
    public class VMSpriteRendererColorGradient : AViewModel<float,UnityEngine.SpriteRenderer>
    {
        public Gradient gradient;
        protected override void OnValue(float value) => Component.color = gradient.Evaluate(value);
    }
}