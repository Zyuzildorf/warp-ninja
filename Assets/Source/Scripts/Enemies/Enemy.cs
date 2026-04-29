using Source.Scripts.Enemies.EnemyStates;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IHealthObject
    {
        [SerializeField] private int _maxHealth;
        
        [SerializeField] private MoveStrategy _searchMoveStrategy;
        [SerializeField] private MoveStrategy _hostileMoveStrategy;
        [SerializeField] private HostileStrategy _hostileStrategy;
        
        private EnemySearchState _searchState;
        private EnemyHostileState  _hostileState;
        private EnemyStateMachine _stateMachine;
        
        private ISearchPattern _searchPattern;
        private TargetFinder _targetFinder;
        
        private Health _health;

        private void Awake()
        {
            
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }

        public virtual void HandleDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Init()
        {
            
        }
    }
}