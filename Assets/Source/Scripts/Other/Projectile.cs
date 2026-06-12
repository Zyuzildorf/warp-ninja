using System;
using System.Collections;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private float _speed;
        private float _lifeTime;

        private WaitForSeconds _waitForSeconds;

        public event Action<Projectile> PrefferToDestroyed;
        public event Action<IHealthObject> OnHitHealthObject;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }

        private void OnDisable()
        {
            Die();
        }

        private void OnDestroy()
        {
            PrefferToDestroyed = null;
        }

        private void OnCollisionEnter(Collision other)
        {
            HandleHit(other);
        }

        private void HandleHit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthObject healthObject))
            {
                OnHitHealthObject?.Invoke(healthObject);
                Die();
            }
            else
            {
                Die();
            }
        }

        public void Initialize(float speed, float lifetime)
        {
            _speed = speed;
            _lifeTime = lifetime;
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }
        
        public void SetVelocity(Vector2 direction)
        {
            _rigidbody.velocity = direction.normalized * _speed;
        }

        public void StartLifeTimeDecreasing()
        {
            StartCoroutine(DecreaseLifeTime());
        }

        private IEnumerator DecreaseLifeTime()
        {
            yield return _waitForSeconds;
            
            Die();
        }

        private void Die()
        {
            PrefferToDestroyed?.Invoke(this);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}