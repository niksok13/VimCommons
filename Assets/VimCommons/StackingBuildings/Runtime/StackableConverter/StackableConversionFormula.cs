using System;
using System.Collections.Generic;
using UnityEngine;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    [CreateAssetMenu]
    public class StackableConversionFormula: ScriptableObject
    {
        public List<StackableConversionFormulaEntry> source;
        public StackableDefinition result;
    }
}