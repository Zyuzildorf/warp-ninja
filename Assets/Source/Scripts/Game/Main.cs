using System;
using Source.Scripts.Game.GameStates;
using UnityEngine;

namespace Source.Scripts.Game
{
    public class Main : MonoBehaviour
    {
        private LevelLoader _levelLoader;
        private UI _ui;

        private MenuState _menuState;
        private PlayingState _playingState;
        private LoseState _loseState;
        private GameStateMachine _stateMachine;

        public static Main Instance { get; private set; }

        public GameState CurrentState => _stateMachine.CurrentState;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();

            _stateMachine.SetState(_menuState);
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            _stateMachine.UpdateCurrentState();
        }

        private void Update()
        {
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && _stateMachine.CurrentState is PlayingState)
            {
                //_stateMachine.SetState(_pausedState);
            }
            
            // if (pauseStatus)
            // {
            //     _progressManager.SaveProgress();
            // }
        }
        
        private void OnApplicationQuit()
        {
            //_progressManager.SaveProgress();
        }
        
        private void Init()
        {
            _stateMachine = new GameStateMachine();

            _menuState = new MenuState(this, _stateMachine);
            _playingState = new PlayingState(this, _stateMachine);
            _loseState = new LoseState(this, _stateMachine);
       
            //_levelManager.OnLevelLoaded.AddListener(OnLevelLoaded);
            //_levelManager.OnLevelUnloaded.AddListener(OnLevelUnloaded);

            
            //_stateManager.OnStateChanged.AddListener(OnStateChanged);
        }

        private void OnLevelLoaded(int levelIndex)
        {
         
            //var level = _levelManager.CurrentLevel;
            //if (level != null)
            //{
                //level.OnLevelComplete += OnLevelComplete;
                //level.OnLevelFailed += OnLevelFailed;
           // }

            //_stateManager.SetState(GameStateManager.GameState.Playing);
        }

        private void OnLevelUnloaded(int levelIndex)
        {
            // var level = _levelManager.CurrentLevel;
            // if (level != null)
            // {
            //     level.OnLevelComplete -= OnLevelComplete;
            //     level.OnLevelFailed -= OnLevelFailed;
            // }
        }

        private void OnLevelComplete()
        {
            // _progressManager.UnlockLevel(_levelManager.CurrentLevelIndex + 1);
            // _stateManager.SetState(GameStateManager.GameState.GameOver);
            // Показать UI победы
        }

        private void OnLevelFailed()
        {
            //_stateManager.SetState(GameStateManager.GameState.GameOver);
            // Показать UI поражения
        }

        private void OnStateChanged()
        {
          
        }

      
        public void StartGame()
        {
            //_levelManager.LoadLevel(_progressManager.Progress.lastUnlockedLevel);
        }

        public void RestartLevel()
        {
            //_levelManager.RestartLevel();
        }

        public void NextLevel()
        {
            //_levelManager.NextLevel();
        }

        public void PauseGame()
        {
            // if (_stateManager.CurrentState == GameStateManager.GameState.Playing)
            // {
            //     _stateManager.SetState(GameStateManager.GameState.Paused);
            // }
        }

        public void ResumeGame()
        {
            // if (_stateManager.CurrentState == GameStateManager.GameState.Paused)
            // {
            //     _stateManager.SetState(GameStateManager.GameState.Playing);
            // }
        }
    }
}