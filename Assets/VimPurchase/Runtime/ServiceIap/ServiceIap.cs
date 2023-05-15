using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimPurchase.Runtime.ServiceIap
{
    public class ServiceIap: MonoBehaviour, IIap, IStoreListener
    {
        
        private static readonly ServiceContainer<IIap> Container = Locator.Single<IIap>();

        private IStoreController _controller;
        public ObservableData<ProductCollection> Products { get; } = new();

        private void Awake() => Container.Attach(this);

        public async void Init(Dictionary<string, ProductType> products)
        {
            var options = new InitializationOptions().SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (var (productId, productType) in products)
                builder.AddProduct(productId, productType);
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            Products.Value = _controller.products;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"Failed to initialize IAP, Reason: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"Failed to initialize IAP, Reason: {error}, Message: {message}");
        }

        public void Purchase(string productID) => _controller?.InitiatePurchase(productID);

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;
            
            Products.Value = _controller.products;
            Products.Touch();
            return PurchaseProcessingResult.Complete;
        }
        
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => 
            Debug.LogError($"Failed to buy {product.definition}, Reason: {failureReason}");
    }
}