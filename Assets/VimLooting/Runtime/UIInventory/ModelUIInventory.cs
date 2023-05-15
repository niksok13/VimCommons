using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimLooting.Runtime.Inventory;

namespace VimLooting.Runtime.UIInventory
{
    public class ModelUIInventory : ModelBehaviour
    {
        private static IInventory Inventory => Locator.Resolve<IInventory>();

        public ObservableData<string> BalanceLabel => Inventory.BalanceLabel;
 
    }
}
