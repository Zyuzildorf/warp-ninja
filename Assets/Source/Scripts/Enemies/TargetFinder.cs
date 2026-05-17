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

        private void OnDrawGizmos()
        {
            float halfAngle = _viewAngle * 0.5f;
            Vector3 forward = transform.forward;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + forward * _distance);

            Gizmos.color = Color.yellow;

            Vector3 leftBorder = Quaternion.Euler(0, -halfAngle, 0) * forward;
            Gizmos.DrawLine(transform.position, transform.position + leftBorder * _distance);

            Vector3 rightBorder = Quaternion.Euler(0, halfAngle, 0) * forward;
            Gizmos.DrawLine(transform.position, transform.position + rightBorder * _distance);

            Vector3 topBorder = Quaternion.Euler(halfAngle, 0, 0) * forward;
            Vector3 bottomBorder = Quaternion.Euler(-halfAngle, 0, 0) * forward;
            Gizmos.DrawLine(transform.position, transform.position + topBorder * _distance);
            Gizmos.DrawLine(transform.position, transform.position + bottomBorder * _distance);
        }
    }
}