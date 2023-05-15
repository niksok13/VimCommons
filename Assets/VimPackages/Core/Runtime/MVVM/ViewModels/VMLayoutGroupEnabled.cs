using UnityEngine.UI;

namespace VimPackages.Core.Runtime.MVVM.ViewModels
{
    public class VMLayoutGroupEnabled : AViewModel<bool,LayoutGroup>
    {
        protected override void OnValue(bool value)
        {
            Component.enabled = value;
        }
    }
}