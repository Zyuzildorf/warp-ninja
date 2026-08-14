using System;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerEnergy : MonoBehaviour
    {
        [SerializeField] private float _teleportCost = 1f;
        [SerializeField] private float _amount = 10f;

        public event Action OnOutOfEnergy;
        
        public bool TryUseEnergy()
        {
            _amount -= _teleportCost;

            if (_amount < 0)
            {
                _amount = 0f;
                OnOutOfEnergy?.Invoke();
                return false;
            }
            
            return true;
        }
    }
}