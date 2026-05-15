using System;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class TargetFinder : SearchStrategy
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _viewAngle;
        [SerializeField] private float _distance;

        private Collider[] _targets = new Collider[1];

        public override event Action<Transform> OnTargetFound;

        public override void CheckForTarget()
        {
            if (0 < (Physics.OverlapSphereNonAlloc(transform.position, _distance, _targets, _targetLayer)))
            {
                Vector3 directionOfTarget = (_targets[0].transform.position - transform.position).normalized;

                float angleToTarget = Vector3.Angle(transform.forward, directionOfTarget);

                if (angleToTarget <= _viewAngle / 2)
                {
                    OnTargetFound?.Invoke(_targets[0].transform);
                }
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 forward = transform.forward * _distance;

            float halfAngle = _viewAngle / 2;
            Quaternion leftRotation = Quaternion.Euler(-halfAngle, 0, 0);
            Quaternion rightRotation = Quaternion.Euler(halfAngle, 0, 0);

            Vector3 leftBoundary = leftRotation * forward;
            Vector3 rightBoundary = rightRotation * forward;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
            Gizmos.DrawWireSphere(transform.position + forward, _distance * 0.1f);
        }
    }
}