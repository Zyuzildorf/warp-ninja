using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Kunai : Weapon
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private LayerMask _bounceLayerMask;
        [SerializeField] private float _zPosition = 0f;
        [SerializeField] private float _bounceEnergyLoss = 0.6f;
        [SerializeField] private float _minRotationSpeed = 1f;
        [SerializeField] private float _maxRotationSpeed = 25f;
        [SerializeField] private float _gravityScale = 1f;

        private Rigidbody _rigidbody;
        private Vector3 _velocityBeforeCollision;
        private float _affectedRotationSpeed;
        private bool _isThrown;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _velocityBeforeCollision = _rigidbody.velocity;

           EnforceZPosition();

           ApplyCustomGravity();
            
            if (_isThrown)
            {
                float currentY =  _rigidbody.velocity.normalized.y;
                transform.Rotate(CalculateRotationSpeed(currentY));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            HandleBounce(other);
            HandleHit(other);
        }

        public override void SetVelocity(Vector3 velocity)
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            
            velocity.z = 0;
            _rigidbody.AddForce(velocity, ForceMode.Impulse);

            _isThrown = true;
        }

        public override void Get()
        {
            gameObject.SetActive(true);

            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        public override void Release()
        {
            _isThrown = false;
            gameObject.SetActive(false);
        }

        private void HandleBounce(Collision collision)
        {
            if (collision.gameObject.layer == _bounceLayerMask)
            {
                Vector3 normal = collision.contacts[0].normal;
                normal.z = 0;
                normal.Normalize();

                BounceOff(normal);

                _isThrown = false;
            }
        }


        private void HandleHit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player.Player player))
            {
                return;
            }
            
            if (collision.gameObject.TryGetComponent(out IHealthObject healthObject))
            {
                InvokeOnHitHealthObject(healthObject);
            }
        }
        
        private Vector3 CalculateRotationSpeed(float yComponent)
        {
            float speed = Mathf.Lerp(_minRotationSpeed, _maxRotationSpeed, yComponent);
            
            return Vector3.right * speed;
        }
        
        private void EnforceZPosition()
        {
            Vector3 position = transform.position;

            if (Mathf.Abs(position.z - _zPosition) > 0.001f)
            {
                position.z = _zPosition;
                transform.position = position;
            }
        }

        private void ApplyCustomGravity()
        {
            Vector3 gravity = Physics.gravity * _gravityScale * Time.deltaTime;
            gravity.z = 0;
            
            _rigidbody.velocity += gravity;
        }
        
        private void BounceOff(Vector3 normal)
        {
            Vector3 velocity = _velocityBeforeCollision;
            velocity.z = 0;

            Vector3 reflectedVelocity = Vector3.Reflect(velocity, normal);
            reflectedVelocity.z = 0;

            reflectedVelocity *= (1f - _bounceEnergyLoss);

            _rigidbody.velocity = reflectedVelocity;
        }
    }
}