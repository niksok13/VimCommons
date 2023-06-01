using VimCommons.SceneManagement.Runtime.GameCore;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Debugger.Runtime
{
    public class CheatLevel: ACheat
    {
        private static IGameCore GameCore => Locator.Resolve<IGameCore>();

        protected override string Command => "level";
        protected override string Description => Command;

        protected override void OnApply()
        {
            GameCore.FinishLevel();
        }
    }
}
