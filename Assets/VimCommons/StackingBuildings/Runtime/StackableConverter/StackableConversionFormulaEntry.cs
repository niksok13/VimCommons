using System;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConversionFormulaEntry
    {
        public StackableDefinition type;
        public int requirement;
        public int capacity;
    }
}