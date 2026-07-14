using System;
using Source.Scripts.Game.GameStates;
using UnityEngine;

namespace Source.Scripts.Game
{
    public class GameLogic : MonoBehaviour
    {
        private GameMenuState _menuState;
        private GamePlayingState _playingState;
        private GamePausedState _pausedState;
        private GameLoseState _loseState;
        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new GameStateMachine();
            
            _menuState = new GameMenuState(this, _stateMachine);
            _playingState = new GamePlayingState(this, _stateMachine);
            _pausedState = new GamePausedState(this, _stateMachine);
            _loseState = new GameLoseState(this, _stateMachine);
        }

        private void Update()
        {
            
        }
    }
}
