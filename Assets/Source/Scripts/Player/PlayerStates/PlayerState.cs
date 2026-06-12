using Source.Scripts.Other;
using Source.Scripts.Utillities;

namespace Source.Scripts.Player.PlayerStates
{
    public abstract class PlayerState : State
    {
        protected Player Player { get; private set; }
        protected PlayerStateMachine StateMachine {get; private set;}

        public PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            CheckerForNull.ThrowIfNullArgument(player);
            CheckerForNull.ThrowIfNullArgument(stateMachine);
            
            Player = player;
            StateMachine = stateMachine;
        }
    }
}