using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Mesh
{
    public class VMSkinnedMesh : AViewModel<UnityEngine.Mesh,SkinnedMeshRenderer>
    {
        protected override void OnValue(UnityEngine.Mesh value)
        {
            Component.sharedMesh = value;
        }
    }
}
