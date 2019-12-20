using System;
using UnityEngine;

namespace ProjectScarlet
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth = 1f;

        public event Action OnDeath = delegate { };
        public event Action OnDamage = delegate { };
        public event Action OnHeal = delegate { };

        private void Start()
        {
            _currentHealth = GetBaseHealth();
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            OnDamage();

            if (_currentHealth == 0)
            {
                Debug.Log("Help I died");
                OnDeath();
            }
        }

        public void Heal(float healAmount)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, GetBaseHealth());

            OnHeal();
        }

        public float GetPercentage()
        {
            return _currentHealth / GetBaseHealth();
        }

        private float GetBaseHealth()
        {
            return GetComponent<CharacterBaseStats>().GetHealth();
        }
    }
}