using Source.Scripts.Utillities;

namespace Source.Scripts.Player.PlayerStates
{
    public class PlayerTeleportState : PlayerState
    {
        private PlayerAimState _aimState;
        private PlayerTeleporter _teleporter;
        
        public PlayerTeleportState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Teleport();
        }

        public void Initialize(PlayerAimState aimState, PlayerTeleporter teleporter)
        {
            CheckerForNull.ThrowIfNullArgument(aimState);
            CheckerForNull.ThrowIfNullArgument(teleporter);
            
            _aimState = aimState;
            _teleporter = teleporter;
        }
        
        private void Teleport()
        {
            _teleporter.Teleport();
            
            StateMachine.SetState(_aimState);
        }
    }
}