using System;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerRotater : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _startPosition;
        
        public Vector3 Direction {get; private set;}

        public event Action<Vector3> OnRotationChanged;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPosition = startPosition;
        }
        
        public void Rotate(Vector3 newPosition)
        {
            SetDirection(newPosition);
            
            _transform.rotation = Quaternion.LookRotation(Direction);
        }
        
        private void SetDirection(Vector3 newPosition)
        {
            Direction = (_startPosition - newPosition).normalized;
            
            OnRotationChanged?.Invoke(Direction);
        }
    }
}