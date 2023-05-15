using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;
using VimPurchase.Runtime.ServiceIap;

namespace VimPurchase.Runtime.UIStore
{
    public class ModelUIStore : ModelBehaviour, IUIStore
    {
        private static readonly ServiceContainer<IUIStore> Container = Locator.Single<IUIStore>();
        
        private static IIap Iap => Locator.Resolve<IIap>();
        private ObservableData<bool> Visible { get; } = new();
        
        private void Awake()
        {
            Container.Attach(this);
        }

        public void Show() => Visible.Value = true;
        public void Hide() => Visible.Value = false;

        private void BtnHide(SignalClick _) => Hide();

    }
}