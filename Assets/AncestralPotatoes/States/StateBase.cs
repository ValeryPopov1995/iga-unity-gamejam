namespace AncestralPotatoes.States
{
    public abstract class StateBase : IState
    {
        protected IStateMachine StateMachine { get; }

        public StateBase(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }


        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}