using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindFloat : AViewModel<float, Animator>
    {
        public string parameter;

        protected override void OnValue(float value) => Component.SetFloat(parameter, value);
    }
}