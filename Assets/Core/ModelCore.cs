using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;
using VimRoulette.Runtime.Roulette;

namespace Tests.Core
{
    public class ModelCore : ModelBehaviour
    {
        private static IRoulette Roulette => Locator.Resolve<IRoulette>();

        public void BtnRoulette(SignalClick _) => Roulette.Show();
    }
}
