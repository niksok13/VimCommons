using System;
using UnityEngine;

namespace VimCommons.Looting.Runtime.Core
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

        public void Animate(Transform from, Transform to) => type.Animate(amount, from, to);
    }
}