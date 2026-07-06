using Source.Scripts.Enemies.MoveStrategies;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyConfigurator : MonoBehaviour
    {
        public void Configure(Enemy enemy, EnemySpawnData data)
        {
            ApplyStrategyOverrides(enemy.gameObject, data);
        }

        private void ApplyStrategyOverrides(GameObject enemyObject, EnemySpawnData data)
        {
            if (data.overrideMovementZone && data.movementZone != null)
            {
                var patroler = enemyObject.GetComponent<Patroler>();
                if (patroler != null)
                    patroler.SetMoveZone(data.movementZone);
            }

            if (data.overrideThreatZone && data.threatZone != null)
            {
                var chaser = enemyObject.GetComponent<Chaser>();
                if (chaser != null)
                    chaser.SetThreatZone(data.threatZone);
            }

            if (data.overrideStartPosition && data.startPosition != null)
            {
                var holder = enemyObject.GetComponent<PositionHolder>();
                if (holder != null)
                    holder.SetStartPosition(data.startPosition);
                    holder.SetDirection(data.isRightLooking);
            }
        }
    }
}