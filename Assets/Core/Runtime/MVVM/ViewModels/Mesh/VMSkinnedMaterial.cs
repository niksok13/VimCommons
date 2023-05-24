using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Mesh
{
    public class VMSkinnedMaterial : AViewModel<Material, SkinnedMeshRenderer>
    {
        protected override void OnValue(Material value)
        {
            Component.material = value;
        }
    }
}
