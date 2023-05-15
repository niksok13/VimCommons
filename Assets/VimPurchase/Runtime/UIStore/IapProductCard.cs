using UnityEngine;
using UnityEngine.Purchasing;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;
using VimPurchase.Runtime.ServiceIap;

namespace VimPurchase.Runtime.UIStore
{
    public class IapProductCard: ModelBehaviour
    {
        private static IIap Iap => Locator.Resolve<IIap>();

        private ObservableData<string> Name { get; } = new();
        private ObservableData<string> Description { get; } = new();
        private ObservableData<string> FakePrice { get; } = new();
        private ObservableData<string> Price { get; } = new();


        [SerializeField] private string productId;
        
        private void Start() => Iap.Products.OnValue += OnProductLoaded;

        private void OnProductLoaded(ProductCollection productCollection)
        {
            if (productCollection==null) return;
            var product = productCollection.WithID(productId);
            if (product==null) return;
            Name.Value = product.metadata.localizedTitle;
            Description.Value = product.metadata.localizedDescription;
            var currencyCode = product.metadata.isoCurrencyCode;
            var cost = product.metadata.localizedPrice;
            Price.Value = $"{cost} {currencyCode}";
            FakePrice.Value = $"{cost * 10 / 4} {currencyCode}";
        }

        private void BtnPurchase(SignalClick _) => Iap.Purchase(productId);
    }
}