using UnityEngine;

namespace Source.Scripts.Enemies.MoveStrategies
{
    public abstract class MoveStrategy : MonoBehaviour
    {
        [field:SerializeField] public float MoveSpeed { get; private set; }
        [field:SerializeField] public float RotationSpeed { get; private set; }

        public abstract void HandleMovement();
        public abstract void SetTarget(Transform target);
    }
}