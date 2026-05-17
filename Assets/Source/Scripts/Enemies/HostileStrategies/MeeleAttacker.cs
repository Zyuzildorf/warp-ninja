using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Interfaces;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.HostileStrategies
{
    public class MeeleAttacker : HostileStrategy
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _distance;
        [SerializeField] private int _damage;

        private WaitForSeconds _waitForSeconds;
        private Transform _target;
        private List<Collider> _hit;
        private bool _isAttackDelayOver;
        
        private IHealthObject _healthObject;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_attackDelay);
            _isAttackDelayOver = true;
        }

        public override void Execute()
        {
            if (_isAttackDelayOver == false)
                return;

            Debug.Log(IsAttackPossible(_target.position) + "possible of attack");
            if (IsAttackPossible(_target.position))
            {
                _healthObject.HandleDamage(_damage);
                Debug.Log("Hit");
            }

            StartCoroutine(WaitForNextAttack());
        }

        public override void SetTarget(Transform target)
        {
            _target = target;
            _healthObject = _target.GetComponent<IHealthObject>();
        }

        private bool IsAttackPossible(Vector3 target)
        {
            return target.IsEnoughClose(transform.position, _distance);
        }

        private IEnumerator WaitForNextAttack()
        {
            _isAttackDelayOver = false;
            Debug.Log("Waiting For Attack");
            yield return _waitForSeconds;
            _isAttackDelayOver = true;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distance);
        }
    }
}