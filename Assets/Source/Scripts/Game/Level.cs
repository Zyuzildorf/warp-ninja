using System;
using System.Collections.Generic;
using Source.Scripts.Enemies;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Dependencies")] [SerializeField]
    private EnemyConfigurator _configurator;

    [Header("Enemy Spawns")] [SerializeField]
    private List<EnemySpawnData> _enemySpawns;

    private List<Enemy> _activeEnemies = new List<Enemy>();
    private int _aliveEnemyCount;

    public event Action OnAllEnemiesDefeated;

    private void Awake()
    {
        SpawnAllEnemies();
    }

    private void SpawnAllEnemies()
    {
        foreach (var spawn in _enemySpawns)
        {
            GameObject obj = Instantiate(spawn.enemyPrefab, spawn.position, Quaternion.identity);
            Enemy enemy = obj.GetComponent<Enemy>();

            if (enemy != null)
            {
                _configurator.Configure(enemy, spawn);

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

    private void OnDestroy()
    {
        CleanUp();
    }
}