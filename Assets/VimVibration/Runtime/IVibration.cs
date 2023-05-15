using VimCore.Runtime.MVVM;

namespace VimVibration.Runtime
{
    public interface IVibration
    {
        ObservableData<bool> IsActive { get; }
        void Light();
        void Medium();
        void Heavy();
        
        void Selection();
        void Success();
        void Warning();
        void Failure();
    }
}