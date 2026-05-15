using System.Collections;
using Source.Scripts.Enemies.HostileStrategies;
using Source.Scripts.Enemies.MoveStrategies;
using Source.Scripts.Interfaces;
using Source.Scripts.Utillities;
using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyHostileState : EnemyState, IUpdatable
    {
        private EnemySearchState _searchState;

        private MoveStrategy _moveStrategy;
        private HostileStrategy _hostileStrategy;
        private SearchStrategy _searchStrategy;

        private WaitForSeconds _searchCooldown;
        private Coroutine _loseTargetCoroutine;

        public EnemyHostileState(Enemy enemy, EnemyStateMachine stateMachine, float searchCooldown) : base(enemy,
            stateMachine)
        {
            _searchCooldown = new WaitForSeconds(searchCooldown);
        }

        public void Initialize(EnemySearchState searchState, MoveStrategy moveStrategy, HostileStrategy hostileStrategy,
            SearchStrategy searchStrategy)
        {
            _searchState = searchState;
            _moveStrategy = moveStrategy;
            _hostileStrategy = hostileStrategy;
            _searchStrategy = searchStrategy;
        }

        public void UpdateState()
        {
            _moveStrategy.HandleMovement();
            _hostileStrategy.Execute();

            _searchStrategy.CheckForTarget();
        }

        public override void Enter()
        {
            _searchStrategy.OnTargetFound += OnPlayerDetected;
        }

        public override void Exit()
        {
            _searchStrategy.OnTargetFound -= OnPlayerDetected;
        }

        public void SetTarget(Transform target)
        {
            if (target.TryGetComponent(out Player.Player player))
            {
                _moveStrategy.SetTarget(target);
                _hostileStrategy.SetTarget(target);
            }
        }

        private void OnPlayerDetected(Transform target)
        {
            if (_loseTargetCoroutine != null)
            {
                CoroutineHandler.Instance.StopCoroutine(_loseTargetCoroutine);
            }

            _loseTargetCoroutine = CoroutineHandler.Instance.StartCoroutine(LoseTargetAfterDelay());
        }

        private IEnumerator LoseTargetAfterDelay()
        {
            yield return _searchCooldown;
            LoseTarget();
        }

        private void LoseTarget()
        {
            StateMachine.SetState(_searchState);
        }
    }
}