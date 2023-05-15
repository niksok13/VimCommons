using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Particles
{
    public class VMParticleColor : AViewModel<Color, ParticleSystem>
    {
        protected override void OnValue(Color value)
        {
            var main = Component.main;
            main.startColor = value;
        }
    }
}
