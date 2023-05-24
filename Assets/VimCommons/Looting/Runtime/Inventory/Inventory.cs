using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VimCommons.Looting.Runtime.Core;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;

namespace VimCommons.Looting.Runtime.Inventory
{
    public class Inventory: MonoBehaviour, IInventory
    {
        private const string Key = "__lootable_balance";

        private static readonly ServiceContainer<IInventory> Container = Locator.Single<IInventory>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private Dictionary<string, int> _data = new();
        private EZ _ez;
        public ObservableData<string> BalanceLabel { get; } = new();

        private void Start()
        {
            _ez = EZ.Spawn();
            var strBalance = PlayerPrefs.GetString(Key, "");
            if (strBalance.Length < 1)
            {
                BalanceLabel.Value = "0";
                return;
            }
            _data = JsonConvert.DeserializeObject<Dictionary<string, int>>(strBalance);
            BalanceUpdated();
        }

        public int GetAmount(LootableDefinition type) => _data[type.guid];
        
        private void BalanceUpdated()
        {
            _ez.Clear();
            _ez.Delay().Call(SaveBalance);
            var result = "";
            foreach (var entry in _data) 
                result += $"{entry.Value}";
            BalanceLabel.Value = result;
        }

        private void SaveBalance(EZData obj)
        {
            var strBalance = JsonConvert.SerializeObject(_data);
            PlayerPrefs.SetString(Key, strBalance);
            PlayerPrefs.Save();
        }

        public bool CanPay(List<LootEntry> valueEstimate)
        {
            foreach (var entry in valueEstimate)
            {
                var id = entry.type.guid;
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
                _data[entry.type.guid] -= entry.amount;
            BalanceUpdated();
            return true;
        }

        public void Receive(LootableDefinition type, int amount)
        {
            var id = type.guid;
            if (!_data.ContainsKey(id))
                _data[id] = 0;
            _data[id] += amount;
            BalanceUpdated();
        }
    }
}