using System;
using System.Collections.Generic;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    [Serializable]
    public struct StackableConversionFormula
    {
        public List<StackableConversionFormulaEntry> source;
        public StackableDefinition result;
    }
}