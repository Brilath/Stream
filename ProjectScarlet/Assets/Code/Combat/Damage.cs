using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;

        private void OnTriggerEnter(Collider other)
        {
            Health targetHealth = other.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(_damage);    
            }
        }
    }
}