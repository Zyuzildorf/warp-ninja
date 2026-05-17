using UnityEngine;

namespace Source.Scripts.Enemies.HostileStrategies
{
    public abstract class HostileStrategy : MonoBehaviour
    {
        public abstract void Execute();
        public abstract void SetTarget(Transform target);
    }
}