using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Heal : MonoBehaviour
    {

        [SerializeField] private float _healAmount = 15;
        [SerializeField] private float _nextHeal;

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();

            if(health != null)
            {
                health.ModifyHealth(_healAmount);
            }
        }

        private void OnTriggerStay(Collider other) 
        {            
            Health health = other.GetComponent<Health>();

            if (health != null)
            {
                health.ModifyHealth(_healAmount);
            }
        }
    }
}