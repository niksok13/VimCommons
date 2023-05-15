using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Mesh
{
    public class VMBlendShape : AViewModel<float,SkinnedMeshRenderer>
    {
        [SerializeField] private int index;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 100;


        protected override void OnValue(float value)
        {
            var target = Mathf.Lerp(minValue,maxValue,value);
            Component.SetBlendShapeWeight(index,target);
        }
    }
}
