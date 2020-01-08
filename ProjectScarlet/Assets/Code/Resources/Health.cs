using System;
using UnityEngine;

namespace ProjectScarlet
{
    public class Health : MonoBehaviour
    {
        public static event Action<Health> OnHealthAdded = delegate { };
        public static event Action<Health> OnHealthRemoved = delegate { };

        [SerializeField] private float _currentHealth = 1f;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Experience _experience;

        public float CurrentHealth {  get { return _currentHealth; } private set { _currentHealth = value;  } }

        public event Action<float> OnHealthPctChange = delegate { };

        public event Action OnDeath = delegate { };
        public event Action OnDamage = delegate { };
        public event Action OnHeal = delegate { };

        private void Awake()
        {
            _experience = GetComponent<Experience>();

            if(_experience != null)
            {
                _experience.OnLevelUp += HandleLevelUp;
            }
        }

        private void Start()
        {
            SetupHealth();
        }

        private void OnEnable()
        {
            SetupHealth();
        }        

        public void Update()
        {
            _maxHealth = GetBaseHealth();
        }

        public void SetupHealth()
        {
            CurrentHealth = GetBaseHealth();
            OnHealthAdded(this);
            //OnHealthPctChange(CurrentHealth / GetBaseHealth());
            //OnHeal();
        }

        public void ModifyHealth(float amount)
        {
            CurrentHealth += amount;

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, GetBaseHealth());

            float currentHealthPercent = CurrentHealth / GetBaseHealth();

            OnHealthPctChange(currentHealthPercent);

            if(CurrentHealth <= 0)
            {
                OnHealthRemoved(this);
                OnDeath();
                //transform.position = new Vector3(0,0,0);
                //this.gameObject.SetActive(false);
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

        private void HandleLevelUp()
        {
            ModifyHealth(GetBaseHealth() - CurrentHealth);
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