using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Runtime.MVVM.ViewModels.Input
{
    public class VMImageClick: 
        ASignalEmitter<SignalClick, CanvasRenderer>, 
        IPointerDownHandler, 
        IPointerUpHandler, 
        IPointerClickHandler
    {
        public void OnPointerDown(PointerEventData eventData) => Component.SetColor(Color.gray);
        public void OnPointerUp(PointerEventData eventData) => Component.SetColor(Color.white);
        public void OnPointerClick(PointerEventData eventData) => Emit(new SignalClick());
    }
}