using Source.Scripts.Game;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(PlayerTeleporter), typeof(PlayerShooter), typeof(InputReader))]
    [RequireComponent(typeof(PlayerRotater), typeof(PlayerPointer), typeof(PlayerEnergy))]
    public class Player : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _rigidbody;
        private InputReader _inputReader;
        private PlayerTeleporter _teleporter;
        private PlayerShooter _shooter;
        private PlayerRotater _rotater;
        private PlayerPointer _pointer;
        private PlayerEnergy _energy;

        private bool _isAiming;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            _shooter = GetComponent<PlayerShooter>();
            _teleporter = GetComponent<PlayerTeleporter>();
            _inputReader = GetComponent<InputReader>();
            _rotater = GetComponent<PlayerRotater>();
            _pointer = GetComponent<PlayerPointer>();
            _energy = GetComponent<PlayerEnergy>();

            Initialize();
        }

        private void OnEnable()
        {
            _inputReader.OnTeleportKeyPress += TryTeleport;
            _inputReader.OnMouseMoved += Rotate;
            _inputReader.OnMouseOver += TryAttack;
            _rotater.OnRotationChanged += ChangeDirection;
        }

        private void OnDisable()
        {
            _inputReader.OnTeleportKeyPress -= TryTeleport;
            _inputReader.OnMouseMoved -= Rotate;
            _inputReader.OnMouseOver -= TryAttack;
            _rotater.OnRotationChanged -= ChangeDirection;
        }

        private void Initialize()
        {
            _teleporter.Initialize(_collider, _rigidbody);
            _shooter.Initialize(_rigidbody);
        }

        private void TryTeleport(Vector3 mousePos)
        {
            if (_energy.TryUseEnergy())
            {
                _rotater.SetStartPosition(mousePos);

                _teleporter.Teleport();
                _pointer.GetObject();

                _isAiming = true;
            }
        }

        private void Rotate(Vector3 newPosition)
        {
            if (_isAiming)
            {
                _rotater.Rotate(newPosition);
            }
        }

        private void TryAttack()
        {
            if (_isAiming)
            {
                _pointer.ReleaseObject();
                _shooter.Attack(_rotater.Direction);
                
                _isAiming = false;
            }
        }

        private void ChangeDirection(Vector3 newDirection)
        {
            _pointer.ChangeRotation(newDirection);
        }
    }
}