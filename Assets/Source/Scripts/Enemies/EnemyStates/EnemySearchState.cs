using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemySearchState : EnemyState, IUpdatable
    {
        private EnemyHostileState  _hostileState;
        
        private TargetFinder _targetFinder;
        private MoveStrategy _moveStrategy;
        
        public EnemySearchState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        
        public void Initialize(EnemyHostileState hostileState, TargetFinder targetFinder, 
            MoveStrategy moveStrategy, HostileStrategy hostileStrategy)
        {
            _hostileState = hostileState;
            _targetFinder = targetFinder;
            
            _moveStrategy = moveStrategy;
        }
        
        public void UpdateState()
        {
            FindTarget();
            
            _moveStrategy.HandleMovement();
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