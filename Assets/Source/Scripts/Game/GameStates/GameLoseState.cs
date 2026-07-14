using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Game.GameStates
{
    public class GameLoseState : GameState
    {
        public GameLoseState(GameLogic gameLogic, GameStateMachine stateMachine) : base(gameLogic, stateMachine)
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