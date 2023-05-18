using UnityEngine;
using VimCore.Runtime.MVVM;
using VimRoulette.Runtime.Reward;

namespace VimRoulette.Runtime.Roulette
{
    public class VMSectorBuilder : AViewModel<RouletteReward[],Transform>
    {
        public VisualSector.VisualSector prefab;


        protected override void OnValue(RouletteReward[] value)
        {
            var sectorAngle = 360 / value.Length;
            for (var index = 0; index < value.Length; index++)
            {
                var reward = value[index];
                var entry = Instantiate(prefab, transform);
                entry.transform.localEulerAngles = Vector3.forward * sectorAngle * (index+0.5f);
                entry.Init(reward);
            }
        }
    }
}
