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
            
            _mover = new EnemyMover(_transform, _rigidbody, MoveSpeed);
            _rotater = new EnemyRotater(_transform, RotationSpeed);
            
            _mover.SetTarget(_startPosition);
            }

        public override void HandleMovement()
        {
            if (IsOnPosition()) return;

            _mover.Move();
            _rotater.Rotate(_startPosition.position);
        }

        public override void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
        }

        private bool IsOnPosition()
        {
            float distance = Vector3.Distance(_startPosition.position, _transform.position);
            
            if (distance <= _safetyMargin)
            {
                return true;
            }
            
            return false;
        }
    }
}