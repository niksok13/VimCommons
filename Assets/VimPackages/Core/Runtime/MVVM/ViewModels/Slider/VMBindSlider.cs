using VimPackages.Core.Runtime.EZTween;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Slider
{
    public class VMBindSlider: ASignalEmitter<SignalSlider, UnityEngine.UI.Slider>
    {
        private void Awake() => EZ.Spawn().Delay().Call(Init);

        private void Init(EZData data) => Component.onValueChanged.AddListener(OnSlider);

        private void OnSlider(float arg0) => Emit(new SignalSlider(arg0));
    }
}
