using System;
using UnityEngine;

namespace ProjectScarlet
{
    public class Health : MonoBehaviour
    {
        public static event Action<Health> OnHealthAdded = delegate { };
        public static event Action<Health> OnHealthRemoved = delegate { };

        [SerializeField] private float _currentHealth = 1f;

        public float CurrentHealth {  get { return _currentHealth; } private set { _currentHealth = value;  } }

        public event Action<float> OnHealthPctChange = delegate { };

        public event Action OnDeath = delegate { };
        public event Action OnDamage = delegate { };
        public event Action OnHeal = delegate { };

        private void Start()
        {
            SetupHealth();
        }

        private void OnEnable()
        {
            SetupHealth();
        }

        public void SetupHealth()
        {
            CurrentHealth = GetBaseHealth();
            OnHealthAdded(this);
        }

        public void ModifyHealth(float amount)
        {
            CurrentHealth += amount;

            float currentHealthPercent = CurrentHealth / GetBaseHealth();

            OnHealthPctChange(currentHealthPercent);

            if(CurrentHealth <= 0)
            {
                OnHealthRemoved(this);
                OnDeath();
                Destroy(this.gameObject);
            }
            if(amount > 0)
            {
                OnHeal();
            }
            if(amount < 0)
            {
                OnDamage();
            }
        }

        public float GetPercentage()
        {
            return CurrentHealth / GetBaseHealth();
        }

        private float GetBaseHealth()
        {
            return GetComponent<CharacterBaseStats>().GetHealth();
        }
    }
}