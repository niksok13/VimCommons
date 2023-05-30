namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public class SGameLevelStart: SGameAbstract
    {
        public override void Enter() => ChangeState<SGameLevelPlay>();
    }
}