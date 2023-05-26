using VimCore.Runtime.MVVM;

namespace VimCommons.Stacking.Runtime
{
    public struct SignalStackInteract : ISignal
    {
        public StackComponent Stack { get; }

        public SignalStackInteract(StackComponent stack)
        {
            Stack = stack;
        }
    }
}