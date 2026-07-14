using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Game.GameStates
{
    public class GamePlayingState : GameState
    {
        public GamePlayingState(GameLogic gameLogic, GameStateMachine stateMachine) : base(gameLogic, stateMachine)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 1f;
        }

        public override void Exit()
        {
            
        }
    }
}