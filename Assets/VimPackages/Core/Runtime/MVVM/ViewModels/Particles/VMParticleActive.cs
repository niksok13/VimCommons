using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Particles
{
    public class VMParticleActive : AViewModel<bool, ParticleSystem>
    {
        public bool invert;

        protected override void OnValue(bool value)
        {
            if (value!=invert)
                Component.Play();
            else
                Component.Stop();
        }
    }
}