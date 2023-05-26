using UnityEngine;
using UnityEngine.EventSystems;

namespace VimCore.Runtime.MVVM.SignalEmitters.CanvasImage
{
    public class VMImageClick: ASignalEmitter<SignalClick>, 
        IPointerDownHandler, 
        IPointerUpHandler, 
        IPointerClickHandler
    {
        private CanvasRenderer _renderer;
        public CanvasRenderer Renderer => _renderer ??= GetComponent<CanvasRenderer>(); 
        public void OnPointerDown(PointerEventData eventData) => Renderer.SetColor(Color.gray);
        public void OnPointerUp(PointerEventData eventData) => Renderer.SetColor(Color.white);
        public void OnPointerClick(PointerEventData eventData) => Emit(new SignalClick());
    }
}