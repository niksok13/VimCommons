namespace VimCore.Runtime.MVVM.SignalEmitters.CanvasSlider
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