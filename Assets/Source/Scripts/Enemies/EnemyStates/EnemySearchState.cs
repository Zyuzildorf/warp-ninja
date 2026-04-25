using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemySearchState : EnemyState, IUpdatable
    {
        private EnemyHostileState  _hostileState;
        private TargetFinder _targetFinder;
        
        public EnemySearchState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        
        public void UpdateState()
        {
            FindTarget();
        }
        
        public void Initialize(EnemyHostileState hostileState, TargetFinder targetFinder)
        {
            _hostileState = hostileState;
            _targetFinder = targetFinder;
        }

        private void FindTarget()
        {
            if (_targetFinder.TryFindTarget(out Transform target))
            {
                _hostileState.SetTarget(target);
                StateMachine.SetState(_hostileState);
            }
        }
    }
}