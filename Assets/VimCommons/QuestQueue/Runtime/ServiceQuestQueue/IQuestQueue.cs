using VimCore.Runtime.MVVM;

namespace VimCommons.QuestQueue.Runtime.ServiceQuestQueue
{
    public interface IQuestQueue
    {
        ObservableData<int> QuestProgress { get; }
        void CompleteQuest(bool auto = false);
        void LookTarget();
        void PushEvent<T>(T payload);
    }
}