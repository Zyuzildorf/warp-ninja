using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyRotater : MonoBehaviour
    {
        private Transform _target;
        
        public void Rotate()
        {
            transform.rotation =  Quaternion.LookRotation(_target.position - transform.position);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}