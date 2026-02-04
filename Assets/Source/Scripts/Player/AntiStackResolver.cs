using UnityEngine;

namespace Source.Scripts.Player
{
    public class AntiStackResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private float _radiusOffset;
        [SerializeField] private float _safetyMargin = 0.05f;

        public Vector3 GetUnstuckPosition(Vector3 desiredPosition, Collider collider)
        {
            float checkRadius = collider.bounds.extents.x * _radiusOffset;

            Collider[] overlaps = Physics.OverlapSphere(desiredPosition, checkRadius, _obstacleLayer);

            foreach (var obstacle in overlaps)
            {
                bool isOverlapping = Physics.ComputePenetration(collider, desiredPosition, transform.rotation,
                    obstacle, obstacle.transform.position, obstacle.transform.rotation,
                    out Vector3 direction, out float distance);

                if (isOverlapping)
                {
                    desiredPosition += direction * (distance + _safetyMargin);
                }
            }

            return desiredPosition;
        }
    }
}