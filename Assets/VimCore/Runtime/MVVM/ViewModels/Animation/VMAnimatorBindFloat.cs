using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindFloat : AViewModel<float, Animator>
    {
        public string parameter;

        protected override void OnValue(float value) => Component.SetFloat(parameter, value);
    }
}