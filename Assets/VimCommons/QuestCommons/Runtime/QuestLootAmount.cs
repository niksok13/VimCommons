using Commons.Looting.Runtime.Core;
using Commons.Looting.Runtime.Inventory;
using Commons.QuestQueue.Runtime.ServiceQuestQueue;
using Core.Runtime.DependencyManagement;

namespace Commons.QuestCommons.Runtime
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