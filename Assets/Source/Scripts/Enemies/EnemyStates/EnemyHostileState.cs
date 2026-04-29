using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyHostileState : EnemyState, IUpdatable
    {
        private EnemySearchState  _searchState;
        
        private MoveStrategy _moveStrategy;
        private HostileStrategy _hostileStrategy;
        
        public EnemyHostileState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public void Initialize(EnemySearchState searchState, MoveStrategy moveStrategy, HostileStrategy hostileStrategy)
        {
            _searchState = searchState;
            _moveStrategy = moveStrategy;
            _hostileStrategy = hostileStrategy;
        }
        
        public void UpdateState()
        {
            _moveStrategy.HandleMovement();
            _hostileStrategy.Execute();
        }

        public void SetTarget(Transform target)
        {
            if (target.TryGetComponent(out Player.Player player))
            {
                _moveStrategy.SetTarget(target);
            }
        }
    }
}