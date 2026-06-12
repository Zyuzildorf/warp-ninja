using System.Collections;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Enemies.HostileStrategies
{
    [RequireComponent(typeof(ProjectileSpawner))]
    public class RangedAttacker : HostileStrategy
    {
        [Header("Burst Settings")]
        [SerializeField] private float _burstInterval;
        [SerializeField] private float _burstCooldown;
        [SerializeField] private int _burstSize;
        
        [Header("Projectile Settings")]
        [SerializeField] private float _projectileSpeed = 10f;
        [SerializeField] private float _projectileLifeTime = 2f;
        [SerializeField] private int _damage;
            
        private ProjectileSpawner _projectileSpawner;
        private WaitForSeconds _burstWait;
        private WaitForSeconds _cooldownWait;
        private bool _isOnCooldown;
        private Transform _target;

        private void Awake()
        {
            _projectileSpawner = GetComponent<ProjectileSpawner>();
            _burstWait = new WaitForSeconds(_burstInterval);
            _cooldownWait = new WaitForSeconds(_burstCooldown);
            _isOnCooldown = false;
            
            _projectileSpawner.SetProjectileSettings(_projectileSpeed, _projectileLifeTime);
        }

        private void OnEnable()
        {
            _projectileSpawner.OnProjectileHit += HandleHit;
        }

        private void OnDisable()
        {
            _projectileSpawner.OnProjectileHit -= HandleHit;
        }

        public override void Execute()
        {
            if (_isOnCooldown) return;
           
            StartCoroutine(BurstAttackRoutine());
        }

        public override void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void Exit()
        {
            StopAllCoroutines();
            _isOnCooldown = false;
        }

        private IEnumerator BurstAttackRoutine()
        {
            _isOnCooldown = true;
            
            for (int i = 0; i < _burstSize; i++)
            {
                Shoot();
                yield return _burstWait;     
            }

            yield return _cooldownWait;

            _isOnCooldown = false;
            
        }
        
        private void Shoot()
        {
            if(_target == null) return;
            
            _projectileSpawner.SpawnBullet(_target.position);
        }

        private void HandleHit(IHealthObject healthObject)
        {
            healthObject.HandleDamage(_damage);
        }
    }
}