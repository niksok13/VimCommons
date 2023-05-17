using UnityEngine.UI;

namespace VimCore.Runtime.MVVM.ViewModels
{
    public class VMLayoutElementExclude : AViewModel<bool, LayoutElement>
    {
        public bool invert;

        protected override void OnValue(bool value)
        {
            Component.ignoreLayout = value != invert;
        }
    }
}
