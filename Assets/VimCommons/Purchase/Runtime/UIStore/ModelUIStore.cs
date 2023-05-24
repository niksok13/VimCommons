using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;
using Core.Runtime.MVVM.ViewModels.Input;

namespace Commons.Purchase.Runtime.UIStore
{
    public class ModelUIStore : ModelBehaviour, IUIStore
    {
        private static readonly ServiceContainer<IUIStore> Container = Locator.Single<IUIStore>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);
        
        private ObservableData<bool> Visible { get; } = new();
        
        public void Show() => Visible.Value = true;
        public void Hide() => Visible.Value = false;

        private void BtnHide(SignalClick _) => Hide();

    }
}