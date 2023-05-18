using UnityEngine.Purchasing;
using VimCore.Runtime.MVVM;
using VimPurchase.Runtime.IapProducts;

namespace VimPurchase.Runtime.ServicePurchase
{
    public interface IPurchase
    {
        void Purchase(ProductInfo productInfo);
        ObservableData<ProductCollection> ProductCollection { get; }
    }
}