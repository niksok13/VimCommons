using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimAds.ServiceAds.Rewarded
{
    public class VMRewardedReady : MonoBehaviour
    {
        private static IRewarded Rewarded => Locator.Resolve<IRewarded>();
        private void Awake() => Rewarded.Ready.OnValue += SetValue;
        private void SetValue(bool state) => gameObject.SetActive(state);
        private void OnDestroy()
        {
            if (Rewarded == null) return;
            Rewarded.Ready.OnValue -= SetValue;
        }
    }
}