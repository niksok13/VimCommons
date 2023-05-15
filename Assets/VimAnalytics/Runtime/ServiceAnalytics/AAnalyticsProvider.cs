using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimAnalytics.Runtime.ServiceAnalytics
{
    public abstract class AAnalyticsProvider : MonoBehaviour
    {
        private static readonly Filter<AAnalyticsProvider> Filter = Locator.Filter<AAnalyticsProvider>();
        private void OnEnable() => Filter.Add(this);
        private void OnDestroy() => Filter.Remove(this);
        
        public abstract void QuestCompleted(int step, string title);
    }
}