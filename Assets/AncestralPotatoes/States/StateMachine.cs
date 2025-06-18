using UnityEngine;

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
            Debug.Log($"{GetHashCode()} Exited {State?.GetType().Name}");
            State = state;
            State.Enter();
            Debug.Log($"{GetHashCode()} Entered {State.GetType().Name}");
        }

        public void Initialize(IState state)
        {
            GoTo(state);
        }

        public void Update()
        {
            State.Update();
        }

        public IState GetCurrentState()
        {
            return State;
        }
    }
}