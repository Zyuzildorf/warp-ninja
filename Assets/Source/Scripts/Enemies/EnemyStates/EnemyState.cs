using Source.Scripts.Other;

namespace Source.Scripts.Enemies.EnemyStates
{
    public class EnemyState : State
    {
        public Enemy Enemy { get; private set; }
        public EnemyStateMachine StateMachine { get; private set; }

        public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
        }
    }
}