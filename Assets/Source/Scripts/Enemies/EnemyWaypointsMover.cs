using System.Collections;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyWaypointsMover
    {
        private readonly float _safetyMargin;
        private readonly WaitForSeconds _waitBeforeMove;
        private readonly float _speed;

        private bool _isOnWaypoint;
        private Collider _moveZone;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Coroutine _changeWaypointCoroutine;
        
        public EnemyWaypointsMover(Transform transform, Rigidbody rigidbody, float speed, float safetyMargin,
            float waitBeforeMove)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _speed = speed;

            _safetyMargin = safetyMargin;
            _waitBeforeMove = new WaitForSeconds(waitBeforeMove);

            _isOnWaypoint = false;
        }
        
        public Vector3 CurrentWaypoint {get; private set;}

        public void Move()
        {
            if (_isOnWaypoint)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            
            if(_moveZone == null) return;
            
            Vector3 distance = CurrentWaypoint - _transform.position;
            Vector3 horizontalDirection = new Vector3(distance.x, 0, 0).normalized;
            
            _rigidbody.velocity = horizontalDirection * _speed;
            _rigidbody.velocity += Physics.gravity;

            if (CheckDestination())
            {
                _isOnWaypoint = true;
                _rigidbody.velocity = Vector3.zero;

                if (_changeWaypointCoroutine != null)
                {
                    CoroutineHandler.Instance.StopCoroutine(_changeWaypointCoroutine);
                }
                
                _changeWaypointCoroutine = CoroutineHandler.Instance.StartCoroutine(ChangeWaypoint());
            }
        }

        public void SetTarget(Transform target)
        {
            CheckerForNull.ThrowIfNullArgument(target);
            
            CurrentWaypoint = target.position;
        }
        
        public void SetMoveZone(Collider moveZone)
        {
            _moveZone = moveZone;
            SetRandomWaypoint();
        }

        private bool CheckDestination()
        {
            float distance = Vector3.Distance(_transform.position, CurrentWaypoint);
            
            if (distance <= _safetyMargin)
            {
                _isOnWaypoint = true;
                return true;
            }

            _isOnWaypoint = false;
            return false;
        }

        private IEnumerator ChangeWaypoint()
        {
            yield return _waitBeforeMove;
            
            SetRandomWaypoint();
            _isOnWaypoint = false;
            
            _changeWaypointCoroutine = null;
        }

        private void SetRandomWaypoint()
        {
            if (_moveZone == null)
            {
                CurrentWaypoint = _transform.position;
                return;
            }
            
            float randomXPos = Random.Range(_moveZone.bounds.min.x, _moveZone.bounds.max.x);
            CurrentWaypoint = new Vector3(randomXPos, _transform.position.y, _transform.position.z);
        }
    }
}