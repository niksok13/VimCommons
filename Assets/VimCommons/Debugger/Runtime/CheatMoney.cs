using VimCommons.Looting.Runtime.Core;
using VimCommons.Looting.Runtime.Inventory;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Debugger.Runtime
{
    public class CheatMoney: ACheat<int>
    {
        public string command;
        public string description;
        public LootableDefinition loot;
        
        private static IInventory Inventory => Locator.Resolve<IInventory>();

        protected override string Command => command;
        protected override string Description => description;

        protected override void OnApply(int amount) => Inventory.Receive(loot, amount);
    }
}