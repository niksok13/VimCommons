using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.SpriteRenderer
{
    public class VMSpriteColor : AViewModel<Color,UnityEngine.SpriteRenderer>
    {
        protected override void OnValue(Color value)
        {
            Component.color = value;
        }
    }
}
