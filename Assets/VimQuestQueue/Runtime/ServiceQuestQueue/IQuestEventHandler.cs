namespace VimQuestQueue.Runtime.ServiceQuestQueue
{
    public interface IQuestEventHandler<T>
    {
        void PushEvent(T payload);
    }
}