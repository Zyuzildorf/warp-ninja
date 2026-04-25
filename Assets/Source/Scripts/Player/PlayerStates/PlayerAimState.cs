using UnityEngine;

namespace Source.Scripts.Player.PlayerStates
{
    public class PlayerAimState : PlayerState
    {
        private PlayerThrowState _playerThrowState;
        private PlayerRotater _rotater;
        private PlayerPointer _pointer;
        
        private Vector3 _startPosition;

        public Vector3 Direction { get; private set; }
        
        public PlayerAimState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            Player.InputReader.OnMouseMoved += Rotate;
            Player.InputReader.OnMouseOver += ChangeState;
            
            _pointer.GetObject();
            
            base.Enter();
        }

        public override void Exit()
        {
            Player.InputReader.OnMouseMoved -= Rotate;
            Player.InputReader.OnMouseOver -= ChangeState;
            
            _pointer.ReleaseObject();
            
            base.Exit();
        }

        public void Initialize(PlayerThrowState throwState, PlayerPointer pointer, PlayerRotater rotater)
        {
            _playerThrowState = throwState;
            _pointer = pointer;
            _rotater = rotater;
        }
        
        public void SetStartPosition(Vector3 startPosition)
        {
            _startPosition = startPosition;
        }

        private void Rotate(Vector3 newPosition)
        {
            Direction = (_startPosition - newPosition).normalized;
            
            _rotater.Rotate(Direction);
            _pointer.ChangeRotation(Direction);
        }

        private void ChangeState()
        {
            StateMachine.SetState(_playerThrowState);
        }
    }
}