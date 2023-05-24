using System;
using VimCommons.Stacking.Runtime.Stackable;

namespace VimCommons.Stacking.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConverterLevelData
    {
        public StackableDefinition targetDefinition;
        public float conversionTime;
    }
}