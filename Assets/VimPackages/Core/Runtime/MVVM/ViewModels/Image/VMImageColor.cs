using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Image
{
    public class VMImageColor : AViewModel<Color, UnityEngine.UI.Image>
    {
        protected override void OnValue(Color value)
        {
            Component.color = value;
        }

    }
}