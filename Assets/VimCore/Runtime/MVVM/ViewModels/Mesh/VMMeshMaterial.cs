using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Mesh
{
    public class VMMeshMaterial : AViewModel<Material,MeshRenderer>
    {
        protected override void OnValue(Material value)
        {
            Component.material = value;
        }
    }
}
