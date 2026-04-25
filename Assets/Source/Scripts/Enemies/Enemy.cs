using Source.Scripts.Enemies.EnemyStates;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IHealthObject
    {
        [SerializeField] private int _maxHealth;
        
        private EnemySearchState _searchState;
        private EnemyHostileState  _hostileState;
        private EnemyStateMachine _stateMachine;

        private IHostilePattern _hostilePattern;
        private IMovePattern _movePattern;
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