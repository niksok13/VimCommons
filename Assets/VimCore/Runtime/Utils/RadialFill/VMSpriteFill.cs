using UnityEngine;
using VimCore.Runtime.MVVM;

namespace VimCore.Runtime.Utils.RadialFill
{
    public class VMSpriteFill : AViewModel<float, SpriteRenderer>
    {
        public bool invert;

        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        protected override void OnValue(float value)
        {
            if (invert)
                value = 1 - value;
        
            var angle = Mathf.Lerp(0, 360, value);
            Component.material.SetFloat(Arc1, angle);
        }

    }
}
