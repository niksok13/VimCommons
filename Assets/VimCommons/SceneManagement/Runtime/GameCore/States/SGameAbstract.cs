using VimCore.Runtime.StateMachine;

namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public abstract class SGameAbstract: AState<ServiceGameCore,SGameAbstract>
    {
        public virtual void FinishLevel()
        {
        }
    }
}