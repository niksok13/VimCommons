using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindInteger : AViewModel<int, Animator>
    {
        public string parameter;

        protected override void OnValue(int value) => Component.SetInteger(parameter, value);
    }
}