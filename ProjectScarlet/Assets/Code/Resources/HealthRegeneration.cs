using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [RequireComponent(typeof(Health))]
    public class HealthRegeneration : MonoBehaviour
    {
        [SerializeField] private float _regenTime;
        [SerializeField] private float _regenCountdown;
        [SerializeField] private float _regenPercent;
        [SerializeField] private float _regenAmount;
        [SerializeField] private Health _health;
    
        private void Awake() 
        {
            _regenCountdown = _regenTime;

            _health = GetComponent<Health>();            
            if(_health == null)
                Destroy(this);    
        }

        // Update is called once per frame
        void Update()
        {
            _regenCountdown -= Time.deltaTime;

            if(_regenCountdown <= 0)
            {
                _regenCountdown = _regenTime;
                _regenAmount = _health.MaxHealth * _regenPercent;
                _health.ModifyHealth(_regenAmount);                
            }            
        }
    }
}