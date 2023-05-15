namespace VimPackages.Core.Runtime.MVVM.ViewModels.SpriteRenderer
{
    public class VMSpriteRendererToggle : AViewModel<bool, UnityEngine.SpriteRenderer>
    {
        public UnityEngine.Sprite onTrue;
        public UnityEngine.Sprite onFalse;

        protected override void OnValue(bool value)
        {
            Component.sprite = value ? onTrue : onFalse;
        }
    }
}