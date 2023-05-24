using System;
using Commons.Stacking.Runtime.Stackable;

namespace Commons.Stacking.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConverterLevelData
    {
        public StackableDefinition targetDefinition;
        public float conversionTime;
    }
}