using System;
using System.Collections.Generic;

namespace VimPackages.Core.Runtime.MVVM
{
    public class ObservableData<T>
    {
        private T _value;
        private Action<T> _onValue;

        public ObservableData(T value = default) => _value = value;

        public event Action<T> OnValue
        {
            add
            {
                _onValue -= value;
                _onValue += value;
                value?.Invoke(_value);
            }
            remove => _onValue -= value;
        }

        public T Value
        {
            get => _value;
            set 
            {
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;
                _value = value;
                if (Equals(_onValue, null)) return;
                _onValue.Invoke(_value);
            }
        }

        public void Touch() => _onValue?.Invoke(_value);        
        public void Invoke(T value) => _onValue?.Invoke(value);

        public bool NotNull => !Equals(_value, null);
        public bool IsNull => Equals(_value, null);
    }
}