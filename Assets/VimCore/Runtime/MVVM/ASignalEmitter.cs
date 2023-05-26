using System;
using UnityEngine;

namespace VimCore.Runtime.MVVM
{
    
    public abstract class ASignalEmitter<TPayload> : MonoBehaviour where TPayload: ISignal
    {
        public string method;
        private ModelBehaviour _model;
        private ModelBehaviour Model => _model ??= GetComponentInParent<ModelBehaviour>(true);

        private Action<TPayload> _listener;
        private Action<TPayload> Listener => _listener ??= Model.GetAction<TPayload>(method);

        public void Emit(TPayload payload) => Listener?.Invoke(payload);

    }
}