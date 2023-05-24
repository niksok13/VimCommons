using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Particles
{
    public class VMParticleGradient : AViewModel<float, ParticleSystem>
    {
        public Gradient colorRange;
        protected override void OnValue(float value)
        {
            var main = Component.main;
            main.startColor = colorRange.Evaluate(value);
        }
    }
}
