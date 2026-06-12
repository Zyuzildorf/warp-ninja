using System.Collections;
using Source.Scripts.Interfaces;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.HostileStrategies
{
    public class MeleeAttacker : HostileStrategy
    {
        [Header("Attack Timing")] 
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _attackDuration = 0.3f;

        [Header("Attack Settings")] 
        [SerializeField] private float _distance;
        [SerializeField] private int _damage;

        private Transform _target;
        private IHealthObject _healthObject;
        private float _nextAttackTime;
        private bool _isAttacking;

        public override void Execute()
        {
            if (_target == null) return;
            if (Time.time < _nextAttackTime) return;
            if (_isAttacking) return;
            if (!IsTargetInRange()) return;

            StartCoroutine(PerformAttack());
        }

        public override void SetTarget(Transform target)
        {
            CheckerForNull.ThrowIfNullArgument(target);

            _target = target;
            _healthObject = _target?.GetComponent<IHealthObject>();
        }

        public override void Exit()
        {
            StopAllCoroutines();
            _isAttacking = false;
        }

        private bool IsTargetInRange()
        {
            return _target.position.IsEnoughClose(transform.position, _distance);
        }

        private IEnumerator PerformAttack()
        {
            _isAttacking = true;
            _nextAttackTime = Time.time + _attackDelay;

            float activeEnd = Time.time + _attackDuration;
            bool hitRegistered = false;

            while (Time.time < activeEnd && !hitRegistered)
            {
                if (IsTargetInRange())
                {
                    _healthObject?.HandleDamage(_damage);
                    hitRegistered = true;
                }

                yield return null;
            }

            _isAttacking = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distance);
        }
    }
}