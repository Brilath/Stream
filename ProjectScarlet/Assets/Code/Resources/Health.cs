using System;
using UnityEngine;

namespace ProjectScarlet
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _currentHealth = 1f;

        public event Action OnDeath = delegate { };

        private void Start()
        {
            _currentHealth = GetBaseHealth();
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            if(_currentHealth == 0)
            {
                Debug.Log("Help I died");
                OnDeath();
            }
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