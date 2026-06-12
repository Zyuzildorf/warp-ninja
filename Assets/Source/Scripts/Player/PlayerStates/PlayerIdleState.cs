using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Player.PlayerStates
{
    public class PlayerIdleState : PlayerState
    {
        private PlayerAimState _aimState;

        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            Player.InputReader.OnTeleportKeyPress += Aim;
            base.Enter();
        }

        public override void Exit()
        {
            Player.InputReader.OnTeleportKeyPress -= Aim;
            base.Exit();
        }

        public void Initialize(PlayerAimState aimState)
        {
            CheckerForNull.ThrowIfNullArgument(aimState);
            
            _aimState = aimState;
        }
        
        private void Aim(Vector3 newPosition)
        {
            _aimState.SetStartPosition(newPosition);
            StateMachine.SetState(_aimState);
        }
    }
}