using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyMover :  MonoBehaviour, IMovePattern
    {
        [SerializeField] protected float _speed;
        
        private Transform _target;
        protected Rigidbody Rigidbody;
        
        public virtual void Move()
        {
            Vector3 direction = _target.position - transform.position;
            
            Rigidbody.velocity = direction.normalized * _speed;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}