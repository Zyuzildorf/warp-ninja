using Source.Scripts.Other;
using Source.Scripts.Utillities;

namespace Source.Scripts.Game
{
    public class GameState : State
    {
        public Main Main { get; private set; }
        public GameStateMachine StateMachine { get; private set; }

        public GameState(Main main, GameStateMachine stateMachine)
        {
            CheckerForNull.ThrowIfNullArgument(main);
            CheckerForNull.ThrowIfNullArgument(stateMachine);
            
            Main = main;
            StateMachine = stateMachine;
        }
    }
}