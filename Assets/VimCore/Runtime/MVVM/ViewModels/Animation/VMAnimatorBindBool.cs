using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindBool : AViewModel<bool, Animator>
    {
        public string parameter;

        protected override void OnValue(bool value) => Component.SetBool(parameter, value);
    }
}