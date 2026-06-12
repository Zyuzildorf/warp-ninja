using System;

namespace Source.Scripts.Other
{
    public class Health
    {
        private int _currentHealth;
        
        public event Action OnDeath;

        public Health(int maxHealth)
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = _currentHealth > damage ? _currentHealth - damage : _currentHealth = 0;
            
            if (_currentHealth == 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}