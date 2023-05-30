using VimCore.Runtime.MVVM;

namespace VimCommons.Triggers.Runtime.TriggerSystem
{
    public struct SignalTrigger: ISignal
    {
        public TriggerHitbox Trigger { get; }
        public TriggerInvoker Invoker { get; }
        public TriggerState State { get; }

        public SignalTrigger(TriggerHitbox trigger, TriggerInvoker invoker, TriggerState state)
        {
            Trigger = trigger;
            Invoker = invoker;
            State = state;
        }
    }
}