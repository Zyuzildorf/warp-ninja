using System.Collections.Generic;
using Source.Scripts.Enemies;
using Source.Scripts.Enemies.MoveStrategies;
using UnityEngine;


namespace Source.Scripts.Game
{
    public class Level : MonoBehaviour
    {
        [Header("Enemy Spawns")] 
        [SerializeField] private List<EnemySpawnData> enemySpawns;

        private void Start()
        {
            SpawnAllEnemies();
        }

        private void SpawnAllEnemies()
        {
            foreach (var spawn in enemySpawns)
            {
                GameObject enemy = Instantiate(spawn.enemyPrefab, spawn.position, Quaternion.identity);
                ApplyOverrides(spawn.enemyPrefab,spawn);
            }
        }

        private void ApplyOverrides(GameObject enemy, EnemySpawnData spawn)
        {
            if (spawn.overrideLookDirection)
            {
                var moveStrategy = enemy.GetComponent<MoveStrategy>();

                if (moveStrategy.TryGetComponent(out PositionHolder holder))
                {
                    holder.SetDirection(spawn.isRightLooking);
                }
            }
        }
    }
}