using System;
using Commons.Camera.Runtime.ServiceCamera;
using Core.Runtime.DependencyManagement;
using Core.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commons.Input.Runtime.InputTouch
{
    public class ServiceInputTouch : MonoBehaviour, ITouchInput,
        IPointerDownHandler, 
        IPointerUpHandler, 
        IPointerMoveHandler,
        IPointerClickHandler
    {
        private static readonly ServiceContainer<ITouchInput> Container = Locator.Single<ITouchInput>();

        private static ICamera Camera => Locator.Resolve<ICamera>();

        public float sensitivity = 5f;
        public RectTransform anchor, stick;

        private bool _pressed;
        private bool _btnPressed;
        private JoystickState _joyValue;
        private Action<JoystickState> _listener;

        private void Awake()
        {
            Container.Attach(this);
            anchor.gameObject.SetActive(false);
            stick.gameObject.SetActive(false);
            LoopUtil.PreUpdate += Tick;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
            LoopUtil.PreUpdate -= Tick;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _joyValue.action = JoystickEvent.Press;
            _joyValue.value = Vector2.zero;
            anchor.gameObject.SetActive(true);
            stick.gameObject.SetActive(true);
            anchor.anchoredPosition = eventData.position;
            stick.anchoredPosition = eventData.position;
            _listener?.Invoke(_joyValue);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_pressed) return;
            _joyValue.action = JoystickEvent.Move;
            _joyValue.value = (eventData.position - eventData.pressPosition) * 0.001f * sensitivity;
            _joyValue.value = Vector2.ClampMagnitude(_joyValue.value,1); 
            var stickpos = Vector3.MoveTowards(eventData.pressPosition, eventData.position, 100);
            stick.anchoredPosition = stickpos;
            _listener?.Invoke(_joyValue);
        }

        public void OnPointerUp(PointerEventData eventData)
        {           
            _pressed = false;
            _joyValue.action = JoystickEvent.Release;
            _joyValue.value = Vector2.zero;
            anchor.gameObject.SetActive(false);
            stick.gameObject.SetActive(false);
            _listener?.Invoke(_joyValue);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var ray = Camera.Camera.ScreenPointToRay(eventData.position);
            var hit = Physics.Raycast(ray, out var info);
            if (!hit) return;
            if (info.collider.TryGetComponent<IClickable>(out var clickable)) 
                clickable.OnClick();
        }

        public void Register(Action<JoystickState> listener)
        {
            if (_listener != null) 
                throw new Exception("A listener already exists");
            _listener = listener;
        }

        public void Release(Action<JoystickState> listener)
        {
            if (_listener != listener) 
                throw new Exception("Another joystick listener is registered");
            _listener = null;
        }

        public void Release() => OnPointerUp(null);

        private void Tick()
        {
            if (Equals(null, _listener)) return;
            var x = UnityEngine.Input.GetAxis("Horizontal");
            var y = UnityEngine.Input.GetAxis("Vertical");
            var joy = new Vector2(x, y);
            
            switch (_btnPressed, joy.sqrMagnitude)
            {
                case (true,0):
                    _btnPressed = false;
                    _joyValue.action = JoystickEvent.Release;
                    _joyValue.value = Vector2.zero;
                    _listener?.Invoke(_joyValue);
                    break;
                
                case (true,>0):
                    _joyValue.value = joy;
                    _joyValue.action = JoystickEvent.Move;
                    _listener?.Invoke(_joyValue);
                    break;
                
                case (false,>0):
                    _btnPressed = true;
                    _joyValue.action = JoystickEvent.Press;
                    _joyValue.value = Vector2.zero;
                    _listener?.Invoke(_joyValue);
                    break;
            }
        }
    }
}