using UnityEngine;

namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public class SGameLevelWin : SGameAbstract
    {
        public override void Enter()
        {
            Context.Level.Value += 1;
            PlayerPrefs.Save();
            ChangeState<SGameLevelLoad>();
        }
    }
}