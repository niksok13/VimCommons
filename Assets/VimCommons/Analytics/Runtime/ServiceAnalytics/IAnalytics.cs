namespace Commons.Analytics.Runtime.ServiceAnalytics
{
    public interface IAnalytics
    {
        void Send<TEvent>(TEvent payload);
    }
}