using System;
using VimCore.Runtime.MVVM;

namespace VimAds.Runtime.Rewarded
{
    public interface IRewarded
    {
        ObservableData<bool> Ready { get; }
        void Show(Action callback);
    }
}