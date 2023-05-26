using UnityEngine.Purchasing;
using VimCommons.Purchase.Runtime.ServicePurchase;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.SignalEmitters.CanvasImage;

namespace VimCommons.Purchase.Runtime.IapProducts
{
    public class ModelProductCard: ModelBehaviour
    {
        public ProductInfo productInfo;
        
        private static IPurchase Purchase => Locator.Resolve<IPurchase>();

        private ObservableData<string> Name { get; } = new();
        private ObservableData<string> Description { get; } = new();
        private ObservableData<string> FakePrice { get; } = new();
        private ObservableData<string> Price { get; } = new();

        private void Start() => Purchase.ProductCollection.OnValue += OnProductLoaded;
        private void OnDestroy() => Purchase.ProductCollection.OnValue -= OnProductLoaded;

        private void OnProductLoaded(ProductCollection productCollection)
        {
            if (productCollection == null) return;
            var product = productCollection.WithID(productInfo.id);
            if (product == null) return;
            Name.Value = product.metadata.localizedTitle;
            Description.Value = product.metadata.localizedDescription;
            var currencyCode = product.metadata.isoCurrencyCode;
            var cost = product.metadata.localizedPrice;
            Price.Value = $"{cost} {currencyCode}";
            FakePrice.Value = $"{cost * 10 / 4} {currencyCode}";
        }

        private void BtnPurchase(SignalClick _) => Purchase.Purchase(productInfo);
    }
}