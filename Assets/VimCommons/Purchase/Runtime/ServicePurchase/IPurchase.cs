using UnityEngine.Purchasing;
using VimCommons.Purchase.Runtime.IapProducts;
using VimCore.Runtime.MVVM;

namespace VimCommons.Purchase.Runtime.ServicePurchase
{
    public interface IPurchase
    {
        void Purchase(ProductInfo productInfo);
        ObservableData<ProductCollection> ProductCollection { get; }
    }
}