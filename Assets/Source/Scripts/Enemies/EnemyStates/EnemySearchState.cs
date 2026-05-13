using Source.Scripts.Interfaces;
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
            _hostileState.SetTarget(target);
            StateMachine.SetState(_hostileState);
        }
    }
}