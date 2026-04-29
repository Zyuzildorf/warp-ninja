using UnityEngine;

namespace Source.Scripts.Enemies
{
    public abstract class MoveStrategy : MonoBehaviour
    {
        [field:SerializeField] public float Speed { get; private set; }

        public abstract void HandleMovement();
        public abstract void SetTarget(Transform target);
    }
}