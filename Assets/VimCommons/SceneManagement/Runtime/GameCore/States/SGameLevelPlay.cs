
namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public class SGameLevelPlay : SGameAbstract
    {
        public override void FinishLevel() => ChangeState<SGameLevelWin>();

    }
}