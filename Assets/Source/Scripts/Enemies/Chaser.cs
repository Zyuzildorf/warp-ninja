using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Chaser : MoveStrategy
    {
        private EnemyMover _mover;
        private EnemyRotater _rotater;
        private Transform _target;
        
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            _mover = new EnemyMover(_transform, _rigidbody);
            _rotater = new EnemyRotater(_transform);
            
            _mover.SetTarget(_target);
            _mover.SetSpeed(Speed);
        }

        public override void HandleMovement()
        {
            _mover.Move();
            _rotater.Rotate(_target.position);
        }

        public override void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}