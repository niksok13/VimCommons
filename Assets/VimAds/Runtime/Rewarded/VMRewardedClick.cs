using UnityEngine;
using UnityEngine.EventSystems;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;

namespace VimAds.Runtime.Rewarded
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