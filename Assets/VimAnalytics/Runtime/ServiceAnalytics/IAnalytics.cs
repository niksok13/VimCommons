namespace VimAnalytics.Runtime.ServiceAnalytics
{
    public interface IAnalytics
    {
        void Send<TEvent>(TEvent payload);
    }
}