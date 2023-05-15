namespace VimCore.Runtime.MVVM.ViewModels.Image
{
    public class VMImageContent : AViewModel<UnityEngine.Sprite, UnityEngine.UI.Image>
    {
        protected override void OnValue(UnityEngine.Sprite value)
        {
            Component.sprite = value;
        }
    }
}
