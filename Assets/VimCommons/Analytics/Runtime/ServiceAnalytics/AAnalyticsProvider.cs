using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Analytics.Runtime.ServiceAnalytics
{
    public abstract class AAnalyticsProvider : MonoBehaviour
    {
        private static readonly Filter<AAnalyticsProvider> Filter = Locator.Filter<AAnalyticsProvider>();
        private void OnEnable() => Filter.Add(this);
        private void OnDestroy() => Filter.Remove(this);
        public abstract void Send<T>(T payload);
    }
}