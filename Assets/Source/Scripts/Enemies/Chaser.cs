using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Chaser : MoveStrategy
    {
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
        }

        public override void HandleMovement()
        {
            _mover.Move();
            _rotater.Rotate(_mover.TargetPosition);
        }

        public override void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
        }
    }
}