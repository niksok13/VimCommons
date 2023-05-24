using System.Collections.Generic;
using System.Linq;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using VimCommons.Purchase.Runtime.IapProducts;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;

namespace VimCommons.Purchase.Runtime.ServicePurchase
{
    public class ServicePurchase: MonoBehaviour, IPurchase, IStoreListener
    {
        public List<ProductInfo> products;

        private static readonly ServiceContainer<IPurchase> Container = Locator.Single<IPurchase>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);

        private IStoreController _controller;
        public ObservableData<ProductCollection> ProductCollection { get; } = new();


        public async void Start()
        {
            var options = new InitializationOptions().SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (var product in products)
                builder.AddProduct(product.id, product.type);
            UnityPurchasing.Initialize(this, builder);
        }
        public void Purchase(ProductInfo productInfo) => _controller?.InitiatePurchase(productInfo.id);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            ProductCollection.Value = _controller.products;
        }
        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var purchasedId = args.purchasedProduct.definition.id;
            var purchasedProductInfo = products.First(i => i.id == purchasedId);
            purchasedProductInfo.Process(args);
            ProductCollection.Value = _controller.products;
            ProductCollection.Touch();
            return PurchaseProcessingResult.Complete;
        }

        public void OnInitializeFailed(InitializationFailureReason error) => Debug.LogError($"Failed to initialize IAP, Reason: {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message) => Debug.LogError($"Failed to initialize IAP, Reason: {error}, Message: {message}");
        
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => Debug.LogError($"Failed to buy {product.definition}, Reason: {failureReason}");
    }
}