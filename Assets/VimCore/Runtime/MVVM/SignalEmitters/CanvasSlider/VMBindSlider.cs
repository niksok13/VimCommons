using UnityEngine.UI;
using VimCore.Runtime.EZTween;

namespace VimCore.Runtime.MVVM.SignalEmitters.CanvasSlider
{
    public class VMBindSlider: ASignalEmitter<SignalSlider>
    {
        private Slider _component;
        protected Slider Component => _component ??= GetComponent<Slider>();
        private void Awake() => EZ.Spawn().Delay().Call(Init);

        private void Init(EZData data) => Component.onValueChanged.AddListener(OnSlider);

        private void OnSlider(float arg0) => Emit(new SignalSlider(arg0));
    }
}
