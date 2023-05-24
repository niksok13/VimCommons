using System.Collections.Generic;
using VimCommons.Looting.Runtime.Core;
using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.Inventory
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