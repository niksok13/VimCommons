using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindInteger : AViewModel<int, Animator>
    {
        public string parameter;

        protected override void OnValue(int value) => Component.SetInteger(parameter, value);
    }
}