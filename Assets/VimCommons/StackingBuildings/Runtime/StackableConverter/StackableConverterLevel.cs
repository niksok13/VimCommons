using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VimCommons.Stacking.Runtime;

namespace VimCommons.StackingBuildings.Runtime.StackableConverter
{
    [CreateAssetMenu]
    public class StackableConverterLevel: ScriptableObject
    {
        public List<StackableConverterFormulaEntry> source;
        public float duration;
        public StackableDefinition result;
    }
}