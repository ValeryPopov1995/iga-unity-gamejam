namespace AncestralPotatoes.States
{
    public class StateMachine : IStateMachine
    {
        public IState State { get; private set; }

        public StateMachine()
        {
        }

        public void GoTo(IState state)
        {
            State?.Exit();
            State = state;
            State.Enter();
        }

        public void Initialize(IState state)
        {
            GoTo(state);
        }

        public void Update()
        {
            State.Update();
        }
    }
}