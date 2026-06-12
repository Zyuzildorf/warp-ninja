using Source.Scripts.Enemies.MoveStrategies;
using Source.Scripts.Interfaces;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemySearchState : EnemyState, IUpdatable
    {
        private EnemyHostileState _hostileState;

        private SearchStrategy _searchStrategy;
        private MoveStrategy _moveStrategy;

        public EnemySearchState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public void Initialize(EnemyHostileState hostileState, SearchStrategy searchStrategy,
            MoveStrategy moveStrategy)
        {
            CheckerForNull.ThrowIfNullArgument(hostileState);
            CheckerForNull.ThrowIfNullArgument(searchStrategy);
            CheckerForNull.ThrowIfNullArgument(moveStrategy);
            
            _hostileState = hostileState;
            _searchStrategy = searchStrategy;

            _moveStrategy = moveStrategy;
            
        }

        public void UpdateState()
        {
            _searchStrategy.CheckForTarget();
            _moveStrategy.HandleMovement();
        }

        public override void Enter()
        {
            _searchStrategy.OnTargetFound += HandleTarget;
        }

        public override void Exit()
        {
            _searchStrategy.OnTargetFound -= HandleTarget;
        }

        private void HandleTarget(Transform target)
        {
            CheckerForNull.ThrowIfNullArgument(target);
            
            _hostileState.SetTarget(target);
            StateMachine.SetState(_hostileState);
        }
    }
}