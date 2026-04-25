using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _viewAngle;
        [SerializeField] private float _distance;

        private Collider[] targets = new Collider[1];

        public bool TryFindTarget(out Transform target)
        {
            if (0 < (Physics.OverlapSphereNonAlloc(transform.position, _distance, targets, _targetLayer)))
            {
                Vector3 directionOfTarget = (targets[0].transform.position - transform.position).normalized;

                float angleToTarget = Vector3.Angle(transform.forward, directionOfTarget);

                if (angleToTarget <= _viewAngle / 2)
                {
                    target = targets[0].transform;
                    return true;
                }
            }

            target = null;
            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 forward = transform.forward * _distance;

            // Рисуем дугу конуса
            float halfAngle = _viewAngle / 2;
            Quaternion leftRotation = Quaternion.Euler(0, -halfAngle, 0);
            Quaternion rightRotation = Quaternion.Euler(0, halfAngle, 0);

            Vector3 leftBoundary = leftRotation * forward;
            Vector3 rightBoundary = rightRotation * forward;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
            Gizmos.DrawWireSphere(transform.position + forward, _distance * 0.1f);
        }
    }
}