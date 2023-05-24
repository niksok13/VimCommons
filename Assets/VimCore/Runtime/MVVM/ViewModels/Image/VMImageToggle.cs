namespace VimCore.Runtime.MVVM.ViewModels.Image
{
    public class VMImageToggle : AViewModel<bool, UnityEngine.UI.Image>
    {
        public UnityEngine.Sprite onTrue;
        public UnityEngine.Sprite onFalse;

        protected override void OnValue(bool value)
        {
            Component.sprite = value ? onTrue : onFalse;
        }
    }
}
