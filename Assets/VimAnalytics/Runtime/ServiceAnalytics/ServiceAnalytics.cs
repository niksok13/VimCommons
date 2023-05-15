using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimAnalytics.Runtime.ServiceAnalytics
{
    public class ServiceAnalytics: MonoBehaviour, IAnalytics
    {
        private static readonly Filter<AAnalyticsProvider> Providers = Locator.Filter<AAnalyticsProvider>();

        private static readonly ServiceContainer<IAnalytics> Container = Locator.Single<IAnalytics>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);


        public void QuestCompleted(int step, string title)
        {
            foreach (var provider in Providers) 
                provider.QuestCompleted(step, title);
        }
    }
}