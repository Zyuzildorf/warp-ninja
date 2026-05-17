using UnityEngine;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyDieState : EnemyState
    {
        public EnemyDieState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enemy killed");
        }
    }
}