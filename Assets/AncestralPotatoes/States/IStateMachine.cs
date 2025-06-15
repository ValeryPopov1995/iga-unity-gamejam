namespace AncestralPotatoes.States
{
    public interface IStateMachine
    {
        public void Initialize(IState state);
        public void GoTo(IState state);
        public void Update();
    }
}
