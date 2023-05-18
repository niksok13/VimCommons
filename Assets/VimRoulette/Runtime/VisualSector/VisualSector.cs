using UnityEngine;
using VimCore.Runtime.MVVM;
using VimRoulette.Runtime.Reward;

namespace VimRoulette.Runtime.VisualSector
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
