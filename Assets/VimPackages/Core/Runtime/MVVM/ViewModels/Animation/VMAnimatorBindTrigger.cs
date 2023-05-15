using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Animation
{
    public class VMAnimatorBindTrigger : AViewModel<Animator>
    {
        public string parameter;

        public override void OnSignal() => Component.SetTrigger(parameter);
    }
}