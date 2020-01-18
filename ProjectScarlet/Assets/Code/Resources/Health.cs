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
        [SerializeField] private float _modifier;

        public float CurrentHealth {  get { return _currentHealth; } private set { _currentHealth = value;  } }

        public event Action<float> OnHealthPctChange = delegate { };

        public event Action OnDeath = delegate { };
        public event Action OnDamage = delegate { };
        public event Action OnHeal = delegate { };

        private void Awake()
        {
            _experience = GetComponent<Experience>();
            _modifier = 1;
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
            _experience = GetComponent<Experience>();            

            if (_experience != null)
            {
                _experience.OnLevelUp += HandleLevelUp;
            }
        }
        private void OnDisable() 
        {
            OnHealthRemoved(this);
        }        

        public void Update()
        {
            _maxHealth = GetBaseHealth();
        }

        public void SetupHealth()
        {
            CurrentHealth = GetBaseHealth();
            _modifier = 1;
            OnHealthAdded(this);
        }

        public void ModifyHealth(float amount)
        {
            if(amount > 0)
            {
                CurrentHealth += amount;
            }
            else
            {
                CurrentHealth += amount * _modifier;
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, GetBaseHealth());

            float currentHealthPercent = CurrentHealth / GetBaseHealth();

            OnHealthPctChange(currentHealthPercent);

            if(CurrentHealth <= 0)
            {
                
                OnDeath();                

                if(transform.gameObject.tag != "Player")
                {
                    this.gameObject.SetActive(false);
                    transform.position = new Vector3(1, 2, 2);
                }
                //Destroy(this.gameObject);
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

        public void SetModifier(float modifier)
        {
            _modifier = modifier;
        }

        public float GetModifier()
        {
            return _modifier;
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