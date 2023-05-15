using System;
using VimCore.Runtime.MVVM;

namespace VimAds.ServiceAds.Rewarded
{
    public interface IRewarded
    {
        ObservableData<bool> Ready { get; }
        void Show(Action callback);
    }
}