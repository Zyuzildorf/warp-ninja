using UnityEngine;

namespace Source.Scripts.Enemies.MoveStrategies
{
    public class Patroler : MoveStrategy
    {
        [Header("Patrol Settings")]
        [SerializeField] private float _safetyMargin;
        [SerializeField] private float _waitBeforeMove;

        private EnemyRotater _rotater;
        private EnemyWaypointsMover _mover;

        private Collider _moveZone;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();

            _rotater = new EnemyRotater(_transform, RotationSpeed);
            _mover = new EnemyWaypointsMover(_transform, _rigidbody, MoveSpeed, _safetyMargin,
                _waitBeforeMove);
        }

        public void SetMoveZone(Collider moveZone)
        {
            _moveZone = moveZone;
            _mover.SetMoveZone(moveZone);
            if(_moveZone != null) Debug.Log("Move Zone set");
        }
        
        public override void HandleMovement()
        {
            if (_mover == null || _rotater == null || _moveZone == null)
            {
                return;
            }
            
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