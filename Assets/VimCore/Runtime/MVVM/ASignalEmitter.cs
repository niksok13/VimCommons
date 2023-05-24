using System;
using UnityEngine;

namespace Core.Runtime.MVVM
{
    
    public abstract class ASignalEmitter<TPayload, TComponent> : MonoBehaviour where TPayload: ISignal
    {
        public string method;
        private ModelBehaviour _model;
        private ModelBehaviour Model => _model ??= GetComponentInParent<ModelBehaviour>(true);

        private Action<TPayload> _listener;
        private Action<TPayload> Listener => _listener ??= Model.GetAction<TPayload>(method);

        private TComponent _component;
        protected TComponent Component => _component ??= GetComponent<TComponent>();

        public void Emit(TPayload payload) => Listener?.Invoke(payload);

    }
}