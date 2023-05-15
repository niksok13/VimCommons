namespace VimAnalytics.Runtime.ServiceAnalytics
{
    public interface IAnalytics
    {
        void QuestCompleted(int step, string title);
    }
}