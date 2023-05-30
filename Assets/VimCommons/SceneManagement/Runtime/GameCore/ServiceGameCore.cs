using UnityEngine;
using VimCommons.SceneManagement.Runtime.GameCore.States;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.StateMachine;

namespace VimCommons.SceneManagement.Runtime.GameCore
{
    public class ServiceGameCore: MonoBehaviour, IGameCore
    {
        private static readonly ServiceContainer<IGameCore> Container = Locator.Single<IGameCore>();

        private Fsm<ServiceGameCore, SGameAbstract> _fsm;

        public ObservableData<int> Level { get; } = new();

        private void Awake()
        {
            Container.Attach(this);
            
            Level.ConnectPref("Level");
            _fsm = new Fsm<ServiceGameCore,SGameAbstract>(this, new SGameInit());
        }

        public void FinishLevel()
        {
            _fsm.State.FinishLevel();
        }
    }
}