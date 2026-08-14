using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class ProjectileSpawner : ObjectsPool<Projectile>
    {
        [SerializeField] private Transform _firePoint;

        private Vector3 _currentDirection;
        private float _currentSpeed;
        private float _currentLifetime;

        public event Action<IHealthObject> OnProjectileHit;
        
        public void SetProjectileSettings(float speed, float lifetime)
        {
            _currentSpeed = speed;
            _currentLifetime = lifetime;
        }
        
        public void SpawnBullet(Vector3 target)
        {
            _currentDirection = target - _firePoint.position;
            
            GetObject();
        }

        protected override void OnGet(Projectile obj)
        {
            obj.PrefferToDestroyed += ReleaseObject;
            obj.OnHitHealthObject += OnHitHealthObject;
            obj.transform.position = _firePoint.position;
            
            base.OnGet(obj);

            obj.Initialize(_currentSpeed, _currentLifetime);
            obj.SetVelocity(_currentDirection);
            obj.StartLifeTimeDecreasing();
        }

        protected override void OnRelease(Projectile obj)
        {
            obj.PrefferToDestroyed -= ReleaseObject;
            obj.OnHitHealthObject -= OnHitHealthObject;

            base.OnRelease(obj);
        }

        private void OnHitHealthObject(IHealthObject healthObject)
        {
            OnProjectileHit?.Invoke(healthObject);
        }
    }
}