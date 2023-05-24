using Commons.Purchase.Runtime.IapProducts;
using Core.Runtime.MVVM;
using UnityEngine.Purchasing;

namespace Commons.Purchase.Runtime.ServicePurchase
{
    public interface IPurchase
    {
        void Purchase(ProductInfo productInfo);
        ObservableData<ProductCollection> ProductCollection { get; }
    }
}