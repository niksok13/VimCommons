using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;
using Core.Runtime.MVVM.ViewModels.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commons.Ads.Runtime.Rewarded
{
    public class VMRewardedClick :
        ASignalEmitter<SignalClick, CanvasRenderer>,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler
    {
        private static IRewarded Rewarded => Locator.Resolve<IRewarded>();
        public void OnPointerDown(PointerEventData eventData) => Component.SetColor(Color.gray);
        public void OnPointerUp(PointerEventData eventData) => Component.SetColor(Color.white);
        public void OnPointerClick(PointerEventData eventData) => Rewarded.Show(OnReward);

        private void OnReward() => Emit(new SignalClick());
    }
}