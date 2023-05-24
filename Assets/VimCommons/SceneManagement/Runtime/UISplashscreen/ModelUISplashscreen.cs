using System.Threading.Tasks;
using Core.Runtime.DependencyManagement;
using Core.Runtime.EZTween;
using Core.Runtime.MVVM;
using Cysharp.Threading.Tasks;

namespace Commons.SceneManagement.Runtime.UISplashscreen
{
    
    public class ModelUISplashscreen: ModelBehaviour, ISplashscreen
    {
        private static readonly ServiceContainer<ISplashscreen> Container = Locator.Single<ISplashscreen>();
        private ObservableData<bool> Visible { get; } = new();
        private ObservableData<string> Message  { get; } = new();
        private ObservableData<float> Progress  { get; } = new();

        public void Awake()
        {
            Container.Attach(this);
        }

        private void OnDestroy()
        {
            Container.Detach(this);
        }

        public async Task Show(string label)
        {
            Message.Value = label;
            if (!Visible.Value)
            {
                Visible.Value = true;
                Progress.Value = 0;
                EZ.Spawn().Tween(5, UpdateProgressBar);
            }
            await UniTask.Delay(200, DelayType.UnscaledDeltaTime);
        }

        private void UpdateProgressBar(EZData ez)
        {
            Progress.Value = ez.QuadOut;
        }

        public void Hide() => Visible.Value = false;
    }
}