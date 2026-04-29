using System.Collections;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyWaypointsMover : Mover
    {
        private readonly Collider _moveZone;
        private readonly float _safetyMargin;
        private readonly WaitForSeconds _waitBeforeMove;
        
        private bool _isOnWaypoint;

        public EnemyWaypointsMover(Transform transform, Rigidbody rigidbody, Collider moveZone, float safetyMargin,
            float waitBeforeMove)
        {
            Transform = transform;
            Rigidbody = rigidbody;

            _moveZone = moveZone;
            _safetyMargin = safetyMargin;
            _waitBeforeMove = new WaitForSeconds(waitBeforeMove);

            _isOnWaypoint = false;
            }

        public override void Move()
        {
            if (_isOnWaypoint) return;

            if (CheckDestination())
            {
                CoroutineHandler.Instance.StartCoroutine(ChangeWaypoint());
            }

            Vector3 direction = Target.position - Transform.position;
            Rigidbody.velocity = direction.normalized * Speed;
        }

        public override void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public override void SetTarget(Transform target)
        {
            Target = target;
        }

        private bool CheckDestination()
        {
            if (Transform.position.x - Target.position.x < _safetyMargin)
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
            CheckDestination();
        }

        private void SetRandomWaypoint()
        {
            float randomXPos = Random.Range(_moveZone.bounds.min.x, _moveZone.bounds.max.x);
            Target.position = new Vector3(randomXPos, Transform.position.y, Transform.position.z);
        }
    }
}