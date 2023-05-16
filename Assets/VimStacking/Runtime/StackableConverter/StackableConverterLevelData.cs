using System;
using VimStacking.Runtime.Stackable;

namespace VimStacking.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConverterLevelData
    {
        public StackableDefinition targetDefinition;
        public float conversionTime;
    }
}