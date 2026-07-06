using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.MoveStrategies
{
    public class Chaser : MoveStrategy
    {
        [Header("Chase Settings")]
        [SerializeField] private float _closeDistance;

        private EnemyMover _mover;
        private EnemyRotater _rotater;

        private Collider _threatZone;
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();

            _mover = new EnemyMover(_transform, _rigidbody, MoveSpeed);
            _rotater = new EnemyRotater(_transform, RotationSpeed);
        }

        public void SetThreatZone(Collider movementZone)
        {
            _threatZone  = movementZone;
            
            if(_threatZone != null) Debug.Log("Threat Zone set");
        }
        
        public override void HandleMovement()
        {
            if (_mover == null || _rotater == null || _threatZone == null)
            {
                return;
            }
            
            Vector3 desiredVelocity = _mover.CalculateDesiredVelocity();
            Vector3 futurePosition = _transform.position + desiredVelocity * Time.fixedDeltaTime;

            if (IsPositionInThreatZone(futurePosition))
            {
                if (Vector3Extensions.IsEnoughClose(_transform.position, _mover.TargetPosition, _closeDistance) ==
                    false)
                {
                    _mover.Move();
                }
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }

            _rotater.Rotate(_mover.TargetPosition);
        }

        public override void SetTarget(Transform target)
        {
            CheckerForNull.ThrowIfNullArgument(target);
            
            _mover.SetTarget(target);
        }

        private bool IsPositionInThreatZone(Vector3 position)
        {
            if (position.x < _threatZone.bounds.min.x) return false;
            if (position.x > _threatZone.bounds.max.x) return false;

            return true;
        }

    }
}