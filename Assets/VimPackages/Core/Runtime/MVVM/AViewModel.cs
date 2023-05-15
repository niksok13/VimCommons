using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM
{
    public abstract class AViewModel<TData,TComponent> : MonoBehaviour
    {
        [SerializeField]
        private string property;

        private ModelBehaviour _model;
        private ModelBehaviour Model => _model ??= GetComponentInParent<ModelBehaviour>(true);

        private ObservableData<TData> _data;
        private ObservableData<TData> Data => _data ??= Model ? Model.GetObservableData<TData>(property) : null;

        private TComponent _component;
        protected TComponent Component => _component ??= GetComponent<TComponent>();
        
        private void Start()
        {
            if (Data == null) return;
            Data.OnValue += OnValue;
        }

        protected abstract void OnValue(TData value);

        private void OnDestroy()
        { 
            if (Data == null) return;
            Data.OnValue -= OnValue;
        }
    }
    
    public abstract class AViewModel<TComponent> : MonoBehaviour
    {
        [SerializeField]
        private string property;
        
        private ModelBehaviour _model;
        private ModelBehaviour Model => _model ??= GetComponentInParent<ModelBehaviour>(true);

        private ObservableChannel _channel;
        private ObservableChannel Channel => _channel ??= Model ? Model.GetObservableChannel(property) : null;

        private TComponent _component;
        protected TComponent Component => _component ??= GetComponent<TComponent>();

        private void Awake()
        {
            if (Channel == null) return;
            Channel.OnSignal += OnSignal;
        }

        public abstract void OnSignal();

        private void OnDestroy()
        { 
            if (Channel == null) return;
            Channel.OnSignal -= OnSignal;
        }
    }
}