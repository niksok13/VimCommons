using System;
using VimCore.Runtime.MVVM;

namespace VimCommons.Ads.Runtime.Rewarded
{
    public interface IRewarded
    {
        ObservableData<bool> Ready { get; }
        void Show(Action callback);
    }
}