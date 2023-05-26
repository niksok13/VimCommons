using VimCommons.Roulette.Runtime.Roulette;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.SignalEmitters.CanvasImage;

namespace Test.Core
{
    public class ModelTest : ModelBehaviour
    {
        private static IRoulette Roulette => Locator.Resolve<IRoulette>();

        public void BtnRoulette(SignalClick _) => Roulette.Show();
    }
}
