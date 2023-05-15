using System;

namespace VimLooting.Runtime.Core
{
    [Serializable]
    public class LootEntry
    {
        public LootableDefinition type;
        public int amount;

        public LootEntry(LootableDefinition type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }
}