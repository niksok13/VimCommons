using System;
using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Particles
{
    public class VMParticleActiveFloat : AViewModel<float, ParticleSystem>
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
            if (newState)
            {
                if(Component.isPlaying)return;
                Component.Play();
            }
            else
            {
                if(!Component.isPlaying)return;
                Component.Stop();
            }
        }
            
    }
}