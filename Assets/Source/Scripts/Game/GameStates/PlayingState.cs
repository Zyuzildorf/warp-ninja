using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Game.GameStates
{
    public class PlayingState : GameState
    {
        //Игровой процесс, пауза, 
        
        public PlayingState(Main main, GameStateMachine stateMachine) : base(main, stateMachine)
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