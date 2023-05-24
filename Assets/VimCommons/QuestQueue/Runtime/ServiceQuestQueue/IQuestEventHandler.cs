namespace VimCommons.QuestQueue.Runtime.ServiceQuestQueue
{
    public interface IQuestEventHandler<T>
    {
        void PushEvent(T payload);
    }
}