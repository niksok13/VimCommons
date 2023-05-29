using UnityEngine;
using VimCommons.Looting.Runtime.Core;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Looting.Runtime.Runner
{
    public class ServiceLootSystemRunner : MonoBehaviour
    {
        private static readonly Filter<ModelLootable> Lootables = Locator.Filter<ModelLootable>();
        private static readonly Filter<ModelLootableBatch> Stacks = Locator.Filter<ModelLootableBatch>();
        private static readonly Filter<LooterComponent> Looters = Locator.Filter<LooterComponent>();
        public void Awake() => LoopUtil.PreUpdate += Tick;
        private void OnDestroy() => LoopUtil.PreUpdate -= Tick;

        private static void Tick()
        {
            foreach (var looter in Looters)
            {
                foreach (var lootable in Lootables)
                    lootable.Tick(looter);
                foreach (var stack in Stacks)
                    stack.Tick(looter);
            }
        }
    }
}