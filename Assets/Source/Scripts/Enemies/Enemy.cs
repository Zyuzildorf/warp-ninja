using Source.Scripts.Enemies.EnemyStates;
using Source.Scripts.Enemies.HostileStrategies;
using Source.Scripts.Enemies.MoveStrategies;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IHealthObject
    {
        [Header("Health")]
        [SerializeField] private int _maxHealth;
    
        [Header("Search Settings")]
        [SerializeField] private float _searchCooldown;

        [Header("Strategies")]
        [SerializeField] private MoveStrategy _searchMoveStrategy;
        [SerializeField] private MoveStrategy _hostileMoveStrategy;
        [SerializeField] private HostileStrategy _hostileStrategy;
        [SerializeField] private SearchStrategy _searchStrategy;

        private EnemySearchState _searchState;
        private EnemyHostileState _hostileState;
        private EnemyDieState _dieState;
        private EnemyStateMachine _stateMachine;

        private Health _health;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _stateMachine.SetState(_searchState);
        }

        private void FixedUpdate()
        {
            _stateMachine.UpdateCurrentState();
        }

        private void OnEnable()
        {
            _health.OnDeath += Die;
        }

        private void OnDisable()
        {
            _health.OnDeath -= Die;
        }

        public virtual void HandleDamage(int damage)
        {
            if (damage >= 0)
            {
                _health.TakeDamage(damage);
            }
        }

        private void Init()
        {
            _health = new Health(_maxHealth);

            _stateMachine = new EnemyStateMachine();

            _searchState = new EnemySearchState(this, _stateMachine);
            _hostileState = new EnemyHostileState(this, _stateMachine, _searchCooldown);
            _dieState = new EnemyDieState(this, _stateMachine);

            _searchState.Initialize(_hostileState, _searchStrategy, _searchMoveStrategy);
            _hostileState.Initialize(_searchState, _hostileMoveStrategy, _hostileStrategy, _searchStrategy);
        }

        private void Die()
        {
            _stateMachine.SetState(_dieState);

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}