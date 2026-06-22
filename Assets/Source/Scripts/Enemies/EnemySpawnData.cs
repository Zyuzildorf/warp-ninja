using UnityEngine;

namespace Source.Scripts.Enemies
{
    [System.Serializable]
    public class EnemySpawnData
    {
        [Header("Prefab")] 
        public GameObject enemyPrefab;

        [Header("Position")] 
        public Vector3 position;

        [Header("Look Direction Override")] 
        public bool overrideLookDirection;
        public bool isRightLooking;
    }
}