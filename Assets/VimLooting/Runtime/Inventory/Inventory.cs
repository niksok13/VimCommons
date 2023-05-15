using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimLooting.Runtime.Core;

namespace VimLooting.Runtime.Inventory
{
    public class Inventory: MonoBehaviour, IInventory
    {
        private const string Key = "__lootable_balance";

        private static readonly ServiceContainer<IInventory> Container = Locator.Single<IInventory>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private Dictionary<int, int> _data = new();
        public ObservableData<string> BalanceLabel { get; } = new();

        private void Start()
        {
            var strBalance = PlayerPrefs.GetString(Key, "");
            if (strBalance.Length < 1)
            {
                BalanceLabel.Value = "0";
                return;
            }
            _data = JsonConvert.DeserializeObject<Dictionary<int, int>>(strBalance);
            BalanceUpdated();
        }

        public int GetAmount(LootableDefinition type) => _data[type.GetHashCode()];
        
        private void BalanceUpdated()
        {
            var strBalance = JsonConvert.SerializeObject(_data);
            PlayerPrefs.SetString(Key, strBalance);
            PlayerPrefs.Save();
            var result = "";
            foreach (var entry in _data) 
                result += $"{entry.Value}";

            BalanceLabel.Value = result;
        }

        public bool CanPay(List<LootEntry> valueEstimate)
        {
            foreach (var entry in valueEstimate)
            {
                var id = entry.type.GetHashCode();
                if (!_data.ContainsKey(id))
                    _data[id] = 0;
                if (_data[id] < entry.amount)
                    return false;
            }
            return true;
        }

        public bool TryPay(List<LootEntry> valueEstimate)
        {
            if (!CanPay(valueEstimate)) return false;
            foreach (var entry in valueEstimate) 
                _data[entry.type.GetHashCode()] -= entry.amount;
            BalanceUpdated();
            return true;
        }

        public void Receive(LootableDefinition type, int amount)
        {
            var id = type.GetHashCode();
            if (!_data.ContainsKey(id))
                _data[id] = 0;
            _data[id] += amount;
            BalanceUpdated();
        }
    }
}