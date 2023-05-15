using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Mesh
{
    public class VMSkinnedMeshColor : AViewModel<Color, SkinnedMeshRenderer>
    {
        public int matIndex;

        protected override void OnValue(Color value)
        {
            Component.materials[matIndex].color = value;
        }
    }
}
