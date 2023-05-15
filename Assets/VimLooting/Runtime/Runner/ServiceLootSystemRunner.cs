using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;
using VimLooting.Runtime.Core;

namespace VimLooting.Runtime.Runner
{
    public class ServiceLootSystemRunner : MonoBehaviour
    {
        private static readonly Filter<Lootable> Lootables = Locator.Filter<Lootable>();
        private static readonly Filter<ModelLootableStack> Stacks = Locator.Filter<ModelLootableStack>();
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