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
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _searchCooldown;
        
        [SerializeField] private MoveStrategy _searchMoveStrategy;
        [SerializeField] private MoveStrategy _hostileMoveStrategy;
        [SerializeField] private HostileStrategy _hostileStrategy;
        [SerializeField] private SearchStrategy _searchStrategy;
        
        private EnemySearchState _searchState;
        private EnemyHostileState  _hostileState;
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

        public virtual void HandleDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Init()
        {
            _health = new Health(_maxHealth);

            _stateMachine = new EnemyStateMachine();
            
            _searchState = new EnemySearchState(this, _stateMachine);
            _hostileState = new EnemyHostileState(this, _stateMachine, _searchCooldown);
            
            _searchState.Initialize(_hostileState, _searchStrategy, _searchMoveStrategy);
            _hostileState.Initialize(_searchState, _hostileMoveStrategy, _hostileStrategy, _searchStrategy);
        }
    }
}