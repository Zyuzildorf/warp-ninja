using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyHostileState : EnemyState, IUpdatable
    {
        private EnemySearchState  _searchState;
        
        private EnemyMover _mover;
        private EnemyRotater _rotater;
        private EnemyAttacker _attacker;
        
        private Transform _target;

        public EnemyHostileState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        
        public void UpdateState()
        {
            
        }

        public void SetTarget(Transform target)
        {
            if (target.TryGetComponent(out Player.Player player))
            {
                _target = target;
            }
        }

        public void Initialize(EnemySearchState searchState, EnemyMover mover, EnemyRotater rotater,
            EnemyAttacker attacker)
        {
            _searchState = searchState;
            _mover = mover;
            _rotater = rotater;
            _attacker = attacker;
        }
        
        private void HandleMovement()
        {
            
            
            Attack();
        }

        private void Attack()
        {
            _attacker.Attack();
        }
    }
}