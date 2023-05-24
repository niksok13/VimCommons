using Core.Runtime.MVVM;
using UnityEngine;

namespace Core.Runtime.Utils.RadialFill
{
    public class VMSpriteFill : AViewModel<float, SpriteRenderer>
    {
        private const string key = "_Arc1";
        public bool invert;

        protected override void OnValue(float value)
        {
            if (invert)
                value = 1 - value;
        
            var angle = Mathf.Lerp(0, 360, value);
            Component.material.SetFloat(key,angle);
        }

    }
}
