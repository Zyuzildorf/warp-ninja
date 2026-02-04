using System;
using UnityEngine;

namespace Source.Scripts.Game
{
    public class InputReader : MonoBehaviour
    {
        public event Action OnMouseOver;
        public event Action<Vector3> OnTeleportKeyPress;
        public event Action<Vector3> OnMouseMoved;

        private void Update()
        {
            CheckTeleportKeyPress();
            CheckAimDirection();
            CheckMouseOver();
        }

        private void CheckAimDirection()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                OnMouseMoved?.Invoke(Input.mousePosition);
            }
        }
    
        private void CheckMouseOver()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnMouseOver?.Invoke();
            }
        }

        private void CheckTeleportKeyPress()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnTeleportKeyPress?.Invoke(Input.mousePosition);
            }
        }
    }
}