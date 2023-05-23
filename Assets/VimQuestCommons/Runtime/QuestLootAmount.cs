using VimCore.Runtime.DependencyManagement;
using VimLooting.Runtime.Core;
using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimLooting.Runtime.Inventory
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