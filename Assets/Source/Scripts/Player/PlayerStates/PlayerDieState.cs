using UnityEngine;

namespace Source.Scripts.Player.PlayerStates
{
    public class PlayerDieState : PlayerState
    {
        public PlayerDieState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
           // Debug.Log("You die");
        }
    }
}