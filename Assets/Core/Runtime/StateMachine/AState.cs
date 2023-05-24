namespace Core.Runtime.StateMachine
{
    public class AState<TContext, TState> where TState: AState<TContext, TState>
    {
        private Fsm<TContext, TState> _fsm;
        protected TContext Context => _fsm.Context;
        internal void Init(Fsm<TContext, TState> fsm) => _fsm = fsm;

        protected void ChangeState(TState nextState) => _fsm.State = nextState;
        protected void ChangeState<TNewState>() where TNewState : TState, new() => ChangeState(new TNewState());

        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}