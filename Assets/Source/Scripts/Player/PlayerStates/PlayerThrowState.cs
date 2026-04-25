using UnityEngine;

namespace Source.Scripts.Player.PlayerStates
{
    public class PlayerThrowState : PlayerState
    {
        private PlayerAimState _aimState;
        private PlayerTeleportState _teleportState;
        
        private PlayerShooter _shooter;
        private PlayerEnergy _energy;

        public PlayerThrowState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            Player.InputReader.OnTeleportKeyPress += TryTeleport;

            _shooter.Attack(_aimState.Direction);

            base.Enter();
        }

        public override void Exit()
        {
            Player.InputReader.OnTeleportKeyPress -= TryTeleport;

            base.Exit();
        }

        public void Initialize(PlayerAimState aimState, PlayerTeleportState teleportState, 
            PlayerEnergy energy, PlayerShooter shooter)
        {
            _aimState = aimState;
            _teleportState = teleportState;
            
            _shooter = shooter;
            _energy = energy;
        }
        
        private void TryTeleport(Vector3 startPosition)
        {
            if (_energy.TryUseEnergy())
            {
                _aimState.SetStartPosition(startPosition);
                StateMachine.SetState(_teleportState);
            }
        }
    }
}