using Source.Scripts.Other;
using Source.Scripts.Utillities;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyState : State
    {
        public Enemy Enemy { get; private set; }
        public EnemyStateMachine StateMachine { get; private set; }

        public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
        {
            CheckerForNull.ThrowIfNullArgument(enemy);
            CheckerForNull.ThrowIfNullArgument(stateMachine);
            
            Enemy = enemy;
            StateMachine = stateMachine;
        }
    }
}