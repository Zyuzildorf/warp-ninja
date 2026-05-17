using UnityEngine;

namespace Source.Scripts.Enemies.MoveStrategies
{
    public class Patroler : MoveStrategy
    {
        [SerializeField] private Collider _moveZone;
        [SerializeField] private float _safetyMargin;
        [SerializeField] private float _waitBeforeMove;

        private EnemyRotater _rotater;
        private EnemyWaypointsMover _mover;

        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();

            _rotater = new EnemyRotater(_transform, RotationSpeed);
            _mover = new EnemyWaypointsMover(_transform, _rigidbody, MoveSpeed, _moveZone, _safetyMargin,
                _waitBeforeMove);
        }

        public override void HandleMovement()
        {
            _mover.Move();

            if (_mover.CurrentWaypoint != Vector3.zero)
            {
                _rotater.Rotate(_mover.CurrentWaypoint);
            }
        }

        public override void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
        }
    }
}