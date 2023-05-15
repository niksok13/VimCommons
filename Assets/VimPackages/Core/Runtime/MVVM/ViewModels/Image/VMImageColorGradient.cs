using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Image
{
    public class VMImageColorGradient : AViewModel<float,UnityEngine.UI.Image>
    {
        public Gradient gradient;
        protected override void OnValue(float value) => Component.color = gradient.Evaluate(value);
    }
}