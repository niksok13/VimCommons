using Core.Runtime.MVVM;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.UIQuestQueue
{
    public interface IUIQuestQueue
    {
        ObservableData<bool> ProgressVisible { get; }
        ObservableData<string> ProgressLabel { get; }
        ObservableData<float> Progress { get; }
        ObservableData<string> Title { get; }
        public ObservableData<Sprite> Icon { get; }
        ObservableData<bool> Visible { get; }
        ObservableData<bool> ClaimVisible { get; }
        public ObservableChannel Bounce { get; }

    }
}