namespace Core.Runtime.StateMachine
{
    public class Fsm<TContext, TState> where TState: AState<TContext, TState>
    {
        internal readonly TContext Context;
        private TState _state;

        public Fsm(TContext ctx, TState initState)
        {
            Context = ctx;
            EnterState(initState);
        }

        private void EnterState(TState newState)
        {
            _state = newState;
            _state.Init(this);
            _state?.Enter();
        }

        public TState State
        {
            get => _state;
            set
            {
                _state?.Exit();
                EnterState(value);
            }
        }
    }
}