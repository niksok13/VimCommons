using UnityEngine;
using UnityEngine.EventSystems;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.SignalEmitters.CanvasImage;

namespace VimCommons.Ads.Runtime.Rewarded
{
    public class VMRewardedClick : ASignalEmitter<SignalClick>,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler
    {
        private CanvasRenderer _renderer;
        public CanvasRenderer Renderer => _renderer ??= GetComponent<CanvasRenderer>(); 
        private static IRewarded Rewarded => Locator.Resolve<IRewarded>();
        public void OnPointerDown(PointerEventData eventData) => Renderer.SetColor(Color.gray);
        public void OnPointerUp(PointerEventData eventData) => Renderer.SetColor(Color.white);
        public void OnPointerClick(PointerEventData eventData) => Rewarded.Show(OnReward);

        private void OnReward() => Emit(new SignalClick());
    }
}