using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Mesh
{
    public class VMMeshColor : AViewModel<Color,MeshRenderer>
    {
        [SerializeField] private int matIndex;

        protected override void OnValue(Color value)
        {
            Component.materials[matIndex].color = value;
        }
    }
}