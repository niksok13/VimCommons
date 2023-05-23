using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;
using VimRoulette.Runtime.Roulette;

namespace Test
{
    public class ModelTest : ModelBehaviour
    {
        private static IRoulette Roulette => Locator.Resolve<IRoulette>();

        public void BtnRoulette(SignalClick _) => Roulette.Show();
    }
}
