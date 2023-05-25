using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using VimCommons.Stacking.Runtime.Stackable;

namespace VimCommons.Stacking.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConverterLevelData
    {
        public StackableConversionFormula conversionFormula;
        public float conversionTime;
    }

    [Serializable]
    public struct StackableConversionFormula
    {
        public List<StackableConversionEntry> source;
        public StackableDefinition result;
    }

    [Serializable]
    public struct StackableConversionEntry
    {
        public StackableDefinition type;
        public int requirement;
        public int capacity;
    }
}