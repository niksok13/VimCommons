using UnityEngine.Purchasing;
using VimCore.Runtime.MVVM;

namespace VimPurchase.Runtime.ServiceIap
{
    public interface IIap
    {
        void Purchase(string id);
        ObservableData<ProductCollection> Products { get; }
    }
}