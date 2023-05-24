using Commons.Looting.Runtime.Inventory;
using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;

namespace Commons.Looting.Runtime.UIInventory
{
    public class ModelUIInventory : ModelBehaviour
    {
        private static IInventory Inventory => Locator.Resolve<IInventory>();

        public ObservableData<string> BalanceLabel => Inventory.BalanceLabel;
 
    }
}
