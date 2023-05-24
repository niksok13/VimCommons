using Core.Runtime.Pooling;
using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Particles
{
    public class VMParticleOneShot : AViewModel<GameObject>
    {
        public ParticleSystem fx;
        public override async void OnSignal()
        {
            await fx.PlayOneShot(transform.position,transform.rotation,transform.lossyScale);
        }
    }
}