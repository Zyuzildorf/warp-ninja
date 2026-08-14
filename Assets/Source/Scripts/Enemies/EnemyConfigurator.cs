using Source.Scripts.Enemies.MoveStrategies;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyConfigurator : MonoBehaviour
    {
        public void Configure(Enemy enemy, EnemySpawnData data)
        {
            ApplyStrategyOverrides(enemy, data);
        }

        private void ApplyStrategyOverrides(Enemy enemy, EnemySpawnData data)
        {
            if (data.overrideMovementZone && data.movementZone != null)
            {
                Patroler patroler = enemy.GetComponent<Patroler>();
                
                if (patroler != null)
                {
                    patroler.SetMoveZone(data.movementZone);
                }
            }

            if (data.overrideThreatZone && data.threatZone != null)
            {
                Chaser chaser = enemy.GetComponent<Chaser>();
                
                if (chaser != null)
                {
                    chaser.SetThreatZone(data.threatZone);
                }
            }

            if (data.overrideStartPosition && data.startPosition != null)
            {
                PositionHolder holder = enemy.GetComponent<PositionHolder>();

                if (holder != null)
                {
                    holder.SetStartPosition(data.startPosition);
                    holder.SetDirection(data.isRightLooking);
                }
            }
        }
    }
}