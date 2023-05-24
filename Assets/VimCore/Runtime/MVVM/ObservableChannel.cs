using System;

namespace VimCore.Runtime.MVVM
{
    public class ObservableChannel
    {
        private Action _onSignal;
        
        public event Action OnSignal
        {
            add
            {
                _onSignal -= value;
                _onSignal += value;
            }
            remove => _onSignal -= value;
        }

        public void Invoke() => _onSignal?.Invoke();
    }
}