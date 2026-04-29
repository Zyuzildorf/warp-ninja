using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyMover : Mover
    {
        public EnemyMover(Transform transform, Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
            Transform = transform;
        }

        public override void Move()
        {
            Vector3 direction = Target.position - Transform.position;

            Rigidbody.velocity = direction.normalized * Speed;
        }

        public override void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public override void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}