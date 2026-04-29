using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _targetLayer;

        private Collider[] targets = new Collider[1];
        
        public void Attack()
        {
            if (0 < (Physics.OverlapSphereNonAlloc(transform.position, _distance, targets, _targetLayer)))
            {
                IHealthObject healthObject = targets[0].GetComponent<IHealthObject>();
                healthObject?.HandleDamage(_damage);
            }
        }
    }
}