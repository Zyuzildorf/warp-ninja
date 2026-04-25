using Source.Scripts.Game;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using Source.Scripts.Player.PlayerStates;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(InputReader), typeof(PlayerPointer), typeof(PlayerRotater))]
    [RequireComponent(typeof(PlayerShooter), typeof(PlayerTeleporter), typeof(PlayerEnergy))]
    public class Player : MonoBehaviour, IHealthObject
    {
        [SerializeField] public int _maxHealth;
        
        private PlayerStateMachine _stateMachine;
        private PlayerIdleState _idleState;
        private PlayerAimState _aimState;
        private PlayerThrowState _throwState;
        private PlayerTeleportState _teleportState;
        
        private PlayerPointer _pointer;
        private PlayerRotater _rotater;
        private PlayerShooter _shooter;
        private PlayerTeleporter _teleporter;
        private PlayerEnergy _energy;
        private Health _health;
        
        private Rigidbody _rigidbody;
        private Collider _collider;
        
        public InputReader InputReader {get; private set;}

        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            _pointer = GetComponent<PlayerPointer>();
            _rotater = GetComponent<PlayerRotater>();
            _shooter = GetComponent<PlayerShooter>();
            _energy = GetComponent<PlayerEnergy>();
            _teleporter = GetComponent<PlayerTeleporter>();
            
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            
            Initialize();
        }

        private void Start()
        {
            _stateMachine.SetState(_idleState);
        }

        public void HandleDamage(int damage)
        {
            //Добавить валидацию
            _health.TakeDamage(damage);
        }
        
        private void Initialize()
        {
            _health = new Health(_maxHealth);
            
            _stateMachine = new PlayerStateMachine();
            
            _idleState = new PlayerIdleState(this,  _stateMachine);
            _aimState = new PlayerAimState(this,  _stateMachine);
            _throwState = new PlayerThrowState(this,  _stateMachine);
            _teleportState = new PlayerTeleportState(this, _stateMachine);
            
            _idleState.Initialize(_aimState);
            _aimState.Initialize(_throwState, _pointer, _rotater);
            _throwState.Initialize(_aimState, _teleportState, _energy, _shooter);
            _teleportState.Initialize(_aimState, _teleporter);
            
            _shooter.Initialize(_rigidbody);
            _teleporter.Initialize(_collider, _rigidbody);
        }

    }
}