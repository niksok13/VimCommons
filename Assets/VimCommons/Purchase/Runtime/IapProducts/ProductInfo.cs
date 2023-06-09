using UnityEngine;
using UnityEngine.Purchasing;

namespace VimCommons.Purchase.Runtime.IapProducts
{
    [CreateAssetMenu]
    public class ProductInfo : ScriptableObject
    {
        public string id;
        public ProductType type;

        public virtual void Process(PurchaseEventArgs args) => Debug.Log("Processing purchase");
    }
}
