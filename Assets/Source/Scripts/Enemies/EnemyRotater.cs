using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyRotater
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public EnemyRotater(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }
        
        public void Rotate(Vector3 target)
        {
            Vector3 distance = target - _transform.position;
            Vector3 horizontalDirection = new Vector3(distance.x, 0, 0);

            if (horizontalDirection == Vector3.zero)
            {
                horizontalDirection = Vector3.forward;
            }
            
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(horizontalDirection), _speed * Time.deltaTime);
        }
    }
}