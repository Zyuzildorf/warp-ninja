using Source.Scripts.Utillities;
using Source.Scripts.Weapons;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(AntiStackResolver))]
    public class PlayerTeleporter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private Transform _transform;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private AntiStackResolver _antiStackResolver;

        private void Awake()
        {
            _transform = transform;
            _antiStackResolver = GetComponent<AntiStackResolver>();

            CheckerForNull.ThrowIfNullArgument(_weapon);
        }

        public void Initialize(Collider collider, Rigidbody rigidbody)
        {
            CheckerForNull.ThrowIfNullArgument(collider);
            CheckerForNull.ThrowIfNullArgument(rigidbody);
            
            _collider = collider;
            _rigidbody = rigidbody;
        }
        
        public void Teleport()
        {
            _transform.position = _antiStackResolver.GetUnstuckPosition(_weapon.transform.position, _collider);
            _weapon.Release();
            
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
    }
}