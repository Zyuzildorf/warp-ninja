using UnityEngine;

namespace Source.Scripts.Enemies
{
    [System.Serializable]
    public class EnemySpawnData
    {
        [Header("Prefab & Position")]
        public GameObject enemyPrefab;
        public Vector3 position;

        [Header("Strategy Overrides")]
        public bool overrideMovementZone;
        public Collider movementZone;

        public bool overrideThreatZone;
        public Collider threatZone;

        public bool overrideStartPosition;
        public Vector3 startPosition;
        
        public bool isRightLooking = false;
    }
}