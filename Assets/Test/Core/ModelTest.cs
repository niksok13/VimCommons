using Commons.Roulette.Runtime.Roulette;
using Core.Runtime.DependencyManagement;
using Core.Runtime.MVVM;
using Core.Runtime.MVVM.ViewModels.Input;

namespace Test.Core
{
    public class ModelTest : ModelBehaviour
    {
        private static IRoulette Roulette => Locator.Resolve<IRoulette>();

        public void BtnRoulette(SignalClick _) => Roulette.Show();
    }
}
