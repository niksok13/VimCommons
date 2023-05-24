using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Mesh
{
    public class VMMesh : AViewModel<UnityEngine.Mesh,MeshFilter>
    {
        protected override void OnValue(UnityEngine.Mesh value)
        {
            Component.sharedMesh = value;
        }
    }
}
