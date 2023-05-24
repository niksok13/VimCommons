using VimCommons.Looting.Runtime.Inventory;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.UIInventory
{
    public class ModelUIInventory : ModelBehaviour
    {
        private static IInventory Inventory => Locator.Resolve<IInventory>();

        public ObservableData<string> BalanceLabel => Inventory.BalanceLabel;
 
    }
}
