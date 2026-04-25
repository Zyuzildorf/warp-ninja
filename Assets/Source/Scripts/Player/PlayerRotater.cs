using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerRotater : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Rotate(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                _transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}