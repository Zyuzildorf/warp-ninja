using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerPointer : MonoBehaviour
    {
        [SerializeField] private Transform _arrow;
        [SerializeField] private Transform _shootPoint;

        private Vector3 _direction;
        
        private void Awake()
        {
            ReleaseObject();
        }

        public void ChangeRotation(Vector3 direction)
        {
            _arrow.position = new Vector3(_shootPoint.position.x, _shootPoint.position.y, 0);
            _arrow.rotation = Quaternion.LookRotation(direction);
        }

        public void GetObject()
        {
            _arrow.gameObject.SetActive(true);
        }
        
        public void ReleaseObject()
        {
            _arrow.gameObject.SetActive(false);
        }
    }
}