namespace VimCore.Runtime.MVVM.ViewModels.Image
{
    public class VMImageFill : AViewModel<float, UnityEngine.UI.Image>
    {
        protected override void OnValue(float value)
        {
            Component.fillAmount = value;
        }
    }
}
