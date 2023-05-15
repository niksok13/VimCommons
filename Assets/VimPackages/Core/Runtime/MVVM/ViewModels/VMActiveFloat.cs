using System;
using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels
{
    public class VMActiveFloat : AViewModel<float,Transform>
    {
        public ComparsionMode mode = ComparsionMode.Exact;
        public float condition;
        protected override void OnValue(float value)
        {
            var newState = mode switch
            {
                ComparsionMode.Exact => Math.Abs(value - condition) < float.Epsilon,
                ComparsionMode.Min => value >= condition,
                ComparsionMode.Max => value <= condition,
                _ => throw new ArgumentOutOfRangeException()
            };
            gameObject.SetActive(newState);
        }

    }
}