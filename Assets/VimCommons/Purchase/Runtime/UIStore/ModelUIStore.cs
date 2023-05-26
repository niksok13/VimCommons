using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.SignalEmitters.CanvasImage;

namespace VimCommons.Purchase.Runtime.UIStore
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