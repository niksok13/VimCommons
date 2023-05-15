using System;
using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels
{
    public class VMActiveInt : AViewModel<int,Transform>
    {
        public ComparsionMode mode = ComparsionMode.Exact;
        public int condition;
        protected override void OnValue(int value)
        {
            var newState = mode switch
            {
                ComparsionMode.Exact => value == condition,
                ComparsionMode.Min => value >= condition,
                ComparsionMode.Max => value <= condition,
                _ => throw new ArgumentOutOfRangeException()
            };
            gameObject.SetActive(newState);
        }

    }
}
