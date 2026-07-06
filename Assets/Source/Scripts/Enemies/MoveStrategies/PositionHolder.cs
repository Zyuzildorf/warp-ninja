using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.MoveStrategies
{
    public class PositionHolder : MoveStrategy
    {
        [SerializeField] private float _safetyMargin = 0.5f;

        private Transform _startPosition;
        private bool _isRightLooking;

        private EnemyMover _mover;
        private EnemyRotater _rotater;

        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _startPosition = transform;
            _rigidbody = GetComponent<Rigidbody>();

            _mover = new EnemyMover(_transform, _rigidbody, MoveSpeed);
            _rotater = new EnemyRotater(_transform, RotationSpeed);
        }

        public void SetStartPosition(Vector3 dataStartPosition)
        {
            _startPosition.position = dataStartPosition;
            _mover.SetTarget(_startPosition);
            
            if(_startPosition != null) Debug.Log("Start position set");
        }

        public override void HandleMovement()
        {
            if (_mover == null || _rotater == null)
            {
                return;
            }
            
            if (IsOnPosition())
            {
                RotateToSide();
                return;
            }

            _mover.Move();
            _rotater.Rotate(_startPosition.position);
        }

        public override void SetTarget(Transform target)
        {
            CheckerForNull.ThrowIfNullArgument(target);

            _mover.SetTarget(target);
        }

        public void SetDirection(bool isRight)
        {
            if (isRight)
            {
                _isRightLooking = true;
            }
            else
            {
                _isRightLooking = false;
            }
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

        private void RotateToSide()
        {
            if (_isRightLooking)
            {
                _rotater.Rotate(_transform.position + Vector3.right);
            }
            else
            {
                _rotater.Rotate(_transform.position + Vector3.left);
            }
        }
    }
}