using System;
using System.Collections.Generic;
using Source.Scripts.Enemies;
using Source.Scripts.Player;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField] private EnemyConfigurator _configurator;
    [SerializeField] private Player _player;

    [Header("Enemy Spawns")] 
    [SerializeField] private List<EnemySpawnData> _enemiesSpawnData;

    private List<Enemy> _activeEnemies = new List<Enemy>();
    private int _aliveEnemyCount;

    public event Action OnAllEnemiesDefeated;
    public event Action OnPlayerDeath;
    public event Action OnPlayerOutOfEnergy;

    private void Awake()
    {
        SpawnAllEnemies();
    }

    private void OnEnable()
    {
        _player.OnOutOfEnergy += HandlePlayerOutOfEnergy;
        _player.OnDie += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        _player.OnOutOfEnergy -= HandlePlayerOutOfEnergy;
        _player.OnDie -= HandlePlayerDeath;
    }

    private void OnDestroy()
    {
        CleanUp();
    }
    
    private void SpawnAllEnemies()
    {
        foreach (var spawnData in _enemiesSpawnData)
        {
            GameObject obj = Instantiate(spawnData.enemyPrefab, spawnData.position, Quaternion.identity);
            Enemy enemy = obj.GetComponent<Enemy>();

            if (enemy != null)
            {
                _configurator.Configure(enemy, spawnData);

                _activeEnemies.Add(enemy);
                _aliveEnemyCount++;
                enemy.OnDie += OnEnemyDied;
            }
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _aliveEnemyCount--;
        
        if (_aliveEnemyCount <= 0)
        {
            OnAllEnemiesDefeated?.Invoke();
        }
    }

    private void HandlePlayerOutOfEnergy()
    {
        Debug.Log("You lose. Out of energy.");
        OnPlayerOutOfEnergy?.Invoke();
    }

    private void HandlePlayerDeath()
    {
        OnPlayerDeath?.Invoke();
        Debug.Log("You lose. Death.");
    }

    private void CleanUp()
    {
        foreach (var enemy in _activeEnemies)
        {
            if (enemy != null)
                enemy.OnDie -= OnEnemyDied;
        }

        _activeEnemies.Clear();
        _aliveEnemyCount = 0;
    }
}