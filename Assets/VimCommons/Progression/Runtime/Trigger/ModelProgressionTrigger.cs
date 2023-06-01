using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using VimCommons.Ads.Runtime.InterstitialRunner;
using VimCommons.Ads.Runtime.Rewarded;
using VimCommons.Looting.Runtime.Core;
using VimCommons.Looting.Runtime.Inventory;
using VimCommons.Progression.Runtime.Node;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Progression.Runtime.Trigger
{
    
    public class ModelProgressionTrigger : ModelBehaviour
    {      
        public ProgressionNode node;
        
        public string label = "BUILD";
        public Sprite icon;

        public float duration = 3;
        public float radius = 1;

        private static readonly Filter<ModelProgressionTrigger> Filter = Locator.Filter<ModelProgressionTrigger>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        private static IInterstitialRunner Interstitial => Locator.Resolve<IInterstitialRunner>();
        private static IRewarded Rewarded => Locator.Resolve<IRewarded>();
        private static IInventory Inventory => Locator.Resolve<IInventory>();

        public ObservableData<int> NodeLevel => node.NodeLevel;
        public ObservableData<float> Accumulation { get; } = new();
        public ObservableData<bool> UpgradeVisible { get; } = new();
        public ObservableData<bool> CanPay { get; } = new();
        public ObservableData<bool> CanUpgrade { get; } = new();
        public ObservableData<string> UpgradeLabel { get; } = new();
        public ObservableData<string> LevelLabel { get; } = new();
        public ObservableData<string> RewardedBonus { get; } = new();
        public ObservableData<string> UpgradeType { get; } = new();
        public ObservableData<Sprite> UpgradeImage { get; } = new();

        private NodeLevelInfo NextUpgrade => node.NextUpgrade;

        private void Start()
        {
            NodeLevel.OnValue += OnProgress;
        }

        private void OnProgress(int obj)
        {
            var unlocked = node.parent == null || node.parent.NodeLevel.Value >= node.parentLevel;
            CanUpgrade.Value = unlocked && obj < node.upgrades.Count;
            LevelLabel.Value = obj < 1 ? label : $"LEVEL {obj}";
            UpgradeImage.Value = icon;
            UpgradeType.Value = label;
            UpgradeLabel.Value = NextUpgrade?.BuildUpgradeLabel();
            RewardedBonus.Value = NextUpgrade?.BuildRewardedBonus(0.25f);
        }


        public void OnEnter(ProgressionTriggerInvoker invoker)
        {
            if (!CanUpgrade.Value) return;
            if (NextUpgrade == null) return;
            if (NextUpgrade.cost.Sum(i => i.amount) < 1) 
                invoker.SetWorkAnimation(true);
            var canPay = Inventory.CanPay(NextUpgrade.cost);
            CanPay.Value = canPay;
            UpgradeVisible.Value = true;
            Accumulation.Value = 0;
        }

        public void OnStay(ProgressionTriggerInvoker invoker)
        {
            if (NextUpgrade == null) return;
            if (!UpgradeVisible.Value) return;
            Accumulation.Value += Time.deltaTime / duration;
            if (Accumulation.Value < 1) return;
            OnExit(invoker);
            if(CanPay.Value)
                Buy(invoker);
            else
                ShowRewarded(invoker);
        }
        
        public void OnExit(ProgressionTriggerInvoker invoker)
        {
            Accumulation.Value = 0;
            UpgradeVisible.Value = false;
            invoker.SetWorkAnimation(false);
        }
        
        private void Buy(ProgressionTriggerInvoker invoker)
        {
            var upgradeCost = NextUpgrade.cost;
            if (!Inventory.TryPay(upgradeCost)) return;
            foreach (var entry in upgradeCost)
                entry.Animate(invoker.Transform, transform);

            node.Upgrade();
            Interstitial?.ResetIdle();
        }
    
        private void ShowRewarded(ProgressionTriggerInvoker invoker) => Rewarded?.Show(()=>OnReward(invoker));

        private async void OnReward(ProgressionTriggerInvoker invoker)
        {
            var upgradeCost = NextUpgrade.cost;
            node.Upgrade();
            foreach (var entry in upgradeCost)
            {
                var bonus = new LootEntry(entry.type, Mathf.CeilToInt(entry.amount * 0.25f));
                bonus.Animate(invoker.Transform, invoker.Transform);
                Inventory.Receive(bonus.type, bonus.amount);
            }
        }
    }
}
