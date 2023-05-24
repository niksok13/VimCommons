using System.Collections.Generic;
using Commons.Looting.Runtime.Core;
using Core.Runtime.MVVM;

namespace Commons.Looting.Runtime.Inventory
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