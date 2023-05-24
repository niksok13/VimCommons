using UnityEngine;
using VimCommons.Roulette.Runtime.Reward;
using VimCore.Runtime.MVVM;

namespace VimCommons.Roulette.Runtime.VisualSector
{
    public class VisualSector : ModelBehaviour
    {
        private RouletteReward _reward;
        private ObservableData<string> Title { get; } = new();
        private ObservableData<Sprite> Icon { get; } = new();

        private ObservableChannel ChannelReward { get; } = new();
        public void Init(RouletteReward reward)
        {
            _reward = reward;
            Title.Value = reward.title;
            Icon.Value = reward.icon;
            reward.OnReward += OnReward;
        }

        private void OnDestroy()
        {
            _reward.OnReward -= OnReward;
        }

        private void OnReward() => ChannelReward.Invoke();
    }
}
