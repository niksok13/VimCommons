using Core.Runtime.MVVM;
using UnityEngine;

namespace Commons.Roulette.Runtime.Roulette
{
    public class VMRotateFloat : AViewModel<float, Transform>
    {

        protected override void OnValue(float value)
        {
            Component.localEulerAngles = Vector3.back * value;
        }
    }
}
