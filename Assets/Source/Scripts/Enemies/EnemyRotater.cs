using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyRotater
    {
        private readonly Transform _transform;

        public EnemyRotater(Transform transform)
        {
            _transform = transform;
        }
        
        public void Rotate(Vector3 target)
        {
            _transform.rotation =  Quaternion.LookRotation(target - _transform.position);
        }
    }
}