using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.Core
{
    public struct SignalLoot : ISignal
    {
        public LootableDefinition Definition { get; }

        public SignalLoot(LootableDefinition definition)
        {
            Definition = definition;
        }
    }
}