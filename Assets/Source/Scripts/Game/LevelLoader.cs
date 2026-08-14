using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Source.Scripts.Game
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _levelScenePrefix = "Level_";
        [SerializeField] private int _firstLevelIndex = 1;
        
        private Level _currentLevel;
        private int _currentLevelIndex;

        public static LevelLoader Instance {get; private set;}
        public Level CurrentLevel => _currentLevel;
        public int CurrentLevelIndex => _currentLevelIndex;

        public event Action<int> OnLevelLoaded;
        public event Action<int> OnLevelUnloaded;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadLevel(int levelIndex)
        {
            _currentLevelIndex = levelIndex;
            string sceneName = _levelScenePrefix + levelIndex;
        
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            OnLevelLoaded?.Invoke(_currentLevelIndex);
        
            _currentLevel = FindObjectOfType<Level>();
        }

        public void RestartLevel()
        {
            LoadLevel(_currentLevelIndex);
        }

        public void NextLevel()
        {
            LoadLevel(_currentLevelIndex + 1);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
            _currentLevel = null;
            OnLevelUnloaded?.Invoke(_currentLevelIndex);
        }

        public void UnloadCurrentLevel()
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
                _currentLevel = null;
                OnLevelUnloaded?.Invoke(_currentLevelIndex);
            }
        }

    }
}