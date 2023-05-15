using System.Collections.Generic;
using VimCore.Runtime.MVVM;
using VimLooting.Runtime.Core;

namespace VimLooting.Runtime.Inventory
{
    public interface IInventory
    {
        void Receive(LootableDefinition type, int amount);
        int GetAmount(LootableDefinition type);
        bool CanPay(List<LootEntry> nextUpgradeCost);
        bool TryPay(List<LootEntry> upgradeCost);
        ObservableData<string> BalanceLabel { get; }
    }
}