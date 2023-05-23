using VimCore.Runtime.DependencyManagement;
using VimLooting.Runtime.Core;
using VimLooting.Runtime.Inventory;
using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimQuestCommons.Runtime
{
    public class QuestLootAmount: AQuestCount
    {
        private static IInventory Inventory => Locator.Resolve<IInventory>();
        
        public LootableDefinition type;
        public int requiredAmount;

        public override int Current => Inventory.GetAmount(type);
        public override int Target => requiredAmount;
    }
}