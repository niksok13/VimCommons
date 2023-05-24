using System;
using Core.Runtime.MVVM;

namespace Commons.Ads.Runtime.Rewarded
{
    public interface IRewarded
    {
        ObservableData<bool> Ready { get; }
        void Show(Action callback);
    }
}