namespace VimCore.Runtime.MVVM.ViewModels.Slider
{
    public struct SignalSlider: ISignal
    {
        public readonly float Value;
        public SignalSlider(float value)
        {
            Value = value;
        }
    }
}