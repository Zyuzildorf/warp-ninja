using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyMover
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Transform _target;
        private float _speed;
        
        public EnemyMover(Transform transform, Rigidbody rigidbody,  float speed)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _speed = speed;
        }

        public Vector3 TargetPosition => _target.position;

        public void Move()
        {
            Vector3 distance = _target.position - _transform.position;
            Vector3 direction = new Vector3(distance.x, 0, 0).normalized;

            _rigidbody.velocity = direction * _speed;
            _rigidbody.velocity += Physics.gravity;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}