using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Game.GameStates
{
    public class LoseState : GameState
    {
        public LoseState(Main main, GameStateMachine stateMachine) : base(main, stateMachine)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }
}