using UnityEngine;
using VimCommons.Looting.Runtime.Core;
using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.ViewModels
{
    
    public class VMLootSpawner : AViewModel<Transform>
    {
        [Range(1, 10)]
        public int amount;
        public LootableDefinition loot;

        public override void OnSignal()
        {
            for (int i = 0; i < amount; i++) 
                SpawnLootable();
        }

        private void SpawnLootable()
        {
            var posFrom = transform.position;
            var offset =  Random.insideUnitSphere;
            offset.y = 0;
            offset.Normalize();
            var posTo = posFrom + offset;
            loot.Spawn(posFrom, posTo);
        }
    }
}