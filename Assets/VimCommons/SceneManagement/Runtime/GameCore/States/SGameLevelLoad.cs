using VimCommons.SceneManagement.Runtime.LevelLoader;
using VimCommons.SceneManagement.Runtime.UISplashscreen;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public class SGameLevelLoad : SGameAbstract
    {
        private static ISplashscreen Splashscreen => Locator.Resolve<ISplashscreen>();
        private static ILevelLoader LevelLoader => Locator.Resolve<ILevelLoader>();

        public override async void Enter()
        {
            await Splashscreen.Show("PLEASE WAIT");
            await LevelLoader.LoadLevel(Context.Level.Value);
            Splashscreen?.Hide();
            ChangeState<SGameLevelStart>();
        }
    }
}