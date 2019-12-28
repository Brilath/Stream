using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBarPrefab;
        private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();
        [SerializeField] private GameObject player;
        [SerializeField] private float viewDistance = 15f;

        private void Awake()
        {
            Health.OnHealthAdded += AddHealthBar;
            Health.OnHealthRemoved += RemoveHealthBar;
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            foreach(KeyValuePair<Health, HealthBar> healthBar in healthBars)
            {
                healthBar.Value.gameObject.SetActive(InViewDistance(healthBar.Key));
            }
        }

        private bool InViewDistance(Health health)
        {
            bool isVisible = false;

            float distanceFromPlayer = Vector3.Distance(player.transform.position,
                    health.transform.position);

            if (distanceFromPlayer <= viewDistance)
                isVisible = true;

            return isVisible;
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