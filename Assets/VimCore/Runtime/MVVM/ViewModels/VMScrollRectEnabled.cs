using UnityEngine.UI;

namespace VimCore.Runtime.MVVM.ViewModels
{
    public class VMScrollRectEnabled : AViewModel<bool,ScrollRect>
    {
        protected override void OnValue(bool value)
        {
            Component.enabled = value;
        }
    }
}