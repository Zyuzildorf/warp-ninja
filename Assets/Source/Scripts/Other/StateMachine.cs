using Source.Scripts.Interfaces;

namespace Source.Scripts.Other
{
    public abstract class StateMachine<T> where T:State 
    {
        public T CurrentState { get; private set; }
        
        public void UpdateCurrentState()
        {
            if (CurrentState is IUpdatable updatable)
            {
                updatable.UpdateState();
            }
        }

        public void SetState(T state)
        {
            if (CurrentState == state)
            {
                return;
            }

            if (CurrentState is IExitable exitable)
            {
                exitable.Exit();
            }
            
            CurrentState = state;

            if (CurrentState is IEnterable enterable)
            {
                enterable.Enter();
            }
        }
    }
}