using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBarPrefab;
        private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

        private void Awake()
        {
            Health.OnHealthAdded += AddHealthBar;
            Health.OnHealthRemoved += RemoveHealthBar;
        }

        private void RemoveHealthBar(Health health)
        {
            if (healthBars.ContainsKey(health))
            {
                Destroy(healthBars[health].gameObject);
                healthBars.Remove(health);
            }
        }

        private void AddHealthBar(Health health)
        {
            if(healthBars.ContainsKey(health) == false)
            {
                var healthBar = Instantiate(healthBarPrefab, transform);
                healthBars.Add(health, healthBar);
                healthBar.SetHealth(health);
            }
        }
    }
}