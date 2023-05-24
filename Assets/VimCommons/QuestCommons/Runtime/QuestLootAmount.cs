using VimCommons.Looting.Runtime.Core;
using VimCommons.Looting.Runtime.Inventory;
using VimCommons.QuestQueue.Runtime.ServiceQuestQueue;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.QuestCommons.Runtime
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