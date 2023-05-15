namespace VimPackages.Core.Runtime.MVVM.ViewModels.SpriteRenderer
{
    public class VMSpriteRendererContent : AViewModel<UnityEngine.Sprite,UnityEngine.SpriteRenderer>
    {
        protected override void OnValue(UnityEngine.Sprite value)
        {
            Component.sprite = value;
        }
    }
}