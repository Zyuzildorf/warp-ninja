using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class PositionHolder : MoveStrategy
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private float _safetyMargin = 0.5f;
        
        private EnemyMover _mover;
        private EnemyRotater _rotater;
        
        private Transform _transform;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            _mover = new EnemyMover(_transform, _rigidbody);
            _rotater = new EnemyRotater(_transform);
            
            _mover.SetTarget(_startPosition);
            }

        public override void HandleMovement()
        {
            if (IsOnPostion()) return;

            _mover.Move();
            _rotater.Rotate(_startPosition.position);
        }

        public override void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
        }

        private bool IsOnPostion()
        {
            if (transform.position.x - _startPosition.position.x <= _safetyMargin)
            {
                return true;
            }
            
            return false;
        }
    }
}