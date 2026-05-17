using System;
using Source.Scripts.Interfaces;
using Source.Scripts.Weapons;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private int _damage = 1;

        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _weapon.OnHitHealthObject += HandleAttack;
        }

        private void OnDisable()
        {
            _weapon.OnHitHealthObject -= HandleAttack;
        }

        public void Initialize(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Attack(Vector3 direction)
        {
            direction.z = 0;
            Debug.Log("Direction: " + direction);
            _weapon.Get();

            _weapon.transform.position = new Vector3(_shootPoint.position.x, _shootPoint.position.y, 0);
            _weapon.transform.rotation = Quaternion.LookRotation(direction);

            _weapon.SetVelocity(direction * _speed);

            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }

        private void HandleAttack(IHealthObject obj)
        {
            obj.HandleDamage(_damage);
        }
    }
}