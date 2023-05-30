using UnityEngine;
using VimCommons.Roulette.Runtime.Reward;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.SignalEmitters.CanvasImage;
using VimCore.Runtime.Utils;

namespace VimCommons.Roulette.Runtime.Roulette
{
    public class ModelRoulette : ModelBehaviour, IRoulette
    {
        public float spinSpeed = 512;
        public float spinDuration = 10;
        public RouletteReward[] rewards;
        
        private static readonly ServiceContainer<IRoulette> Container = Locator.Single<IRoulette>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private ObservableData<RouletteReward[]> Rewards { get; } = new();

        private ObservableData<bool> Visible { get; } = new();
        private ObservableData<bool> IsRun { get; } = new();
        private ObservableData<string> Message { get; } = new();
        private ObservableData<float> RouletteAngle { get; } = new();

        private void Start() => Rewards.Value = rewards;

        public void Show()
        {
            Message.Value = "Roll the roulette";
            Visible.Value = true;
        }

        public void Hide() => Visible.Value = false;

        public void BtnClose(SignalClick _)
        {
            if (IsRun.Value) return;
            Hide();
        }

        public void BtnRoll(SignalClick _)
        {
            if (IsRun.Value) return;
            IsRun.Value = true;
            var curSpeed = spinSpeed * UnityEngine.Random.Range(0.8f, 1.2f);
            EZ.Spawn().Tween(spinDuration, ez =>
            {
                var deceleration = 1 - ez.QuadOut;
                var spinStep = deceleration * Time.deltaTime;
                RouletteAngle.Value += spinStep * curSpeed;
            }).Call(FinishSpin);
        }

        private void FinishSpin(EZData obj)
        {
            IsRun.Value = false;
            var reward = CalculateReward();
            Message.Value = "Your reward: \n" + reward.title;
            reward.Apply();
        }

        private RouletteReward CalculateReward()
        {
            var angle = RouletteAngle.Value % 360;
            var sectorSize = 360 / rewards.Length;
            var index = Mathf.FloorToInt(angle / sectorSize);
            return rewards[index];
        }
    }
}
