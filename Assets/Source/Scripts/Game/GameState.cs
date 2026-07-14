using Source.Scripts.Other;
using Source.Scripts.Utillities;

namespace Source.Scripts.Game
{
    public class GameState : State
    {
        public GameLogic GameLogic { get; private set; }
        public GameStateMachine StateMachine { get; private set; }

        public GameState(GameLogic gameLogic, GameStateMachine stateMachine)
        {
            CheckerForNull.ThrowIfNullArgument(gameLogic);
            CheckerForNull.ThrowIfNullArgument(stateMachine);
            
            GameLogic = gameLogic;
            StateMachine = stateMachine;
        }
    }
}