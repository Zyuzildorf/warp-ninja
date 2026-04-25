using Source.Scripts.Other;

namespace Source.Scripts.Player.PlayerStates
{
    public abstract class PlayerState : State
    {
        public Player Player { get; private set; }
        public PlayerStateMachine StateMachine {get; private set;}

        public PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            Player = player;
            StateMachine = stateMachine;
        }
    }
}